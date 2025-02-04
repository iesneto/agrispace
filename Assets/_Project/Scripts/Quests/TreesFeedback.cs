using Cysharp.Threading.Tasks;
using Gamob;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Quests
{
    public class TreesFeedback : MonoBehaviour, IFeedback
    {

        [SerializeField] private int _coolDownTime;
        [SerializeField] private GameObject _fruits;
        private Collider _collider;
        private PlayGameAudio _treeSFX;

        public void Run()
        {
            PlayAudio();
            ItemCoolDown();
        }

        private void Start()
        {
            _collider = GetComponent<Collider>();
            _treeSFX = GetComponent<PlayGameAudio>();
        }

        private async void ItemCoolDown()
        {
            _fruits.SetActive(false);
            _collider.enabled = false;
            await UniTask.Delay(_coolDownTime);
            _fruits.SetActive(true);
            _collider.enabled = true;
        }

        public void PlayAudio()
        {
            if (_treeSFX == null)
            {
                return;
            }
            _treeSFX.PlayAudio();
        }
    }
}

