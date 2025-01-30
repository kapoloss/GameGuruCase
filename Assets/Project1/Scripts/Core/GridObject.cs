using UnityEngine;

public class GridObject : MonoBehaviour
{
    private bool _isMarked;
    [SerializeField] private GameObject markObject;
    public void OnClick()
    {
        ChangeValue();
    }

    private void ChangeValue()
    {
        _isMarked = !_isMarked;
        markObject.SetActive(_isMarked);
    }

    public bool GetValue()
    {
        return _isMarked;
    }
}
