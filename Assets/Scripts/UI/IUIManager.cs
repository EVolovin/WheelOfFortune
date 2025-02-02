using System;
using UnityEngine;

namespace WheelOfFortune.UI
{
    public interface IUIManager
    {
        Transform GameUIContainer { get; }
        void InitGameUI();
        void SetScore(long value);
        void UpdateScore(long winValue, long totalWinValue);
    }
}
