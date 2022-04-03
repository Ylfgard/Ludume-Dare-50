using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using City;
using Protesters;
using TMPro;

namespace Police
{
    public delegate void SendAvtozak(AvtozakBehavior avtozak);

    public class AvtozakBehavior : MonoBehaviour
    {
        public event SendAvtozak Destructed;
        [SerializeField]
        private AvtozakMovement _movement;
        [SerializeField]
        private Slider _healthBar;
        [SerializeField]
        private Slider _occupancyBar;
        [SerializeField]
        private TextMeshProUGUI _count;
        [SerializeField]
        private GameObject _outline;
        [Header ("Specifications")]
        [SerializeField]
        private int _health;
        [SerializeField]
        private float _speed;
        [SerializeField]
        private int _capacity;
        [SerializeField]
        private float _arrestDelay;
        [SerializeField]
        private float _unloadingDelay;
        [SerializeField]
        private int _avtozakPrice;

        private Miting _onMiting;

        public int Health => _health;
        public float Speed => _speed;
        public int Capacity => _capacity;
        public float ArrestDelay => _arrestDelay;
        public int AvtozakPrice => _avtozakPrice;

        public GameObject Outline => _outline;

        private void Awake()
        {
            Upgrade(_health, _speed, _capacity, _arrestDelay);
            _healthBar.value = _health;
            _occupancyBar.value = 0;
            _count.text = "0";
            _movement.ArrivedOnMiting += StartArrests;
            _movement.LeavedMiting += EndArrests;
            _movement.ArrivedOnPoliceStation += OnPoliceStation;
            _movement.LeavedPoliceStation += LeavePoliceStation;
        }
        public void Initialize(Square square)
        {
            _movement.Initialize(Speed, this, square);
        }
        public void Upgrade(int health, float speed, int capacity, float arrestDelay)
        {
            _health = health;
            _healthBar.maxValue = _health;
            _capacity = capacity;
            _occupancyBar.maxValue = _capacity;
            _speed = speed;
            _movement.Initialize(_speed, this, _movement.OnSquare);
            _arrestDelay = arrestDelay;
        }
        
        public void MoveCommand(Vector3 point, GameObject target)
        {
            var square = target.GetComponent<Square>();
            if(square != null) _movement.MoveToSquare(point, square);
            else _movement.MoveToPoint(point);
        }

        public void StartArrests()
        {
            if(_occupancyBar.value >= _capacity) return;
            _onMiting = _movement.OnSquare.GetComponent<MitingSquare>().Miting;
            StartCoroutine(Arrest());
        }

        public void EndArrests()
        {
            StopAllCoroutines();
            _onMiting = null;
        }

        public void TakeDamage(float damage)
        {
            _healthBar.value -= damage;
            if(_healthBar.value <= 0) Destruction();
        }

        private void Destruction()
        {
            if(_movement.OnSquare != null) 
                _movement.OnSquare.LeaveSquare(this);
            EndArrests();
            Debug.Log("Avtozak " + this + " destroyed");
            Destructed?.Invoke(this);
            Destroy(gameObject);
        }

        private IEnumerator Arrest()
        {
            yield return new WaitForSeconds(_arrestDelay);
            if(_onMiting != null)
            {
                _onMiting.ArrestPeople();
                _occupancyBar.value++;
                _count.text = _occupancyBar.value.ToString();
                if(_occupancyBar.value < _capacity) StartCoroutine(Arrest());
                else EndArrests();
            }
            else EndArrests();
        }
        
        public void OnPoliceStation()
        {
            if(_occupancyBar.value > 0) StartCoroutine(Unloading());
        }

        public void LeavePoliceStation()
        {
            StopAllCoroutines();
        }

        private IEnumerator Unloading()
        {
            yield return new WaitForSeconds(_unloadingDelay);
            if(_movement.OnSquare != null)
            {
                var policeStation = _movement.OnSquare.GetComponent<PoliceStation>();
                if(policeStation != null)
                    if(_occupancyBar.value > 1)
                    {
                        _occupancyBar.value--;
                        _count.text = _occupancyBar.value.ToString();
                        StartCoroutine(Unloading());
                    } 
                    else if(_occupancyBar.value > 0)
                    {
                        _occupancyBar.value--;
                        _count.text = _occupancyBar.value.ToString();
                    }
            }
        }
    }
}