using Coimbra;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Agrispace.Core;
using Coimbra.Services;
using Sensorama.Core;
using TMPro;

namespace Agrispace.UI
{
    public class UIManager : Singleton<UIManager>
    {

        [SerializeField] private CanvasGroup _menuCanvasGroup;
        [SerializeField] private CanvasGroup _gameCanvasGroup;
        
        [SerializeField] private Button _startGameButton;
        [SerializeField] private TextMeshProUGUI _questSummaryText;


        public void UpdateQuestSummary(string text)
        {
            Debug.Log(text);
            _questSummaryText.text = text;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            EnableMainMenuCanvasGroup();
            _startGameButton.onClick.AddListener(HandleStartGameButton);
        }

        private void OnDestroy()
        {
            _startGameButton.onClick.RemoveAllListeners();
        }
        

        private void HandleStartGameButton()
        {
            GameManager.Instance.StartGame();
            EnableGameCanvasGroup();
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
        }

        private void DisableCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.CanvasGroupEnable(false);
        }

        private void EnableCanvasGroup(CanvasGroup canvasGroup)
        {
            canvasGroup.CanvasGroupEnable(true);
        }

    }
}

