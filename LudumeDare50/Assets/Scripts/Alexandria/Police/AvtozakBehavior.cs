using UnityEngine;
using City;

namespace Police.Avtozak
{
    public class AvtozakBehavior : MonoBehaviour
    {
        [SerializeField]
        private AvtozakMovement _avtozakMovement;
        
        public void MoveCommand(Vector3 point, GameObject target)
        {
            var square = target.GetComponent<Square>();
            if(square != null) _avtozakMovement.MoveToSquare(point, square);
            else _avtozakMovement.MoveToPoint(point);
        }
    }
}