using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformConfig", menuName = "GridSystem/PlatformConfig", order = 0)]

public class PlatformConfig : ScriptableObject
{
    public Vector3 firstPlatformScale;
    public float minXScaleForPlatform;
    public List<Material> platformColors;
}