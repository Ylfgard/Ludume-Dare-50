using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Police;
using City;

namespace GameDataKeepers
{
    public class PoliceDataStorage : MonoBehaviour
    {
        private List<AvtozakBehavior> _avtozaks;
        private List<PoliceStation> _policeStations;

        public List<AvtozakBehavior> Avtozaks => _avtozaks;
        public List<PoliceStation> PoliceStations => _policeStations;

        private void Awake()
        {
            _avtozaks = FindObjectsOfType<AvtozakBehavior>().ToList();
            _policeStations = FindObjectsOfType<PoliceStation>().ToList();
            foreach(var avtozak in _avtozaks)
                avtozak.Destructed += RemoveAvtozak;
            foreach(var station in _policeStations)
                station.AvtozakSpawned += AddAvtozak;
        }

        private void AddAvtozak(AvtozakBehavior avtozak)
        {
            _avtozaks.Add(avtozak);
            avtozak.Destructed += RemoveAvtozak;
        }

        private void RemoveAvtozak(AvtozakBehavior avtozak)
        {
            _avtozaks.Remove(avtozak);
        }
    }
}