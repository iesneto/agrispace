using Agrispace.Events;
using Coimbra.Services;
using Coimbra.Services.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Quests
{
    public class TriggerObjectiveOnValue : MonoBehaviour
    {
        [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
        [SerializeField] private int _triggerOnValue;

        private int _currentObjectiveValue;
        private bool _isActive = true;
        private EventHandle _eventHandle;

        private void Start()
        {
            _eventHandle = UpdateQuestObjectiveValueEvent.AddListener(HandleUpdateQuestObjectiveValueEvent);
            
        }

        private void OnDestroy()
        {
            ServiceLocator.GetChecked<IEventService>().RemoveListener(_eventHandle);
            
        }

        private void HandleUpdateQuestObjectiveValueEvent(ref EventContext context, in UpdateQuestObjectiveValueEvent questEvent)
        {
            
            if(!_isActive || questEvent.Objective.QuestTarget != _objective.QuestTarget || questEvent.Objective.ObjectiveNumber != _objective.ObjectiveNumber)
            {
                return;
            }

            UpdateObjectiveValue(questEvent.Value);
        }

        private void UpdateObjectiveValue(int value)
        {
            _currentObjectiveValue += value;

            if(_currentObjectiveValue < _triggerOnValue) 
            {
                return;
            }

            _objective.Invoke();

            _isActive = false;
        }
    }
}

