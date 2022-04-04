using UnityEngine;
using City;

public class AvtozakShop : MonoBehaviour
{
    [SerializeField]
    private GameObject _shopWindow;
    private PoliceStation _policeStation;

    public void OpenAvtozakShop(PoliceStation policeStation)
    {
        _policeStation = policeStation;
        _shopWindow.SetActive(true);
    }
    
    public void BuyAvtozak()
    {
        if(_policeStation == null)
        {
            Debug.LogError("No police station");
            return;
        }
        _policeStation.SpawnAvtozak(true);
        _shopWindow.SetActive(false);
    }

    public void CloseAvtozakShop()
    {
        _policeStation = null;
        _shopWindow.SetActive(false);
    }
}