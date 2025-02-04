using UnityEngine;

namespace GameGuruCase.Project2.Config
{
    /// <summary>
    /// Holds audio-related configuration data such as clip, pitch settings, and tolerance values
    /// for perfect cuts or timing-based events.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "GridSystem/AudioConfig", order = 0)]
    public class AudioConfig : ScriptableObject
    {
        public AudioClip noteClip;
        public float basePitch = 1.0f;
        public float pitchIncrement = 0.1f;
        public float maxPitch = 2.0f;
        public float tolerance = 0.1f;
    }
}