using System;
using DG.Tweening;
using UnityEngine;

public class PlatformHandler : MonoBehaviour
{
    private RingBufferPool<Platform> _platformRingBuffer;
    private RingBufferPool<Material> _materialRingBuffer;
    private Platform _currentPlatform;
    private Platform _lastPlacedPlatform;
    private LevelConfig _currentLevelConfig;
    private bool _lastIsLeft;
    private void Awake()
    {
        _platformRingBuffer = new RingBufferPool<Platform>(
            10, 
            () =>
            {
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Platform platform = obj.AddComponent<Platform>();
                obj.SetActive(false);
                return platform;
            });
        
    }

    private void OnEnable()
    {
        GameEventBus.PlacePlatformAction += PlacePlatform;
    }

    private void OnDisable()
    {
        GameEventBus.PlacePlatformAction -= PlacePlatform;
    }

    public void InitializeLevel(LevelConfig config)
    {
        _currentLevelConfig = config;

        Material[] materials = new Material[_currentLevelConfig.platformConfig.platformColors.Count];
        for (int i = 0; i < _currentLevelConfig.platformConfig.platformColors.Count; i++)
        {
            materials[i] = _currentLevelConfig.platformConfig.platformColors[i];
        }
        
        _materialRingBuffer = new RingBufferPool<Material>(
            materials.Length,
            pool: materials);
        
        SetFirstPlatform();
        SetFinishPlatform();
    }

    public void SendNewPlatform()
    {
        Platform platform = _platformRingBuffer.GetNext();
        platform.gameObject.SetActive(true);
        
        Vector3 pos = _lastPlacedPlatform? _lastPlacedPlatform.transform.position : Vector3.zero;
        pos.x = _lastIsLeft ?  -5 : 5;
        pos.z += _currentLevelConfig.platformConfig.firstPlatformScale.z;

        platform.transform.position = pos;

        Vector3 targetScale = _currentLevelConfig.platformConfig.firstPlatformScale;
        targetScale.x = _lastPlacedPlatform
            ? MeshHelper.GetMeshXSize(_lastPlacedPlatform.GetComponent<MeshFilter>().mesh)
            : _currentLevelConfig.platformConfig.firstPlatformScale.x;
        
        MeshHelper.ScaleMeshToDimensions(
            platform.GetComponent<MeshFilter>().mesh
            ,targetScale);
        
        platform.meshRenderer.material = _materialRingBuffer.GetNext();
        platform.transform.DOMoveX(_lastIsLeft ? 5 : -5, _currentLevelConfig.CalculatePlatformFlowSpeed()).SetEase(Ease.Linear);
        
        _currentPlatform = platform;

    }

    private void SetFirstPlatform()
    {
        Platform platform = _platformRingBuffer.GetNext();
        platform.gameObject.SetActive(true);
        platform.transform.position = Vector3.zero;
        
        MeshHelper.ScaleMeshToDimensions(
            platform.GetComponent<MeshFilter>().mesh
            ,_currentLevelConfig.platformConfig.firstPlatformScale);
        
        platform.meshRenderer.material = _materialRingBuffer.GetNext();
        _lastPlacedPlatform = platform;
    }

    private void SetFinishPlatform()
    {
        
    }

    private void PlacePlatform()
    {
        _currentPlatform?.transform.DOKill();

        if (_currentPlatform && _lastPlacedPlatform)
        {
            CutPlatformResult result = CutEngine.CutPlatform(
                _lastPlacedPlatform,
                _currentPlatform,
                _currentLevelConfig.platformConfig.minXScaleForPlatform,
                _currentLevelConfig.platformConfig.firstPlatformScale
            );

            if (!result.IsSuccessful)
            {
                GameEventBus.RaisePlatformPlacedUnsuccessfully(result);
                return;
            }
            
            GameEventBus.RaisePlatformPlacedSuccessfully(result);
        }

        _lastPlacedPlatform = _currentPlatform;
        _lastIsLeft = !_lastIsLeft;
        SendNewPlatform();
    }
    
    
}
