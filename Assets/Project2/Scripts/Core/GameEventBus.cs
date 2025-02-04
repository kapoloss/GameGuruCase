using System;
using GameGuruCase.Project2.Config;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Central event bus for communicating gameplay events such as platform placement, level start/fail/complete.
    /// </summary>
    public static class GameEventBus
    {
        public static event Action<LevelConfig> LevelStarted;
        public static event Action<PlatformRouteArgs> PlatformOnRoute;
        public static event Action PlacePlatformAction;
        public static event Action<CutPlatformResult> PlatformPlacedSuccessfully;
        public static event Action PlatformPlacedUnsuccessfully;
        public static event Action LevelFailed;
        public static event Action LevelCompleted;
        public static event Action OnRestartClicked;
        public static event Action OnNextLevelClicked;
        
        public static void RaiseLevelStarted(LevelConfig level)
        {
            LevelStarted?.Invoke(level);
        }

        public static void RaisePlatformOnRoute(PlatformRouteArgs args)
        {
            PlatformOnRoute?.Invoke(args);
        }
        public static void RaisePlacePlatformAction()
        {
            PlacePlatformAction?.Invoke();
        }

        public static void RaisePlatformPlacedSuccessfully(CutPlatformResult result)
        {
            PlatformPlacedSuccessfully?.Invoke(result);
        }

        public static void RaisePlatformPlacedUnsuccessfully()
        {
            PlatformPlacedUnsuccessfully?.Invoke();
        }

        public static void RaiseLevelFailed()
        {
            LevelFailed?.Invoke();
        }

        public static void RaiseLevelCompleted()
        {
            LevelCompleted?.Invoke();
        }

        public static void RaiseOnRestartClicked()
        {
            OnRestartClicked?.Invoke();
        }

        public static void RaiseOnNextLevelClicked()
        {
            OnNextLevelClicked?.Invoke();
        }
    }
}