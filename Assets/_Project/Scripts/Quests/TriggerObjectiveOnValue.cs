using Agrispace.Events;
using Coimbra.Services;
using Coimbra.Services.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agrispace.Core;

namespace Agrispace.Quests
{
    public class TriggerObjectiveOnValue : MonoBehaviour
    {
        [SerializeField] private ObjectiveTrigger _objective = new ObjectiveTrigger();
        [SerializeField] private int _triggerOnValue;
        [SerializeField] private bool _tryToGrabItem;
        [SerializeField] private GrabItem _grabItem;

        private int _currentObjectiveValue;
        private bool _isActive = true;
        private EventHandle _eventHandle;
        private PlayerGrabber _playerGrabber;

        private void Start()
        {
            _eventHandle = UpdateQuestObjectiveValueEvent.AddListener(HandleUpdateQuestObjectiveValueEvent);
            _playerGrabber = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerGrabber>();
        }

        private void OnDestroy()
        {
            ServiceLocator.GetChecked<IEventService>().RemoveListener(_eventHandle);
            
        }

        private void HandleUpdateQuestObjectiveValueEvent(ref EventContext context, in UpdateQuestObjectiveValueEvent questEvent)
        {
            if(_tryToGrabItem && _playerGrabber != null && _playerGrabber.IsGrabbing) 
            {
                return;
            }

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

            _playerGrabber.RemoveGrabbedItem();

            if (_grabItem != null)
            {
                _playerGrabber.TryToGrabItem(_grabItem);
            }

            _objective.Invoke();

            _isActive = false;
        }
    }
}

