using System.Collections.Generic;
using UnityEngine;
using Agrispace.UI;
using Agrispace.Core;
using Gamob;

namespace Agrispace.Quests
{
    public class QuestManager : Singleton<QuestManager>
    {
        [SerializeField] private List<Quest> _quests = new List<Quest>();
        [SerializeField] private SFXAudios _completeQuestAudio;
        [SerializeField] private SFXAudios _completeObjectiveAudio;

        private List<QuestStatus> _questStatuses = new List<QuestStatus> ();

        public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, ObjectiveStatus status)
        {                                    
            foreach (QuestStatus questStatus in _questStatuses)
            {
                if (questStatus.QuestData.name != quest.name)
                {
                    continue;
                }

                if (questStatus.Status == ObjectiveStatus.Complete)
                {
                    continue;
                }

                if (status == ObjectiveStatus.Complete)
                {
                    AudioSystem.Instance.PlayOneShotClip(_completeObjectiveAudio);
                }

                questStatus.ObjectiveStatuses[objectiveNumber] = status;

                if(questStatus.Status == ObjectiveStatus.Complete)
                {
                    TriggerCompleteQuest(questStatus);
                }
            }

            UpdateObjectiveSummaryText();
        }

        public ObjectiveStatus GetObjectiveStatus(Quest quest, int objectiveNumber)
        {
            if (objectiveNumber < 0)
            {
                Debug.LogError($"Tried to access an invalid objective index");
                return ObjectiveStatus.Open;
            }

            foreach(QuestStatus status in _questStatuses)
            {
                if(status.QuestData.name != quest.name)
                {
                    continue;
                }
                
                return status.ObjectiveStatuses[objectiveNumber];
            }
            return ObjectiveStatus.Open;
        }

        public void InitializeQuestStatuses()
        {
            _questStatuses.Clear();
            foreach (Quest quest in _quests)
            {
                QuestStatus questStatus = new QuestStatus(quest);
                _questStatuses.Add(questStatus);
            }
            UpdateObjectiveSummaryText();
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            InitializeQuestStatuses();
        }

        private void UpdateObjectiveSummaryText()
        {
            System.Text.StringBuilder label = new System.Text.StringBuilder();

            foreach (QuestStatus questStatus in _questStatuses)
            {
                label.AppendFormat($"Quest: {questStatus.QuestData.QuestName}\n");
                label.AppendFormat(questStatus.ToString());
                label.AppendLine();                
            }

            UIManager.Instance.UpdateQuestSummary(label.ToString());
        }
        
        private void TriggerCompleteQuest(QuestStatus questStatus)
        {
            AudioSystem.Instance.PlayOneShotClip(_completeQuestAudio);
            int coinReward = questStatus.QuestData.Reward;
            GameManager.Instance.AddCoins(coinReward);
            GameManager.Instance.CompleteQuest();
        }
    }

}

