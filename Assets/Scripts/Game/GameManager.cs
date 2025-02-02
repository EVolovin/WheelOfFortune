using System;
using UnityEngine;
using WheelOfFortune.Sound;
using Zenject;
using WheelOfFortune.UI;
using WheelOfFortune.Utilities;

namespace WheelOfFortune.Game
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [SerializeField] WheelConfig _wheelConfig;
        [SerializeField] WheelController _wheelPrefab;
        
        [Inject] IUIManager _uiManager;
        [Inject] ISoundManager _soundManager;
        
        IWheelController _wheelController;
        User _currentUser;
        
        public bool GameIsRunning { get; private set; }
        public WheelConfig WheelConfig => _wheelConfig;
        
        
        void Awake()
        {
            if (_wheelConfig == null)
                throw new ArgumentNullException("Wheel config is null");
            if (_wheelPrefab == null)
                throw new ArgumentNullException("Wheel prefab is null");
        }
        
        public void StartGame()
        {
            if (GameIsRunning)
                return;
            
            _uiManager.InitGameUI();
            
            _wheelController = Instantiate<WheelController>(_wheelPrefab, _uiManager.GameUIContainer);
            
            _wheelController.SpinStart += OnWheelSpinStart;
            _wheelController.SpinComplete += OnWheelSpinComplete;
            
            _wheelController.Initialize(_wheelConfig);
            
            LoadUserData();
            
            GameIsRunning = true;
        }
        
        void LoadUserData()
        {
            _currentUser = new User();
            var userData = new UserData();
            
            if (SaveDataManager.LoadJsonData(userData))
            {
                _currentUser.Balance = userData.balance;
                _uiManager.SetScore(_currentUser.Balance);
            }
        }
        
        void OnWheelSpinStart()
        {
            var clip = _soundManager.Sounds.WheelSpin;
            _soundManager.PlaySound(clip);
        }
        
        void OnWheelSpinComplete(long winValue)
        {
            // Being able to start a new spin while the win text is still counting is by design!
            _currentUser.Balance += winValue;
            
            _uiManager.UpdateScore(winValue, _currentUser.Balance);
            SaveDataManager.SaveJsonData(new UserData(_currentUser.Balance));
        }
    }
}
