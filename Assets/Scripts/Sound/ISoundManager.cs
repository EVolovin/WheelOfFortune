using System;
using UnityEngine;

namespace WheelOfFortune.Sound
{
    public interface ISoundManager
    {
        SoundHandler PlaySound(AudioClip clip);
        SoundHandler PlaySound(AudioClip clip, Vector3 position);
        
        public SoundsConfig Sounds { get; }
    }
}
