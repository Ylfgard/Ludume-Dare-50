using UnityEngine;

namespace InputSystem
{
    public class InputDetector : MonoBehaviour
    {
        [SerializeField]
        private InputHandler _inputHandler;
        [SerializeField]
        private LayerMask _ignoreLayers;
        [SerializeField]
        private LayerMask _selectableLayers;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0)) LeftMouseButton();
            else if(Input.GetMouseButtonDown(1)) RightMouseButton();
        }

        private void LeftMouseButton()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, Mathf.Infinity, _selectableLayers)) _inputHandler.SelectUnit(hit);
        }

        private void RightMouseButton()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, Mathf.Infinity, _ignoreLayers)) return;
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) _inputHandler.HandleInput(hit);
        }
    }
}