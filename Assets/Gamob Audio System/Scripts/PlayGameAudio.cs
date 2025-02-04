//using Coimbra.Services;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Gamob
{
    public sealed class PlayGameAudio : MonoBehaviour
    {
        public enum AudioType
        {
            Music,
            SFX,
            UI
        }

        [Header("Select the Audio Type")]
        [SerializeField] private AudioType _audioType = AudioType.Music;
        [Header("Select the Audio to be Play")]
        [SerializeField] private MusicAudios _musicAudios;
        [SerializeField] private SFXAudios _sfxAudios;
        [SerializeField] private UIAudios _uiAudios;

        [Header("Configurations")]
        [Tooltip("If the audio should be play on the Start")]
        [SerializeField] private bool _playOnStart = true;
        [Tooltip("Add a delay to play the music on the Start")]
        [SerializeField] private float _delayMusicSeconds = 0f;
        [Tooltip("If a button is referenced, this will listining to the button click")]
        [SerializeField] private Button _button;
        [Header("Music configuration")]
        [SerializeField] private bool _additionalMusic = false;
        [Header("Minigame BG Music stop on destroy")]
        [SerializeField] private bool _musicStopOnDestroy;
        
        private AudioSystem _audioSystem;
        //private IAudioService _audioSystem; // Use Coimbra.Services

        public void PlayAudio()
        {
            switch (_audioType)
            {
                case AudioType.Music:
                    if (_additionalMusic)
                    {
                        _audioSystem.PlayLoopMusic(_musicAudios);
                    }
                    else
                    {
                        _audioSystem.ChangeMainMusic(_musicAudios);
                    }
                    break;
                case AudioType.SFX:
                    _audioSystem.PlayOneShotClip(_sfxAudios);
                    break;
                case AudioType.UI:
                    _audioSystem.PlayOneShotClip(_uiAudios);
                    break;
            }
        }

        private void Start()
        {
            // Uncomment this to use Coimbra.Services
            //_audioSystem = ServiceLocator.GetChecked<IAudioService>();
            _audioSystem = AudioSystem.Instance;

            if(_button == null)
            {
                _button = GetComponent<Button>();
            }

            if (_button != null)
            {
                _button.onClick.AddListener(PlayAudio);
            }

            if (_playOnStart)
            {
                StartCoroutine(DelayStart());
            }
        }

        private void OnDisable()
        {
            if (_button != null)
            {
                _button.onClick.RemoveAllListeners();
            }
        }

        private void OnDestroy()
        {
            if (_audioType != AudioType.Music || !_musicStopOnDestroy)
            {
                return;
            }
            _audioSystem.StopLoopMusic(_musicAudios);
        }

        private IEnumerator DelayStart()
        {
            yield return new WaitForSeconds(_delayMusicSeconds);
            PlayAudio();
        }
    }

}
