using System;
using UnityEngine;

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
