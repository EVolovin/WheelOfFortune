using Zenject;
using WheelOfFortune.Game;
using WheelOfFortune.Sound;
using WheelOfFortune.UI;

namespace WheelOfFortune
{
    public class InjectionInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            // Configure the Zenject bindings
            
            Container.Bind<IApp>().To<App>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IGameManager>().To<GameManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<ISoundManager>().To<SoundManager>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IUIManager>().To<UIManager>().FromComponentInHierarchy().AsSingle();
        }
    }
}
