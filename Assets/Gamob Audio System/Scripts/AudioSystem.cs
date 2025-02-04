using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Audio;
using Agrispace.Core;

namespace Gamob
{
    [Serializable]
    public struct AudioSettings
    {
        public float VolumeMaster;
        public bool MuteMaster;
        public float VolumeMusic;
        public bool MuteMusic;
        public float VolumeSFX;
        public bool MuteSFX;
        public float VolumeUI;
        public bool MuteUI;

        public AudioSettings(float volumeMaster, bool muteMaster, float volumeMusic, bool muteMusic, float volumeSFX, bool muteSFX, float volumeUI, bool muteUI)
        {
            VolumeMaster = volumeMaster;
            MuteMaster = muteMaster;
            VolumeMusic = volumeMusic;
            MuteMusic = muteMusic;
            VolumeSFX = volumeSFX;
            MuteSFX = muteSFX;
            VolumeUI = volumeUI;
            MuteUI = muteUI;
        }
    }

    public enum AudioChannels
    {
        Master = -1,
        MusicChannel = 0,
        SFXChannel = 1,
        UIChannel = 2,
    }

    public class AudioSystem : Singleton<AudioSystem>, IAudioService // Actor, IAudioService // Usar Coimbra.Services ou fazer Singleton
    {
        [SerializeField] private AudioSystemScriptable _audioScriptable;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioMixerGroup _musicGroup;
        [SerializeField] private AudioSource _musicChannel;
        [SerializeField] private AudioSource _sfxChannel;
        [SerializeField] private AudioSource _uiChannel;
        private Dictionary<AudioChannels, AudioSource> _channels = new Dictionary<AudioChannels, AudioSource>();
        private List<AudioSource> _aditionalMusics = new List<AudioSource>();
        private AudioSettings _audioSettings = new AudioSettings
        {
            VolumeMaster = 1f,
            MuteMaster = false,
            VolumeMusic = 0.5f,
            MuteMusic = false,
            VolumeSFX = 0.5f,
            MuteSFX = false,
            VolumeUI = 0.5f,
            MuteUI = false,
        };

        private const string MasterVolumeParameterName = "MasterVolume";
        private const string MusicVolumeParameterName = "MusicVolume";
        private const string SfxVolumeParameterName = "SFXVolume";
        private const string UIVolumeParameterName = "UIVolume";

        private const string FileName = "audio.json";

        private MusicAudios _actualMusic = MusicAudios.NoneBGMusic;

        public AudioSource GetAudioSource(AudioChannels channel)
        {
            AudioSource audioSource = _musicChannel;
            switch (channel)
            {
                case AudioChannels.MusicChannel:
                    audioSource = _musicChannel;
                    break;
                case AudioChannels.SFXChannel:
                    audioSource = _sfxChannel;
                    break;
                case AudioChannels.UIChannel:
                    audioSource = _uiChannel;
                    break;
            }
            return audioSource;
        }

        public float GetAudioVolume(AudioChannels channel)
        {
            switch (channel)
            {
                case AudioChannels.MusicChannel:
                    return _audioSettings.VolumeMusic;                    
                case AudioChannels.SFXChannel:
                    return _audioSettings.VolumeSFX;   
                case AudioChannels.UIChannel:
                    return _audioSettings.VolumeUI;
                default:
                    return _audioSettings.VolumeMaster;                    
            }
        }

        public void ChangeChannelVolume(AudioChannels channel, float volume)
        {
            switch (channel)
            {
                case AudioChannels.MusicChannel:
                    _audioSettings.VolumeMusic = volume;
                    break;
                case AudioChannels.SFXChannel:
                    _audioSettings.VolumeSFX = volume;
                    break;
                case AudioChannels.UIChannel:
                    _audioSettings.VolumeUI = volume;
                    break;
                default:
                    _audioSettings.VolumeMaster = volume;
                    break;
            }

            SetupMixerVolumes(channel);
        }

        public void ChangeMainMusic(MusicAudios audioClip)
        {
            if (audioClip == _actualMusic)
            {
                return;
            }

            AudioClip audio = GetAudioClip(audioClip);
            if (audio == null)
            {
                Debug.LogWarning("Audio clip not found " + audioClip);
                return;
            }

            _channels[AudioChannels.MusicChannel].Stop();
            _channels[AudioChannels.MusicChannel].clip = audio;
            _channels[AudioChannels.MusicChannel].loop = true;
            _channels[AudioChannels.MusicChannel].Play();
            _actualMusic = audioClip;
        }

        public AudioSettings GetAudioSettings()
        {
            return _audioSettings;
        }

        public AudioSystemScriptable GetSoundScriptable()
        {
            return _audioScriptable;
        }

        public void MuteChannel(AudioChannels channel)
        {
            switch (channel)
            {
                case AudioChannels.MusicChannel:
                    _audioSettings.MuteMusic = true;
                    break;
                case AudioChannels.SFXChannel:
                    _audioSettings.MuteSFX = true;
                    break;
                case AudioChannels.UIChannel:
                    _audioSettings.MuteUI = true;
                    break;
                case AudioChannels.Master:
                    _audioSettings.MuteMaster = true;
                    break;
            }
            SetupMixerVolumes(channel);
        }

        public void PlayLoopMusic(MusicAudios audioClip)
        {
            AudioClip audio = GetAudioClip(audioClip);
            if (audio == null)
            {
                Debug.LogWarning("Audio clip not found " + audioClip);
                return;
            }

            if (!_channels[AudioChannels.MusicChannel].isPlaying)
            {
                _channels[AudioChannels.MusicChannel].clip = audio;
                _channels[AudioChannels.MusicChannel].loop = true;
                _channels[AudioChannels.MusicChannel].Play();
            }
            else
            {
                PlayAdditionalLoopClip(audio);
            }
        }

        public void PlayOneShotClip(Enum audioClip)
        {
            AudioChannels channel = GetAudioChannel(audioClip);
            AudioClip audio = GetAudioClip(audioClip);

            if (audio == null ||
                channel == AudioChannels.Master ||
                channel == AudioChannels.MusicChannel)
            {
                Debug.LogWarning("Audio clip not found " + audioClip);
                return;
            }

            _channels[channel].PlayOneShot(audio, _audioSettings.VolumeSFX);
        }

        public void StopAllAdditionalLoops()
        {
            foreach (AudioSource source in _aditionalMusics)
            {
                source.Stop();
            }
        }

        public void StopChannel(AudioChannels channel)
        {
            if (channel == AudioChannels.MusicChannel)
            {
                _channels[AudioChannels.MusicChannel].Stop();
                StopAllAdditionalLoops();
            }
            else
            {
                _channels[channel].Stop();
            }
        }

        public void StopLoopMusic(MusicAudios audioClip)
        {
            AudioClip audio = GetAudioClip(audioClip);
            if (_musicChannel.clip == audio)
            {
                _musicChannel.Stop();
                _musicChannel.clip = null;
                _actualMusic = MusicAudios.NoneBGMusic;
            }
            else
            {
                foreach (AudioSource source in _aditionalMusics)
                {
                    if (source.clip == audio)
                    {
                        source.Stop();
                        source.clip = null;
                    }
                }
            }
        }

        public void UnMuteChannel(AudioChannels channel)
        {
            switch (channel)
            {
                case AudioChannels.MusicChannel:
                    _audioSettings.MuteMusic = false;
                    break;
                case AudioChannels.SFXChannel:
                    _audioSettings.MuteSFX = false;
                    break;
                case AudioChannels.UIChannel:
                    _audioSettings.MuteUI = false;
                    break;
                case AudioChannels.Master:
                    _audioSettings.MuteMaster = false;
                    break;
            }
            SetupMixerVolumes(channel);
        }

        public void InitializeSystem()
        {
            DontDestroyOnLoad(this);            
            SetupChannels();
            SetupFileAudio();
        }

        // Remove this when using Coimbra.Services
        private void Start()
        {
            InitializeSystem();
        }

        private AudioChannels GetAudioChannel(Enum audioClip)
        {
            Type audio = audioClip.GetType();
            if (audio == typeof(MusicAudios))
            {
                return AudioChannels.MusicChannel;
            }

            if (audio == typeof(SFXAudios))
            {
                return AudioChannels.SFXChannel;
            }

            if (audio == typeof(UIAudios))
            {
                return AudioChannels.UIChannel;
            }

            return AudioChannels.Master;
        }

        private AudioClip GetAudioClip(Enum audioClip)
        {
            Type audio = audioClip.GetType();
            if (audio == typeof(MusicAudios))
            {
                return _audioScriptable.MusicAudios[(MusicAudios)audioClip];
            }

            if (audio == typeof(SFXAudios))
            {
                return _audioScriptable.SFXAudios[(SFXAudios)audioClip];
            }

            if(audio == typeof(UIAudios))
            {
                return _audioScriptable.UIAudios[(UIAudios)audioClip];
            }

            return null;
        }

        private void PlayAdditionalLoopClip(AudioClip audio)
        {
            AudioSource freeSource = new AudioSource();
            bool hasFreeSource = false;
            foreach (AudioSource source in _aditionalMusics)
            {
                if (source.isPlaying) continue;
                hasFreeSource = true;
                freeSource = source;
            }

            if (hasFreeSource)
            {
                freeSource.clip = audio;
                freeSource.loop = true;
                freeSource.Play();
            }
            else
            {
                AudioSource source = CreateAdditionalTrack(audio);
                _aditionalMusics.Add(source);
            }
        }

        private AudioSource CreateAdditionalTrack(AudioClip audio)
        {
            GameObject gameObject = new GameObject();
            gameObject.transform.parent = transform;
            gameObject.transform.name = "AudioSource_" + audio.name;
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.loop = true;
            source.clip = audio;
            source.volume = 1;
            source.mute = false;
            source.outputAudioMixerGroup = _musicGroup;
            source.Play();
            return source;
        }

        private void SetupMixerVolumes(AudioChannels channel)
        {
            switch (channel)
            {
                case AudioChannels.Master:
                    if (_audioSettings.MuteMaster)
                    {
                        _audioMixer.SetFloat(MasterVolumeParameterName, -80);

                    }
                    else
                    {
                        _audioMixer.SetFloat(MasterVolumeParameterName, Mathf.Log10(_audioSettings.VolumeMaster) * 20);
                    }
                    break;
                case AudioChannels.MusicChannel:
                    if (_audioSettings.MuteMusic)
                    {
                        _musicChannel.mute = true;
                    }
                    else
                    {
                        _musicChannel.mute = false;
                        _audioMixer.SetFloat(MusicVolumeParameterName, Mathf.Log10(_audioSettings.VolumeMusic) * 20);
                    }
                    break;
                case AudioChannels.SFXChannel:
                    if (_audioSettings.MuteSFX)
                    {
                        _sfxChannel.mute = true;
                    }
                    else
                    {
                        _sfxChannel.mute = false;
                        _audioMixer.SetFloat(SfxVolumeParameterName, Mathf.Log10(_audioSettings.VolumeSFX) * 20);
                    }
                    break;
                case AudioChannels.UIChannel:
                    if (_audioSettings.MuteUI)
                    {
                        _uiChannel.mute = true;
                    }
                    else
                    {
                        _uiChannel.mute = false;
                        _audioMixer.SetFloat(UIVolumeParameterName, Mathf.Log10(_audioSettings.VolumeUI) * 20);
                    }
                    break;
            }
        }

        private void SetupChannels()
        {
            _channels.Add(AudioChannels.MusicChannel, _musicChannel);
            _channels.Add(AudioChannels.SFXChannel, _sfxChannel);
            _channels.Add(AudioChannels.UIChannel, _uiChannel);
        }

        private void SetupFileAudio()
        {
            AudioSettings settings = new AudioSettings(0.5f, false, 0.5f, false, 0.5f, false, 0.5f, false);
            _audioSettings = settings;
            SetupMixerVolumes(AudioChannels.Master);
            SetupMixerVolumes(AudioChannels.MusicChannel);
            SetupMixerVolumes(AudioChannels.SFXChannel);
            SetupMixerVolumes(AudioChannels.UIChannel);
        }

       

        

        
    }
}
