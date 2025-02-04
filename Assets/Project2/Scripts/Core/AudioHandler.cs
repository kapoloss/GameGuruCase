using GameGuruCase.Project2.Config;
using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Manages audio feedback for perfect or imperfect platform cuts.
    /// </summary>
    public class AudioHandler : MonoBehaviour
    {
        public AudioSource audioSource;
        public AudioConfig audioConfig;
        private int _perfectCutCount;
        private float _currentPitch;

        /// <summary>
        /// Retrieves AudioSource and loads AudioClip.
        /// </summary>
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = audioConfig.noteClip;
        }

        /// <summary>
        /// Subscribes to relevant game events.
        /// </summary>
        private void OnEnable()
        {
            GameEventBus.PlatformPlacedSuccessfully += CheckPerfectCut;
            GameEventBus.PlatformPlacedUnsuccessfully +=  ResetPitch;
            GameEventBus.OnRestartClicked += ResetPitch;
        }

        /// <summary>
        /// Unsubscribes from game events.
        /// </summary>
        private void OnDisable()
        {
            GameEventBus.PlatformPlacedSuccessfully -= CheckPerfectCut;
            GameEventBus.PlatformPlacedUnsuccessfully -= ResetPitch;
            GameEventBus.OnRestartClicked -= ResetPitch;

        }

        /// <summary>
        /// Checks if the cut is within tolerance, updates pitch accordingly.
        /// </summary>
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
                ResetPitch();
            }
        }

        /// <summary>
        /// Plays note using the current pitch.
        /// </summary>
        private void PlayNoteWithPitch(float pitchValue)
        {
            audioSource.pitch = pitchValue;
            audioSource.PlayOneShot(audioConfig.noteClip);
        }

        /// <summary>
        /// Resets pitch to the base value.
        /// </summary>
        private void ResetPitch()
        {
            _currentPitch = audioConfig.basePitch;
            audioSource.pitch = _currentPitch;
        }
    }
}