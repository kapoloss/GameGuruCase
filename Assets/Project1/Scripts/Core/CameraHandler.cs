using System;
using System.Collections;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    private Camera _cam;

    private void Awake()
    {
        _cam = GetComponent<Camera>();
    }

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
