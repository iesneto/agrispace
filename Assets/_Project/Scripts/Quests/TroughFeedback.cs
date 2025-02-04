using Gamob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Quests
{
    public class TroughFeedback : MonoBehaviour, IFeedback
    {
        
        [SerializeField] private MeshRenderer _water;

        private PlayGameAudio _waterSFX;

        private void Start()
        {
            _water.enabled = false;
        }

        public void Run()
        {
            PlayAudio();
            _water.enabled = true;
            
        }

        public void PlayAudio()
        {
            if (_waterSFX == null)
            {
                return;
            }
            _waterSFX.PlayAudio();
        }
    }
}

