using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Quests
{
    [CreateAssetMenu(fileName = "New Quest", menuName = "Quest", order = 100)]
    public class Quest : ScriptableObject
    {
        public string QuestName;

        public List<QuestObjective> Objectives;
    }
}
