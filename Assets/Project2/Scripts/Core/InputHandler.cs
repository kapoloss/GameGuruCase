using System;
using UnityEngine;

namespace GameGuruCase.Project2.Core
{
    /// <summary>
    /// Handles mouse input and emits a world position via OnMouseClick when the player clicks.
    /// </summary>
    public class InputHandler : MonoBehaviour
    {
        public event Action<Vector2> OnMouseClick;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null)
                {
                    Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    OnMouseClick?.Invoke(worldPos);
                }
            }
        }
    }
}