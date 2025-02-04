using Coimbra.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Agrispace.Core
{
    [RequiredService]
    public interface IGameManager : IService
    {
        public void StartGame();
        public void AddCoins(int value);
        
    }
}

