using UnityEngine;
using TMPro;
using FMODUnity;
public class MoneySystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private float _timer;
    [SerializeField] private float _currentTime;
    [SerializeField] private int _income;
    [SerializeField] [EventRef] private string _moneySound;

    public int MoneyAmount;

    public int Income => _income;
    public string MoneySound => _moneySound;

    #region Singleton Init
    private static MoneySystem _instance;
    private void Awake() // Init in order
    {
        if (_instance == null)
            Init();
        else if (_instance != this)
        {
            Debug.Log($"Destroying {gameObject.name}, caused by one singleton instance");
            Destroy(gameObject);
        }
    }
    public static MoneySystem Instance // Init not in order
    {
        get
        {
            if (_instance == null)
                Init();
            return _instance;
        }
        private set { _instance = value; }
    }
    static void Init() // Init script
    {
        _instance = FindObjectOfType<MoneySystem>();
        if (_instance != null)
            _instance.Initialize();
    }
    #endregion

    private void Update()
    {
        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            InscreaseMoney();
            _currentTime = _timer;
        }

        _moneyText.text = MoneyAmount.ToString();
    }

    public void ChangeMoneyCount(int value)
    {
        MoneyAmount += value;
    }

    public void DecreaseMoneyAmount(int value)
    {
        MoneyAmount -= value;
    }

    public void ChangeIncome(int income)
    {
        _income = income;
    }

    private int InscreaseMoney()
    {
        return MoneyAmount += _income;
    }

    private void Initialize()
    {
        enabled = true;
    }
}
