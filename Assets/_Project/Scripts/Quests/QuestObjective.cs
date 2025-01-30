using System;

namespace Agrispace.Quests
{
    [Serializable]
    public enum ObjectiveStatus
    {
        Open,
        Complete,
        Failed
    }

    [Serializable]
    public class QuestObjective
    {
        public string Name = "New Objective";

        public bool IsOptional = false;

        public bool IsVisible = true;

        public ObjectiveStatus InitialStatus = ObjectiveStatus.Open;
    }
}

