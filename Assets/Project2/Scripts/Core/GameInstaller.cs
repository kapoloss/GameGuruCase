using UnityEngine;
using Zenject;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Zenject installer to bind main scene MonoBehaviours and classes.
    /// </summary>
    public class GameInstaller : MonoInstaller
    {
        [Header("Scene MonoBehaviours")]
        [SerializeField] private GameController gameController;
        [SerializeField] private PlatformHandler platformHandler;
        [SerializeField] private LevelHandler levelHandler;
        [SerializeField] private PlayerController playerController;
        [SerializeField] private CameraHandler cameraHandler;
        [SerializeField] private InputHandler inputHandler;

        public override void InstallBindings()
        {
            Container.Bind<GameController>().FromInstance(gameController).AsSingle();
            Container.Bind<PlatformHandler>().FromInstance(platformHandler).AsSingle();
            Container.Bind<LevelHandler>().FromInstance(levelHandler).AsSingle();
            Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
            Container.Bind<CameraHandler>().FromInstance(cameraHandler).AsSingle();
            Container.Bind<InputHandler>().FromInstance(inputHandler).AsSingle();
            Container.Bind<GameStateMachine.GameStateMachine>().AsSingle();
        }
    }
}