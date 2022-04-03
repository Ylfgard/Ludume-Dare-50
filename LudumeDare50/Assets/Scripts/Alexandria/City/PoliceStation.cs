using UnityEngine;
using Police;

namespace City
{
    public class PoliceStation : Square
    {
        [SerializeField] private GameObject _avtozakPrefab;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _arrivingPoint;
        public event SendAvtozak AvtozakSpawned;
        private bool _isTriggeredEvent;
        
        
        public void SpawnAvtozak()
        {
            GameObject spawnedAvtozak = Instantiate(_avtozakPrefab, _spawnPoint);
            AvtozakBehavior avtozakBehaviour = GetAvtozakBehavior(spawnedAvtozak);
            avtozakBehaviour.Initialize(this);
            avtozakBehaviour.MoveCommand(_arrivingPoint.position, this.gameObject);
            DecreaseMoney(avtozakBehaviour);
            if (_isTriggeredEvent) return;
            _isTriggeredEvent = true;
            AvtozakSpawned?.Invoke(GetAvtozakBehavior(spawnedAvtozak));
        }
        
        private AvtozakBehavior GetAvtozakBehavior(GameObject spawnedAvtozak)
        {
            return spawnedAvtozak.GetComponent<AvtozakBehavior>();
        }

        private void DecreaseMoney(AvtozakBehavior avtozakBehavior)
        {
            if(MoneySystem.Instance.MoneyAmount >= avtozakBehavior.AvtozakPrice)
                MoneySystem.Instance.DecreaseMoneyAmount(avtozakBehavior.AvtozakPrice);
        }
    }
}