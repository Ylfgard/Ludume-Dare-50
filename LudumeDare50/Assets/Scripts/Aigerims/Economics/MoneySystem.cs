using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private int _moneyAmount;
    [SerializeField] private float _timer;
    [SerializeField] private float _currentTime;
    private void Update()
    {
        if (_currentTime > 0)
            _currentTime -= Time.deltaTime;
        else
        {
            InscreaseMoney();
            _moneyText.text = _moneyAmount.ToString();
            _currentTime = _timer;
        }
    }

    private int InscreaseMoney()
    {
        return _moneyAmount += 100;
    }

}
