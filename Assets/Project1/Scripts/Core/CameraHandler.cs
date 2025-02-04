using System.Collections;
using GameGuruCase.Project1.Config;
using UnityEngine;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// Adjusts the orthographic camera size based on a given GridSystemConfig, ensuring the entire grid is visible.
    /// </summary>
    public class CameraHandler : MonoBehaviour
    {
        private Camera _cam;

        private void Awake()
        {
            _cam = GetComponent<Camera>();
        }

        /// <summary>
        /// Dynamically adjusts the camera's orthographic size to fit the grid.
        /// </summary>
        public IEnumerator AdjustCamera(GridSystemConfig config)
        {
            yield return new WaitForEndOfFrame();

            float gridWidth = config.gridCount.x * (config.gridScale + config.spaceBetweenGrids.x);
            float gridHeight = config.gridCount.y * (config.gridScale + config.spaceBetweenGrids.y);
            float aspectRatio = (float)Screen.safeArea.width / Screen.safeArea.height;

            float verticalSize = gridHeight / 2f;
            float horizontalSize = (gridWidth / 2f) / aspectRatio;

            _cam.orthographicSize = Mathf.Max(verticalSize, horizontalSize);

            Vector3 gridCenter = config.originPosition;
            gridCenter.z = -10f;
            _cam.transform.position = gridCenter;
        }
    }
}