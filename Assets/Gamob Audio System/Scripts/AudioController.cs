using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gamob
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSystem _audioSystem;
        [SerializeField] private Slider _slider;
        [SerializeField] private AudioChannels _audioChannel;

        private void Start()
        {
            _slider.onValueChanged.AddListener(HandleVolumeChange);
            _slider.minValue = 0.0001f;
        }

        private void HandleVolumeChange(float volume)
        {            
            _audioSystem.ChangeChannelVolume(_audioChannel, volume);
        }
    }
}
