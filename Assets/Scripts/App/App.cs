using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using WheelOfFortune.Game;
using WheelOfFortune.Utilities;

namespace WheelOfFortune
{
    public class App : MonoBehaviour, IApp
    {
        [Inject] IGameManager _gameManager;
        
        
        void Awake()
        {
            // should unsubscribe first if Domain reloading is disabled
            Application.lowMemory += () => Resources.UnloadUnusedAssets();
            
            SceneManager.sceneLoaded += (scene, mode) =>
            {
                if (scene.name == StringUtil.SCENE_MAIN_NAME)
                    _gameManager.StartGame();
            };
            
            DontDestroyOnLoad(gameObject);
        }
        
        public void InitGame()
        {
            if (_gameManager.GameIsRunning)
                return;
            
            SceneManager.LoadScene(StringUtil.SCENE_MAIN_NAME);
        }
    }
}
