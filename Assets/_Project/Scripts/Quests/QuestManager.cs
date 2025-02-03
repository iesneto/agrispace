using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Coimbra;

namespace Agrispace.Quests
{
    public class QuestManager : Actor, IQuestService
    {
        [SerializeField] private Quest _startingQuest = null;
        [SerializeField] private TextMeshProUGUI _objectiveSummary = null;
        [SerializeField] private List<Quest> _quests = new List<Quest>();

        private QuestStatus _activeQuestStatus;
        private List<QuestStatus> _questStatuses = new List<QuestStatus> ();

        public void StartQuest(Quest quest)
        {
            _activeQuestStatus = new QuestStatus(quest);
            UpdateObjectiveSummaryText();

            Debug.LogFormat($"Started quest {_activeQuestStatus.QuestData.QuestName}");
        }

        public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, ObjectiveStatus status)
        {
            if (_activeQuestStatus == null)
            {
                Debug.LogError("Tried to set an objective status, but no quest is active");
                return;
            }

            if (_activeQuestStatus.QuestData != quest)
            {
                Debug.LogWarningFormat($"Tried to set an objective status for quest {quest.QuestName}, but this is not the active quest. Ignoring");
                return;
            }

            _activeQuestStatus.ObjectiveStatuses[objectiveNumber] = status;
            UpdateObjectiveSummaryText();

            foreach (QuestStatus questStatus in _questStatuses)
            {
                if (questStatus.QuestData.name != quest.name)
                {
                    continue;
                }

                questStatus.ObjectiveStatuses[objectiveNumber] = status;
            }
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

        protected override void OnInitialize()
        {
            base.OnInitialize();
            DontDestroyOnLoad(this);

            if (_startingQuest == null)
            {
                return;
            }
            InitializeQuestStatuses();
            StartQuest(_startingQuest);
        }

        private void UpdateObjectiveSummaryText()
        {
            string label;

            if (_activeQuestStatus == null)
            {
                label = "No active quest.";
            }
            else
            {
                label = _activeQuestStatus.ToString();
            }

            _objectiveSummary.text = label;
        }

        private void InitializeQuestStatuses()
        {
            foreach (Quest quest in _quests)
            {
                QuestStatus questStatus = new QuestStatus(quest);
                _questStatuses.Add(questStatus);
            }
        }
    }

}

