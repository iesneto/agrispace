using Coimbra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Agrispace.Quests;
using Agrispace.UI;
using Coimbra.Services;
using UnityEngine.SceneManagement;

namespace Agrispace.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int _gameSceneIndex;
        [SerializeField] private int _menuSceneIndex;
        private int _coins;

        public void StartGame()
        {
            QuestManager.Instance.InitializeQuestStatuses();
            _coins = 0;
            LoadSceneAsync(_gameSceneIndex);
        }

        public void AddCoins(int value)
        {
            _coins += value;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        private void LoadSceneAsync(int sceneIndex)
        {            
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

