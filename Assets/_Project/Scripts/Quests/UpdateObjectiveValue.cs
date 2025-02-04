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
        [SerializeField] private IFeedback _feedback;

        private void Start()
        {
            _feedback = GetComponent<IFeedback>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if ((_playerLayer & 1 << other.gameObject.layer) != 0)
            {
                new UpdateQuestObjectiveValueEvent(_objective, _value).Invoke(this);
                RunFeedback();
            }
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
}

