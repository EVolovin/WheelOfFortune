using System;
using UnityEngine;

namespace WheelOfFortune.Game
{
    [Serializable]
    public class UserData : ISaveable
    {
        public UserData(long value = 0)
        {
            balance = value;
        }
        
        public long balance;
        
        
        public string ToJson() => JsonUtility.ToJson(this);
        public void LoadFromJson(string jsonString) => JsonUtility.FromJsonOverwrite(jsonString, this);
    }
    
    public class User
    {
        public long Balance { get; set; }
    }
}
