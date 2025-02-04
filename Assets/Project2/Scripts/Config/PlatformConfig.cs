using System.Collections.Generic;
using UnityEngine;

namespace  GameGuruCase.Project2.Config
{
    /// <summary>
    /// Holds general platform-related config such as scale, minimum overlap, and possible colors.
    /// </summary>
    [CreateAssetMenu(fileName = "PlatformConfig", menuName = "GridSystem/PlatformConfig", order = 0)]
    public class PlatformConfig : ScriptableObject
    {
        public Vector3 firstPlatformScale;
        public float minXScaleForPlatform;
        public List<Material> platformColors;
    }
}