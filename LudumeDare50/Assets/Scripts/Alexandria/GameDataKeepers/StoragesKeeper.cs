using UnityEngine;
using Protesters;

namespace GameDataKeepers
{
    public class StoragesKeeper : MonoBehaviour
    {
        [SerializeField]
        private MoneySystem _moneySystem;
        [SerializeField]
        private RevolutionBar _revolutionBar;
        [SerializeField]
        private MitingsDataStorage _mitingsStorage;
        [SerializeField]
        private PoliceDataStorage _policeStorage;

        public MoneySystem MoneySystem => _moneySystem;
        public RevolutionBar RevolutionBar => _revolutionBar;
        public MitingsDataStorage MitingsStorage => _mitingsStorage;
        public PoliceDataStorage PoliceStorage => _policeStorage;
    }
}