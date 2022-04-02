using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Protesters
{
    public class ProtestWarning : MonoBehaviour
    {
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

        public void Initialize(int maxPeople, int maxPower, Vector3 position)
        {
            _transform.position = position;
            _peopleBar.maxValue = maxPeople;
            _peopleBar.value = maxPeople;
            _peopleCount.text = maxPeople.ToString();
            _powerBar.maxValue = maxPower;
            _powerBar.value = maxPower;
            _powerCount.text = maxPower.ToString();
        }

        public void ChangePeopleCount(int value)
        {
            var people = _peopleBar.value;
            people += value;
            if(people <= 0)
            {
                Debug.Log("Miting ended");
                Destroy(gameObject);
                return;
            }
            _peopleCount.text = people.ToString();
            float percent = people / _powerBar.maxValue;
            _powerBar.value = _powerBar.maxValue * percent;
            _powerCount.text = _peopleBar.value.ToString();
        }
    }
}