using UnityEngine;
using UnityEngine.AI;
using City;

namespace Police.Avtozak
{
    public class AvtozakMovement : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent _agent;
        private Square _onSquare;
        private Square _targetSquare;

        public Square OnSquare => _onSquare;

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void MoveToSquare(Vector3 point, Square square)
        {
            MoveToPoint(point);
            if(square == _onSquare || square == _targetSquare) return;
            if(_targetSquare != null) _targetSquare.EnterSquare -= ArrivedOnSquare;
            square.EnterSquare += ArrivedOnSquare;
            _targetSquare = square;
        }

        private void ArrivedOnSquare(Collider collider, Square square)
        {
            Debug.Log("Arrived at " + this + " " + collider.name);
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = square;
             _onSquare.LeaveSquare += LeaveSquare;
            _targetSquare = null;
            square.EnterSquare -= ArrivedOnSquare;
        }

        private void LeaveSquare(Collider collider, Square square)
        {
            Debug.Log("Leave " + this + " " + collider.name);
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = null;
            square.LeaveSquare -= LeaveSquare;
        }
    }
}