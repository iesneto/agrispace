using Coimbra.Services.Events;
using Agrispace.Quests;

namespace Agrispace.Events
{
    public readonly partial struct UpdateQuestObjectiveValueEvent : IEvent
    {
        public readonly ObjectiveTrigger Objective;
        public readonly int Value;

        public UpdateQuestObjectiveValueEvent(ObjectiveTrigger objective, int value)
        {
            Objective = objective;
            Value = value;
        }

    }
    
}
