using UnityEngine;
using Agrispace.Quests;
using Agrispace.UI;
using UnityEngine.SceneManagement;
using Gamob;
using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;


namespace Agrispace.Core
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private int _questsToFinishGame;
        [SerializeField] private int _gameSceneIndex;
        [SerializeField] private int _menuSceneIndex;
        [SerializeField] private SFXAudios _finishGameSFX;
        [SerializeField] private GameObject _player;

        private int _coins;
        private int _questsCompleted;


        public void StartGame()
        {
            QuestManager.Instance.InitializeQuestStatuses();
            _coins = 0;
            _questsCompleted = 0;
            UpdateUICoins();
            LoadScene(_gameSceneIndex);
        }

        public void ExitGame()
        {
            LoadScene(_menuSceneIndex);
        }

        public void CompleteQuest()
        {
            _questsCompleted++;

            if(_questsCompleted < _questsToFinishGame)
            {
                return;
            }

            FinishGame();
        }

        public void AddCoins(int value)
        {
            _coins += value;
            UpdateUICoins();
        }        

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }


        private async void LoadScene(int sceneIndex)
        {            
            await SceneManager.LoadSceneAsync(sceneIndex);
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        private void FinishGame()
        {
            UIManager.Instance.FinishGame();
            AudioSystem.Instance.PlayOneShotClip(_finishGameSFX);

            if( _player == null ) 
            {
                return;
            }

            PlayerInput playerInput = _player.GetComponent<PlayerInput>();
            playerInput.enabled = false;
        }

        private void UpdateUICoins()
        {
            UIManager.Instance.UpdateCoinText(_coins.ToString());
        }

    }
}

