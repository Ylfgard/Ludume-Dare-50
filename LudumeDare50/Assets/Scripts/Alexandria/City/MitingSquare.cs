using UnityEngine;
using Protesters;
using Police;

namespace City
{
    public class MitingSquare : Square
    {
        public event EventHappend MitingStarted;
        public event EventHappend MitingEnded;
        private Miting _miting;

        public Miting Miting => _miting;

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            CheckForResistence();    
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            CheckForResistence();    
        }

        public override void LeaveSquare(AvtozakBehavior avtozak)
        {
            base.LeaveSquare(avtozak);
            CheckForResistence();
        }

        private void CheckForResistence()
        {
            if(_miting == null) return;
            if(_avtozaksOnSquare.Count > 0 && _miting.Resisting == false) 
                _miting.StartResist();
            else if(_avtozaksOnSquare.Count == 0 && _miting.Resisting) 
                _miting.EndResist();
        }

        public void StartMiting(ProtestWarning protest)
        {
            _miting = protest.Miting;
            protest.ProtestEnded += EndMiting;
            CheckForResistence();
            MitingStarted?.Invoke();
        }
        
        public void EndMiting()
        {
            _miting.Protest.ProtestEnded -= EndMiting;
            MitingEnded?.Invoke();
            _miting = null;
        }
    }
}