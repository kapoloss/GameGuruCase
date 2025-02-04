using GameGuruCase.Project1.Config;
using UnityEngine;
using Zenject;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// Zenject installer for binding scene objects (MonoBehaviours) and interfaces.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        [Header("Scene References")]
        [SerializeField] private GameController gameController;
        [SerializeField] private InputHandler inputHandler;
        [SerializeField] private CameraHandler cameraHandler;

        [Header("Grid Settings")]
        [SerializeField] private GridSystemConfig gridSystemConfig;
        [SerializeField] private Transform gridParent;

        public override void InstallBindings()
        {
            Container.Bind<GameController>().FromInstance(gameController).AsSingle();
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
            Container.Bind<CameraHandler>().FromInstance(cameraHandler).AsSingle();

            Container.BindInstance(gridSystemConfig).AsSingle();
            Container.BindInstance(gridParent).WithId("GridParent").AsSingle();

            Container.Bind<IWinStrategy>().To<ThreeNeighboursStrategy>().AsSingle();
        }
    }
}