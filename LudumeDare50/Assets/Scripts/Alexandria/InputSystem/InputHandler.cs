using UnityEngine;
using Police;
using City;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        private GameObject _selectedObject;

        public void SelectUnit(RaycastHit hit)
        {
            if(_selectedObject != null)
            {
                var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
                if(avtozak != null)
                {
                    var policeStation = hit.collider.GetComponent<PoliceStation>();
                    if(policeStation != null)
                    {
                    
                        
                    }
                }
            }
            _selectedObject = hit.collider.gameObject;
        }

        public void DeselectObject()
        {
            _selectedObject = null;
        }

        public void HandleInput(RaycastHit hit)
        {
            if(_selectedObject == null) return;
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null)
            {
                avtozak.MoveCommand(hit.point, hit.collider.gameObject);
                return;
            }
            var policeStation = _selectedObject.GetComponent<PoliceStation>();
            if(policeStation != null)
            {
                policeStation.SetArrivingPoint(hit.point, hit.collider.gameObject);
            }
        }
    }
}