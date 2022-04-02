using UnityEngine;
using Police.Avtozak;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        private AvtozakBehavior _selectedAvtozak;
        private Vector3 _targetPoint;

        public void SelectUnit(RaycastHit hit)
        {
            var avtozak = hit.collider.GetComponent<AvtozakBehavior>();
            if(avtozak == null) return;
            _selectedAvtozak = avtozak;
        }

        public void DeselectUnit()
        {
            _selectedAvtozak = null;
        }

        public void HandleInput(RaycastHit hit)
        {
            _targetPoint = hit.point;
            if(_selectedAvtozak == null) return;
            _selectedAvtozak.MoveCommand(_targetPoint, hit.collider.gameObject);
        }
        
        private void OnDrawGizmos()
        {
            if(_targetPoint == Vector3.zero) return;
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPoint, 0.5f); 
        }
    }
}