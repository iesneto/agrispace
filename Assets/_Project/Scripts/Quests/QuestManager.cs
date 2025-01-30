using UnityEngine;
using TMPro;

namespace Agrispace.Quests
{
    public class QuestManager : MonoBehaviour
    {
        [SerializeField] private Quest _startingQuest = null;
        [SerializeField] private TextMeshProUGUI _objectiveSummary = null;

        private QuestStatus _activeQuestStatus;

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
        }

        private void Start()
        {
            if (_startingQuest == null)
            {
                return;
            }

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
    }

}

