using System.Collections.Generic;


namespace Agrispace.Quests
{
    public class QuestStatus
    {
        public Quest QuestData;
        public Dictionary<int, ObjectiveStatus> ObjectiveStatuses;

        public QuestStatus(Quest questData)
        {
            this.QuestData = questData;
            ObjectiveStatuses = new Dictionary<int, ObjectiveStatus>();

            for (int i = 0; i < questData.Objectives.Count; i++)
            {
                QuestObjective objectiveData = questData.Objectives[i];
                ObjectiveStatuses[i] = objectiveData.InitialStatus;
            }
        }

        public ObjectiveStatus Status
        {
            get
            {
                for (int i = 0; i < QuestData.Objectives.Count; i++)
                {
                    QuestObjective objectiveData = QuestData.Objectives[i];
                    if (objectiveData.IsOptional)
                    {
                        continue;
                    }

                    ObjectiveStatus objectiveStatus = ObjectiveStatuses[i];
                    if (objectiveStatus == ObjectiveStatus.Failed)
                    {
                        return ObjectiveStatus.Failed;
                    }
                    else if (objectiveStatus != ObjectiveStatus.Complete)
                    {
                        return ObjectiveStatus.Open;
                    }
                }

                return ObjectiveStatus.Complete;
            }
        }

        public override string ToString()
        {
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

            for (int i = 0; i < QuestData.Objectives.Count; i++)
            {
                QuestObjective objectiveData = QuestData.Objectives[i];
                ObjectiveStatus objectiveStatus = ObjectiveStatuses[i];

                if (objectiveData.IsVisible == false && objectiveStatus == ObjectiveStatus.Open)
                {
                    continue;
                }

                if (objectiveData.IsOptional)
                {
                    stringBuilder.AppendFormat($"{objectiveData.Name} (Optional) - {objectiveStatus.ToString()}\n");
                }
                else
                {
                    stringBuilder.AppendFormat($"{objectiveData.Name} - {objectiveStatus.ToString()}\n");
                }

            }

            stringBuilder.AppendLine();
            stringBuilder.AppendFormat($"Status: {this.Status.ToString()}");

            return stringBuilder.ToString();
        }
    }

}
