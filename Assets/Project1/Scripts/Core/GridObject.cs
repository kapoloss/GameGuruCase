using UnityEngine;

namespace GameGuruCase.Project1.Core
{
    /// <summary>
    /// Represents a single cell or tile in the grid, toggling a mark on click.
    /// </summary>
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

        public void WinGrid()
        {
            ChangeValue();
        }
    }
}