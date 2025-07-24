using Core.Scripts.LevelController;
using Core.Scripts.UI;
using Core.Scripts.UI.DiamondCounter;
using Core.Scripts.UI.LevelPoint__Door_Interaction_;
using UnityEngine;
using Zenject;

namespace Core.Scripts.MonoInstallers
{
    public class UIViewSceneInstaller : MonoInstaller
    {
        #region Fields

        [SerializeField] private NextLevelPortal _nextLevelPortal;

        #endregion
        public override void InstallBindings()
        {
            BindingDiamondCount();
            BindingTooltipRepository();
            InjectLevelPortal();
        }

        private void InjectLevelPortal()
        {
            Container.Bind<ILevelTransitionContact>().To<NextLevelPortal>().FromInstance(_nextLevelPortal);
        }
        
        private void BindingTooltipRepository()
        {
            Container.Bind<TooltipsVisibilityControllerViewModel>().FromNew().AsCached().NonLazy();
            Container.Bind<ITooltipDataRepository>().To<TooltipsVisibilityControllerViewModel>().AsCached();
        }
        
        private void BindingDiamondCount()
        {
            Container.Bind<DiamondCountViewModel>().FromNew().AsCached().NonLazy();
            Container.Bind<IDiamondCounter>().To<DiamondCountViewModel>().AsCached();
        }
    }
}

