using Agrispace.Core;
using Agrispace.Events;
using Agrispace.Quests;
using Coimbra.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TriggerObjectiveOnSequence : MonoBehaviour
{
    [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
    [SerializeField] private LayerMask _playerLayer;
    [SerializeField] private IFeedback _feedback;
    [SerializeField] private bool _tryToGrabItem;
    [SerializeField] private GrabItem _grabItem;

    private bool _isActive = true;

    private PlayerGrabber _playerGrabber;

    private void Start()
    {
        _feedback = GetComponent<IFeedback>();
        _playerGrabber = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGrabber>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((_playerLayer & 1 << other.gameObject.layer) != 0)
        {
            TryToApplyObjectiveStatus();
        }
    }

    private void TryToApplyObjectiveStatus()
    {

        if (QuestManager.Instance == null)
        {
            return;
        }

        if (!_isActive || QuestManager.Instance.GetObjectiveStatus(_objective.QuestTarget, _objective.ObjectiveNumber - 1) != ObjectiveStatus.Complete)
        {
            return;
        }
        _playerGrabber.RemoveGrabbedItem();

        if (_grabItem != null)
        {
            _playerGrabber.TryToGrabItem(_grabItem);
        }

        _objective.Invoke();
        RunFeedback();
        _isActive = false;
    }

    private void RunFeedback()
    {
        if (_feedback == null)
        {
            return;
        }
        _feedback.Run();
    }


}
