using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioConfig audioConfig;
    private int _perfectCutCount;
    private float _currentPitch;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioConfig.noteClip;
    }

    private void OnEnable()
    {
        GameEventBus.PlatformPlacedSuccessfully += CheckPerfectCut;
        GameEventBus.PlatformPlacedUnsuccessfully += ResetPitch;
    }

    private void OnDisable()
    {
        GameEventBus.PlatformPlacedSuccessfully -= CheckPerfectCut;
        GameEventBus.PlatformPlacedUnsuccessfully -= ResetPitch;
    }

    private void CheckPerfectCut(CutPlatformResult cutPlatformResult)
    {
        float diff = Mathf.Abs(cutPlatformResult.AccuracyRate);

        if (diff <= audioConfig.tolerance)
        {
            _perfectCutCount++;
            
            _currentPitch = audioConfig.basePitch + audioConfig.pitchIncrement * _perfectCutCount;
            _currentPitch = Mathf.Min(_currentPitch, audioConfig.maxPitch);
            
            PlayNoteWithPitch(_currentPitch);
        }
        else
        {
            _perfectCutCount = 0;
            ResetPitch(null);
        }
    }
    
    private void PlayNoteWithPitch(float pitchValue)
    {
        audioSource.pitch = pitchValue;
        audioSource.PlayOneShot(audioConfig.noteClip);
    }
    
    private void ResetPitch(CutPlatformResult cutPlatformResult)
    {
        _currentPitch = audioConfig.basePitch;
        audioSource.pitch = _currentPitch;
    }
}
