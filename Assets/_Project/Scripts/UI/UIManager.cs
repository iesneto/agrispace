using Coimbra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agrispace.Core;
using Coimbra.Services;
using Sensorama.Core;
using TMPro;
using Gamob;

namespace Agrispace.UI
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Menu UI")]
        [SerializeField] private CanvasGroup _menuCanvasGroup;
        [SerializeField] private Button _startGameButton;

        [Header("Game UI")]
        [SerializeField] private CanvasGroup _gameCanvasGroup;
        [SerializeField] private CanvasGroup _questSummaryCanvasGroup;            
        [SerializeField] private TextMeshProUGUI _questSummaryText;
        [SerializeField] private Button _questsButton;
        [SerializeField] private Button _closeQuestsButton;
        [SerializeField] private CanvasGroup _quitGameCanvasGroup;
        [SerializeField] private Button _quitGameButton;
        [SerializeField] private Button _quitGameYesButton;
        [SerializeField] private Button _quitGameNoButton;
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private CanvasGroup _finishGameCanvasGroup;
        [SerializeField] private Button _finishGameOkButton;

        [Header("Audio")]
        [SerializeField] private UIAudios _buttonAudio;


        public void UpdateQuestSummary(string text)
        {
            _questSummaryText.text = text;
        }

        public void UpdateCoinText(string value)
        {
            _coinText.text = value;
        }

        public void FinishGame()
        {
            EnableCanvasGroup(_finishGameCanvasGroup);
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            EnableMainMenuCanvasGroup();
            _startGameButton.onClick.AddListener(HandleStartGameButton);
            _questsButton.onClick.AddListener(HandleQuestsButton);
            _closeQuestsButton.onClick.AddListener(HandleCloseQuestsButton);
            _quitGameButton.onClick.AddListener(HandleQuitGameButton);
            _quitGameYesButton.onClick.AddListener(HandleQuitGameYesButton);
            _quitGameNoButton.onClick.AddListener(HandleQuitGameNoButton);
            _finishGameOkButton.onClick.AddListener(HandleQuitGameYesButton);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
            _questsButton.onClick.RemoveAllListeners();
            _closeQuestsButton.onClick.RemoveAllListeners();
            _quitGameYesButton.onClick.RemoveAllListeners();
            _quitGameNoButton.onClick.RemoveAllListeners();
            _finishGameOkButton.onClick.RemoveAllListeners();
        }
        

        private void HandleStartGameButton()
        {
            PlayButtonClick();
            GameManager.Instance.StartGame();
            EnableGameCanvasGroup();            
        }

        private void PlayButtonClick()
        {
            AudioSystem.Instance.PlayOneShotClip(_buttonAudio);
        }
        
        private void EnableMainMenuCanvasGroup()
        {
            DisableCanvasGroup(_gameCanvasGroup);
            EnableCanvasGroup(_menuCanvasGroup);
        }


        private void EnableGameCanvasGroup()
        {
            DisableCanvasGroup(_menuCanvasGroup);
            EnableCanvasGroup(_gameCanvasGroup);
            DisableCanvasGroup(_questSummaryCanvasGroup);
            DisableCanvasGroup(_finishGameCanvasGroup);
            DisableCanvasGroup(_quitGameCanvasGroup);
        }

        private void DisableCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.CanvasGroupEnable(false);
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.CanvasGroupEnable(true);
        }

        private void HandleQuestsButton()
        {
            _questSummaryCanvasGroup.CanvasGroupEnable(true);
            PlayButtonClick();
        }

        private void HandleCloseQuestsButton()
        {
            _questSummaryCanvasGroup.CanvasGroupEnable(false);
            PlayButtonClick();
        }

        private void HandleQuitGameButton()
        {
            EnableCanvasGroup(_quitGameCanvasGroup);
            PlayButtonClick();
        }

        private void HandleQuitGameYesButton()
        {
            PlayButtonClick();
            GameManager.Instance.ExitGame();
            EnableMainMenuCanvasGroup();
        }

        private void HandleQuitGameNoButton()
        {
            DisableCanvasGroup(_quitGameCanvasGroup);
            PlayButtonClick();
        }


    }
}

