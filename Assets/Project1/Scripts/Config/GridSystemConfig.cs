using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridSystemProperties", menuName = "GridSystem/GridSystemProperties", order = 0)]

public class GridSystemConfig : ScriptableObject
{
    public Vector2Int gridCount;
    
    public Vector3 originPosition;

    public float gridScale;

    public Vector2 spaceBetweenGrids;

    public GameObject prefab;
}