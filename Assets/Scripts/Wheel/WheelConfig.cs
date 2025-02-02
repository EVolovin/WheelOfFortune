using System;
using System.Collections.Generic;
using UnityEngine;

namespace WheelOfFortune.Game
{
    [CreateAssetMenu(fileName = "WheelConfig", menuName = "Configs/WheelConfig", order = 0)]
    public class WheelConfig : ScriptableObject
    {
        [SerializeField] long _rangeMin;
        [SerializeField] long _rangeMax;

        public long RangeMin => _rangeMin;
        public long RangeMax => _rangeMax;
    }
}
