using UnityEngine;

namespace GameDataKeepers
{
    public class StoragesKeeper : MonoBehaviour
    {
        [SerializeField]
        private PoliceDataStorage _policeStorage;

        public PoliceDataStorage PoliceStorage => _policeStorage;
    }
}