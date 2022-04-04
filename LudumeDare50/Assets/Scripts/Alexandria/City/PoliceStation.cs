using UnityEngine;
using Police;
using FMODUnity;

namespace City
{
    public class PoliceStation : Square
    {
        [SerializeField] private GameObject _avtozakPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _arrivingPoint;
        public event SendAvtozak AvtozakSpawned;
        private bool _isTriggeredEvent;
        private GameObject _arrivingSquare;

        public Transform ArrivingPoint => _arrivingPoint;

        private void Start()
        {
            _arrivingSquare = this.gameObject;    
        }

        public void SetArrivingPoint(Vector3 position, GameObject arrivingSquare)
        {
            _arrivingPoint.position = position;
            _arrivingSquare = arrivingSquare;
        }

        public void SpawnAvtozak()
        {
            if (MoneySystem.Instance.MoneyAmount >= _avtozakPrefab.GetComponent<AvtozakBehavior>().AvtozakPrice)
            {
                GameObject spawnedAvtozak = Instantiate(_avtozakPrefab, _spawnPoint);
                AvtozakBehavior avtozakBehaviour = GetAvtozakBehavior(spawnedAvtozak);
                avtozakBehaviour.Initialize(this);
                avtozakBehaviour.MoveCommand(_arrivingPoint.position, _arrivingSquare);
                DecreaseMoney(avtozakBehaviour);
                if (_isTriggeredEvent) return;
                _isTriggeredEvent = true;
                AvtozakSpawned?.Invoke(avtozakBehaviour);
            }
        }

        private AvtozakBehavior GetAvtozakBehavior(GameObject spawnedAvtozak)
        {
            return spawnedAvtozak.GetComponent<AvtozakBehavior>();
        }

        private void DecreaseMoney(AvtozakBehavior avtozakBehavior)
        {
            MoneySystem.Instance.DecreaseMoneyAmount(avtozakBehavior.AvtozakPrice);
            RuntimeManager.PlayOneShot(MoneySystem.Instance.MoneySound);
        }
    }
}