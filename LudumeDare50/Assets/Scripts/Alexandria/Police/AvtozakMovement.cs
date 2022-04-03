using UnityEngine;
using UnityEngine.AI;
using City;
using Protesters;

namespace Police
{
    public class AvtozakMovement : MonoBehaviour
    {
        public event EventHappend ArrivedOnMiting;
        public event EventHappend LeavedMiting;
        public event EventHappend ArrivedOnPoliceStation;
        public event EventHappend LeavedPoliceStation;
        [SerializeField]
        private NavMeshAgent _agent;
        private AvtozakBehavior _behavior;
        private Square _onSquare;
        private Square _targetSquare;

        public Square OnSquare => _onSquare;

        public void Initialize(float speed, AvtozakBehavior behavior)
        {
            _behavior = behavior;
            _agent.speed = speed;
        }

        public void MoveToPoint(Vector3 point)
        {
            _agent.SetDestination(point);
        }

        public void MoveToSquare(Vector3 point, Square square)
        {
            MoveToPoint(point);
            if(square == _onSquare || square == _targetSquare) return;
            if(_targetSquare != null) _targetSquare.EnteredSquare -= ArrivedOnSquare;
            square.EnteredSquare += ArrivedOnSquare;
            _targetSquare = square;
        }

        private void ArrivedOnSquare(Collider collider, Square square)
        {
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = square;
            _onSquare.LeavedSquare += LeaveSquare;
            _targetSquare = null;
            square.EnteredSquare -= ArrivedOnSquare;
            var miting = square.GetComponent<MitingSquare>();
            if(miting != null)
            {
                miting.MitingStarted += _behavior.StartArrests;
                miting.MitingEnded += _behavior.EndArrests;
                if(miting.Miting == null) return;
                ArrivedOnMiting?.Invoke();
            }
            else
            {
                var policeStation = square.GetComponent<PoliceStation>();
                if(policeStation == null) return;
                ArrivedOnPoliceStation?.Invoke();
            }
        }

        private void LeaveSquare(Collider collider, Square square)
        {
            var avtozak = collider.GetComponent<AvtozakMovement>();
            if(avtozak != this) return;
            _onSquare = null;
            square.LeavedSquare -= LeaveSquare;
            var miting = square.GetComponent<MitingSquare>();
            if(miting != null)
            {
                miting.MitingStarted -= _behavior.StartArrests;
                miting.MitingEnded -= _behavior.EndArrests;
                if(miting.Miting == null) return;
                LeavedMiting?.Invoke();
            }
            else
            {
                var policeStation = square.GetComponent<PoliceStation>();
                if(policeStation == null) return;
                LeavedPoliceStation?.Invoke();
            }
        }
    }
}