using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Represents a single platform in the game, storing mesh and collider references.
    /// </summary>
    public class Platform : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private BoxCollider _boxCollider;
        public MeshRenderer meshRenderer;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _boxCollider = GetComponent<BoxCollider>();
            meshRenderer = GetComponent<MeshRenderer>();
        }

        /// <summary>
        /// Updates the BoxCollider size based on the current mesh bounds.
        /// </summary>
        public void ResizeCollider()
        {
            _boxCollider.size = _meshFilter.sharedMesh.bounds.size;
        }
    }
}