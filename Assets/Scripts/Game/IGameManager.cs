using System;

namespace WheelOfFortune.Game
{
    public interface IGameManager
    {
        bool GameIsRunning { get; }
        WheelConfig WheelConfig { get; }
        void StartGame();
    }
}
