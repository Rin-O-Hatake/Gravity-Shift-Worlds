using Core.Scripts.UI.DiamondCounter;
using UnityEngine;
using Zenject;

namespace Core.Scripts.MonoInstallers
{
    public class UIViewSceneInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindingDiamondCountViewModel();
        }
        

        private void BindingDiamondCountViewModel()
        {
            Container.Bind<DiamondCountViewModel>().FromNew().AsCached().NonLazy();
            Container.Bind<IDiamondCounter>().To<DiamondCountViewModel>().AsCached();
        }
    }
}

