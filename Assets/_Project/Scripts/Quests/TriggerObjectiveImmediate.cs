using Agrispace.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectiveImmediate : MonoBehaviour
{
    [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
    [SerializeField] private LayerMask _playerLayer;

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer & 1 << other.gameObject.layer) != 0)
        {
            _objective.Invoke();
        }
    }
}
