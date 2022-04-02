using UnityEngine;

namespace City
{
    public delegate void OnSquare(Collider collider, Square square);

    public class Square : MonoBehaviour
    {
        public event OnSquare EnterSquare;
        public event OnSquare LeaveSquare;

        private void OnTriggerEnter(Collider other)
        {
            EnterSquare?.Invoke(other, this);
        }

        private void OnTriggerExit(Collider other)
        {
            LeaveSquare?.Invoke(other, this);
        }
    }
}