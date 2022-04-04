using UnityEngine;
using UnityEngine.UI;
using TMPro;
using City;

namespace Protesters
{
    public delegate void EventHappend();

    public class ProtestWarning : MonoBehaviour
    {
        public event EventHappend ProtestEnded;
        [SerializeField]
        private RectTransform _transform;
        [SerializeField]
        private Miting _miting;
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

        public Miting Miting => _miting;
        public Slider PeopleBar => _peopleBar;
        public Slider PowerBar => _powerBar;

        public void Initialize(int maxPeople, float maxPower, Vector3 position, RevolutionBar revolutionBar, MitingSquare square)
        {
            _miting.Initialize(revolutionBar, square);
            _transform.position = Camera.main.WorldToScreenPoint(position);
            _peopleBar.maxValue = maxPeople;
            _peopleBar.value = maxPeople;
            _powerBar.maxValue = maxPower;
            _powerBar.value = maxPower;
            ShowCount();
        }
        
        public void EndProtest()
        {
            ProtestEnded?.Invoke();
            Destroy(gameObject);
            return;
        }

        public void ShowCount()
        {
            _peopleCount.text = _peopleBar.value.ToString();
            _powerCount.text = _powerBar.value.ToString();
        }
    }
}