using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Protesters
{
    public delegate void EventHappend();
    public class ProtestWarning : MonoBehaviour
    {
        public event EventHappend ProtestsEnded;
        [SerializeField]
        private RectTransform _transform;
        [Header ("People")]
        [SerializeField]
        private Slider _peopleBar;
        [SerializeField]
        private TextMeshProUGUI _peopleCount;
        [Header ("Power")]
        [SerializeField]
        private Slider _powerBar;
        [SerializeField]
        private TextMeshProUGUI _powerCount;
        [SerializeField]
        private int _revolutionPeriod;
        private RevolutionBar _revolutionBar;

        public void Initialize(int maxPeople, float maxPower, Vector3 position, RevolutionBar revolutionBar)
        {
            _revolutionPeriod *= 100;
            _revolutionBar = revolutionBar;
            _transform.position = Camera.main.WorldToScreenPoint(position);
            _peopleBar.maxValue = maxPeople;
            _peopleBar.value = maxPeople;
            _powerBar.maxValue = maxPower;
            _powerBar.value = maxPower;
            ShowCount();
        }

        private void FixedUpdate()
        {
            var value = _powerBar.value * Time.deltaTime / _revolutionPeriod;
            _revolutionBar.ChangeRevolutionLevel(value);
        }

        public void AddProtesters(int newPeople, float newPower)
        {
            _peopleBar.maxValue += newPeople;
            _peopleBar.value += newPeople;
            _powerBar.maxValue += newPower;
            _powerBar.value += newPower;
            ShowCount();
        }

        public void DecreasePeopleCount(int value)
        {
            var people = _peopleBar.value;
            people -= value;
            if(people <= 0) EndProtest();
            float percent = people / _powerBar.maxValue;
            _powerBar.value = _powerBar.maxValue * percent;
            ShowCount();
        }
        
        private void EndProtest()
        {
            Debug.Log("Miting ended");
            ProtestsEnded?.Invoke();
            Destroy(gameObject);
            return;
        }

        private void ShowCount()
        {
            _peopleCount.text = _peopleBar.value.ToString();
            _powerCount.text = _powerBar.value.ToString();
        }
    }
}