using UnityEngine;

namespace Code.Runtime
{
    public class Pickup : MonoBehaviour
    {
        public void SnapAbove(Vector3 snapPoint)
        {
            transform.position = snapPoint + Vector3.up * 2;
        }

        public void SnapTo(Vector3 snapPoint)
        {
            transform.position = snapPoint;
        }

        private void OnMouseDown()
        {
            HandManager.Current.Holding ??= this;
        }
    }
}