using Cinemachine;
using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Handles the active camera mode, switching between runner and win cameras.
    /// Moves the win camera along a dolly track.
    /// </summary>
    public class CameraHandler : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _runnerCam;
        [SerializeField] private CinemachineVirtualCamera _winCam;
        private CinemachineTrackedDolly _winCamDolly;

        private CameraType _currentCamera = CameraType.Runner;
        [SerializeField] private float dollySpeed = 3;

        private void Awake()
        {
            _winCamDolly = _winCam.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        private void OnEnable()
        {
            GameEventBus.LevelCompleted += SetWinCam;
            GameEventBus.OnNextLevelClicked += SetRunnerCam;
            GameEventBus.OnRestartClicked += SetRunnerCam;
        }

        private void OnDisable()
        {
            GameEventBus.LevelCompleted -= SetWinCam;
            GameEventBus.OnNextLevelClicked -= SetRunnerCam;
            GameEventBus.OnRestartClicked -= SetRunnerCam;
        }

        private void Update()
        {
            if (_currentCamera == CameraType.Win)
            {
                _winCamDolly.m_PathPosition += dollySpeed * Time.deltaTime;
            }
        }

        private void SetWinCam()
        {
            _currentCamera = CameraType.Win;
            _winCam.gameObject.SetActive(true);
            _runnerCam.gameObject.SetActive(false);
        }

        private void SetRunnerCam()
        {
            _currentCamera = CameraType.Runner;
            _winCam.gameObject.SetActive(false);
            _runnerCam.gameObject.SetActive(true);
        }
    }

    public enum CameraType
    {
        Runner,
        Win
    }
}