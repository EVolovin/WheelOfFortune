using System;
using UnityEngine;

namespace WheelOfFortune.Game
{
    public interface ISaveable
    {
        string ToJson();
        void LoadFromJson(string jsonString);
    }
}
