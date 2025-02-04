using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamob;

namespace Agrispace.Quests
{
    public class CropFeedback : MonoBehaviour, IFeedback
    {
        [SerializeField] private int _coolDownTime;
        private MeshRenderer _meshRenderer;
        private Collider _collider;
        private PlayGameAudio _cropSFX;

        public void Run()
        {
            PlayAudio();
            ItemCoolDown();
        }

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
            _cropSFX = GetComponent<PlayGameAudio>();
        }

        private async void ItemCoolDown()
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            await UniTask.Delay(_coolDownTime);
            _meshRenderer.enabled = true;
            _collider.enabled = true;
        }

        public void PlayAudio()
        {
            if(_cropSFX == null) 
            {
                return;
            }
            _cropSFX.PlayAudio();
        }
    }
}

