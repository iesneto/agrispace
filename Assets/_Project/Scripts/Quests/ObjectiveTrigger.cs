using UnityEngine;

namespace Agrispace.Quests
{
    [System.Serializable]
    public class ObjectiveTrigger
    {
        public Quest QuestTarget;

        public ObjectiveStatus ObjectiveStatusToApply;

        public int ObjectiveNumber;

        public void Invoke()
        {
            QuestManager questManager = Transform.FindObjectOfType<QuestManager>();
            if (questManager == null)
            {
                return;
            }

            questManager.UpdateObjectiveStatus(QuestTarget, ObjectiveNumber, ObjectiveStatusToApply);
        }
    }
}


