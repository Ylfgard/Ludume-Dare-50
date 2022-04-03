using UnityEngine;
using Protesters;

namespace GameDataKeepers
{
    public class StoragesKeeper : MonoBehaviour
    {
        [SerializeField]
        private PoliceDataStorage _policeStorage;
        [SerializeField]
        private MoneySystem _moneySystem;
        [SerializeField]
        private RevolutionBar _revolutionBar;

        public PoliceDataStorage PoliceStorage => _policeStorage;
        public MoneySystem MoneySystem => _moneySystem;
        public RevolutionBar RevolutionBar => _revolutionBar;
    }
}