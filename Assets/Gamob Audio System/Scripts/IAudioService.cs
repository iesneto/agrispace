using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gamob
{
    public interface IAudioService //: IService // Use Coimbra.Services
    {
        public AudioSettings GetAudioSettings();

        void InitializeSystem();

        public void PlayOneShotClip(Enum audioClip);

        public void PlayLoopMusic(MusicAudios audioClip);

        public void StopLoopMusic(MusicAudios audioClip);

        public void ChangeMainMusic(MusicAudios audioClip);

        public void StopAllAdditionalLoops();

        public void StopChannel(AudioChannels channel);

        public void ChangeChannelVolume(AudioChannels channel, float volume);

        public void MuteChannel(AudioChannels channel);

        public void UnMuteChannel(AudioChannels channel);

        public AudioSystemScriptable GetSoundScriptable();

        public AudioSource GetAudioSource(AudioChannels channel);

        public float GetAudioVolume(AudioChannels channel);
    }
}
