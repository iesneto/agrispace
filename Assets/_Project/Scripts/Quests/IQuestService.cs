using Agrispace.Quests;
using Coimbra.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Quests
{
    [RequiredService]
    public interface IQuestService : IService
    {
        public void StartQuest(Quest quest);
        public void UpdateObjectiveStatus(Quest quest, int objectiveNumber, ObjectiveStatus status);
        public ObjectiveStatus GetObjectiveStatus(Quest quest, int objectiveNumber);

    }
}

