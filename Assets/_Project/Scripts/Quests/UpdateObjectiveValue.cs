using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agrispace.Events;
using Cysharp.Threading.Tasks;

namespace Agrispace.Quests
{
    public class UpdateObjectiveValue : MonoBehaviour
    {
        [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
        [SerializeField] private int _value;
        [SerializeField] private LayerMask _playerLayer;
        [SerializeField] private int _coolDownTime;

        private MeshRenderer _meshRenderer;
        private Collider _collider;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_playerLayer & 1 << other.gameObject.layer) != 0)
            {
                new UpdateQuestObjectiveValueEvent(_objective, _value).Invoke(this);
                ItemCoolDown();
            }
        }

        private async void ItemCoolDown()
        {
            _meshRenderer.enabled = false;
            _collider.enabled = false;
            await UniTask.Delay(_coolDownTime);
            _meshRenderer.enabled = true;
            _collider.enabled = true;
        }
    }
}

