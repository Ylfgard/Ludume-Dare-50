using UnityEngine;
using Police;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        private AvtozakBehavior _selectedAvtozak;

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
            if(_selectedAvtozak != null)
                _selectedAvtozak.MoveCommand(hit.point, hit.collider.gameObject);
            DeselectUnit();
        }
    }
}