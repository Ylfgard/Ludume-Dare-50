using UnityEngine;
using TMPro;
using FMODUnity;

namespace Police
{
    public class AvtozakUpgradeSystem : MonoBehaviour
    {
        [SerializeField] private AvtozakBehavior _behaviour;
        [SerializeField] private TextMeshProUGUI _arrestDelayText;
        [SerializeField] private TextMeshProUGUI _capacityText;
        [SerializeField] private TextMeshProUGUI _speedText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private TextMeshProUGUI _totalPriceText;

        [Header ("UI windows")]
        [SerializeField] private GameObject _upgradeWindow;
        [SerializeField] private GameObject _shopWindow;


        [Header("Values")]
        [SerializeField] private float _arrestDelay;
        [SerializeField] private int _capacity;
        [SerializeField] private float _speed;
        [SerializeField] private int _health;


        [Header("Upgrade Values")]
        [SerializeField] private int _arrestDelayUpgradeValue;
        [SerializeField] private int _capacityUpgradeValue;
        [SerializeField] private int _speedUpgradeValue;
        [SerializeField] private int _healthUpgradeValue;

        [Header("Prices")]
        [SerializeField] private int _arrestDelayPrice;
        [SerializeField] private int _capacityPrice;
        [SerializeField] private int _speedPrice;
        [SerializeField] private int _healthPrice;

        [Header("Total Price")]
        [SerializeField] private int _totalPrice;

        private void Awake()
        {
            EndUpgrade();    
        }

        private void ActivateUpgradeWindow()
        {
            _arrestDelay = _behaviour.ArrestDelay;
            _capacity = _behaviour.Capacity;
            _speed = _behaviour.Speed;
            _health = _behaviour.Health;

            _arrestDelayText.text = _arrestDelay.ToString();
            _capacityText.text = _capacity.ToString();
            _speedText.text = _speed.ToString();
            _healthText.text = _health.ToString();
        }

        public void IncreaseArrestDelay()
        {
            _arrestDelay++;
            _totalPrice += _arrestDelayPrice;
            _totalPriceText.text = _totalPrice.ToString();
            _arrestDelayUpgradeValue++;
            _arrestDelayText.text = _arrestDelay.ToString();
        }

        public void DecreaseArrestDelay()
        {
            if (_arrestDelay > _behaviour.ArrestDelay)
            {
                _arrestDelay--;
                _arrestDelayText.text = _arrestDelay.ToString();
                _arrestDelayUpgradeValue--;
                _totalPrice -= _arrestDelayPrice;
                _totalPriceText.text = _totalPrice.ToString();
            }
            else
            {
                _arrestDelay = _behaviour.ArrestDelay;
                _arrestDelayText.text = _arrestDelay.ToString();
                _arrestDelayUpgradeValue = 0;
            }    
        }

        public void IncreaseCapacity()
        {
            _capacity++;
            _capacityUpgradeValue++;
            _totalPrice += _capacityPrice;
            _totalPriceText.text = _totalPrice.ToString();
            _capacityText.text = _capacity.ToString();
        }

        public void DecreaseCapacity()
        {
            if (_capacity > _behaviour.Capacity)
            {
                _capacity--;
                _capacityText.text = _capacity.ToString();
                _capacityUpgradeValue--;
                _totalPrice -= _capacityPrice;
                _totalPriceText.text = _totalPrice.ToString();
            }
            else
            {
                _capacity = _behaviour.Capacity;
                _capacityText.text = _capacity.ToString();
                _capacityUpgradeValue = 0;
            }
        }

        public void IncreaseSpeed()
        {
            _speed++;
            _speedUpgradeValue++;
            _totalPrice += _speedPrice;
            _totalPriceText.text = _totalPrice.ToString();
            _speedText.text = _speed.ToString();
        }

        public void DecreaseSpeed()
        {
            if (_speed > _behaviour.Speed)
            {
                _speed--;
                _speedText.text = _speed.ToString();
                _speedUpgradeValue--;
                _totalPrice -= _speedPrice;
                _totalPriceText.text = _totalPrice.ToString();
            }
            else
            {
                _speed = _behaviour.Speed;
                _speedText.text = _speed.ToString();
                _speedUpgradeValue = 0;
            }
        }

        public void IncreaseHP()
        {
            _health++;
            _healthUpgradeValue++;
            _totalPrice += _healthPrice;
            _totalPriceText.text = _totalPrice.ToString();
            _healthText.text = _health.ToString();
        }

        public void DecreaseHP()
        {
            if (_health > _behaviour.Health)
            {
                _health--;
                _healthText.text = _health.ToString();
                _healthUpgradeValue--;
                _totalPrice -= _healthPrice;
                _totalPriceText.text = _totalPrice.ToString();
            }
            else
            {
                _health = _behaviour.Health;
                _healthText.text = _health.ToString();
                _healthUpgradeValue = 0;
            }
        }

        public void UpgradeValues()
        {
            if (MoneySystem.Instance.MoneyAmount >= _totalPrice)
            {
                MoneySystem.Instance.DecreaseMoneyAmount(_totalPrice);
                RuntimeManager.PlayOneShot(FMODSingleton.Instance.moneySound);
                _behaviour.Upgrade(_behaviour.Health + _healthUpgradeValue, _behaviour.Speed + _speedUpgradeValue, _behaviour.Capacity + _capacityUpgradeValue, _behaviour.ArrestDelay + _arrestDelayUpgradeValue);
                EndUpgrade();
            }
        }

        public void StartUpgrade(AvtozakBehavior behavior)
        {
            _behaviour = behavior;
            ActivateUpgradeWindow();
            _upgradeWindow.SetActive(true);
        }

        public void EndUpgrade()
        {
            _behaviour = null;
            _upgradeWindow.SetActive(false);
        }

        public void EndUpgrade(AvtozakBehavior avtozak)
        {
            _behaviour = null;
            avtozak.LeavedPoliceStation -= EndUpgrade;
            _upgradeWindow.SetActive(false);
        }
    }
}

