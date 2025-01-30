using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agrispace.Events;

namespace Agrispace.Quests
{
    public class UpdateObjectiveValue : MonoBehaviour
    {
        [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
        [SerializeField] private int _value;
        [SerializeField] private LayerMask _playerLayer;

        private void OnTriggerEnter(Collider other)
        {
            if ((_playerLayer & 1 << other.gameObject.layer) != 0)
            {
                new UpdateQuestObjectiveValueEvent(_objective, _value).Invoke(this);
            }
        }
    }
}

