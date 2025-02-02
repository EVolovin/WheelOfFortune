using System;
using UnityEngine;

namespace WheelOfFortune.Sound
{
    [CreateAssetMenu(fileName = "Sounds Config", menuName = "Configs/SoundsConfig", order = 1)]
    public class SoundsConfig : ScriptableObject
    {
        // The config asset that contains the sound clips.
        
        [SerializeField] AudioClip _coinsLoopClip;
        [SerializeField] AudioClip _trailsClip;
        [SerializeField] AudioClip _wheelSpinClip;
        [SerializeField] AudioClip _winClip;
        
        
        public AudioClip CoinsLoop => _coinsLoopClip;
        public AudioClip Trails => _trailsClip;
        public AudioClip WheelSpin => _wheelSpinClip;
        public AudioClip Win => _winClip;
    }
}
