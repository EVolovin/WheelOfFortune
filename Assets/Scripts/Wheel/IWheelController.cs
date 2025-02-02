using System;

namespace WheelOfFortune.Game
{
    public interface IWheelController
    {
        event Action SpinStart;
        event Action<long> SpinComplete;
        void Initialize(WheelConfig config);
    }
}
