using UnityEngine;

namespace GameGuruCase.Project1.Config
{
    /// <summary>
    /// Stores configuration for the grid system, such as grid count, scale, spacing, origin, and the prefab to instantiate.
    /// </summary>
    [CreateAssetMenu(fileName = "GridSystemProperties", menuName = "GridSystem/GridSystemProperties", order = 0)]
    public class GridSystemConfig : ScriptableObject
    {
        public Vector2Int gridCount;
        public Vector3 originPosition;
        public float gridScale;
        public Vector2 spaceBetweenGrids;
        public GameObject prefab;
    }
}