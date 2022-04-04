using UnityEngine;
using Police;
using City;
using GameDataKeepers;

namespace InputSystem
{
    public class InputHandler : MonoBehaviour
    {
        [SerializeField]
        private Transform _pointer;
        private StoragesKeeper _storagesKeeper;
        private GameObject _selectedObject;

        private void Start()
        {
            _storagesKeeper = FindObjectOfType<StoragesKeeper>();
            DeselectObject();
        }

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
                        if(avtozak.OnPoliceStation == policeStation)
                        {
                            _pointer.position = policeStation.PointerPoint.position;
                            _pointer.gameObject.SetActive(true);
                            _storagesKeeper.PoliceStorage.AvtozakUpgrade.StartUpgrade(avtozak);
                            return;
                        }
                    }
                }
            }
            _selectedObject = hit.collider.gameObject;
            OutlineObject();
        }

        private void OutlineObject()
        {
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null) 
            {
                avtozak.Outline.SetActive(true);
                return;
            }

            var policeStation = _selectedObject.GetComponent<PoliceStation>();
            if(policeStation != null)
            {
                _pointer.position = policeStation.PointerPoint.position;
                _pointer.gameObject.SetActive(true);
                
            }
        }

        private void DeselectObject()
        {
            if(_selectedObject == null) return;
            var avtozak = _selectedObject.GetComponent<AvtozakBehavior>();
            if(avtozak != null) avtozak.Outline.SetActive(false);
            else _pointer.gameObject.SetActive(false);
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