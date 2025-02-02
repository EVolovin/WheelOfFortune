using System;
using UnityEngine;
using Zenject;
using WheelOfFortune.Game;
using WheelOfFortune.Sound;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.UI
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [Inject] IGameManager _gameManager;
        [Inject] ISoundManager _soundManager;
        
        CanvasGroup _mainCanvasGroup;
        GameUI _gameUI;
        
        public Transform GameUIContainer => _mainCanvasGroup.transform;
        
        
        public void InitGameUI()
        {
            var uiContainerGO = GameObject.FindGameObjectWithTag(StringUtil.UI_CONTAINER_TAG);
            if (uiContainerGO == null)
                throw new UnityException($"GameObject with tag {StringUtil.UI_CONTAINER_TAG} not found.");
            
            _mainCanvasGroup = uiContainerGO.GetComponent<CanvasGroup>();
            _gameUI = uiContainerGO.GetComponent<GameUI>();
            _gameUI.SoundManager = _soundManager;
        }
        
        public void SetScore(long value)
        {
            _gameUI.SetScore(value);
        }
        
        public void UpdateScore(long winValue, long totalWinValue)
        {
            // Being able to start a new spin while the win text is still counting is by design!
            _gameUI.UpdateScore(winValue, totalWinValue);
        }
    }
}
