using Coimbra.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.UI
{
    [RequiredService]
    public interface IUIManager : IService
    {
        public void UpdateQuestSummary(string text);
    }
}

