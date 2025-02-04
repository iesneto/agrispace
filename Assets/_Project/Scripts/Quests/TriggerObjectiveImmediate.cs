using Agrispace.Core;
using Agrispace.Quests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectiveImmediate : MonoBehaviour
{
    [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private bool _tryToGrabItem;
    [SerializeField] private GrabItem _grabItem;

    private PlayerGrabber _playerGrabber;


    private void Start()
    {
        _playerGrabber = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGrabber>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer & 1 << other.gameObject.layer) != 0)
        {
            TryToTriggerObjective();
        }
    }

    private void TryToTriggerObjective()
    {
        if (_tryToGrabItem && _playerGrabber != null && _playerGrabber.IsGrabbing)
        {
            return;
        }

        _playerGrabber.RemoveGrabbedItem();

        if (_grabItem != null)
        {
            _playerGrabber.TryToGrabItem(_grabItem);
        }

        _objective.Invoke();
    }
}
