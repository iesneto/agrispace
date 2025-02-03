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

    private IQuestService _questManager;

    private void Start()
    {
        _questManager = ServiceLocator.GetChecked<IQuestService>();
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
        if (_questManager == null)
        {
            return;
        }

        if (_questManager.GetObjectiveStatus(_objective.QuestTarget, _objective.ObjectiveNumber - 1) != ObjectiveStatus.Complete)
        {
            return;
        }

        _objective.Invoke();
    }


}
