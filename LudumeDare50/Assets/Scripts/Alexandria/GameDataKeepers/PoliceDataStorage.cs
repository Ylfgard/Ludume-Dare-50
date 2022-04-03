using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using Police;

namespace GameDataKeepers
{
    public class PoliceDataStorage : MonoBehaviour
    {
        private List<AvtozakBehavior> _avtozaks;

        public List<AvtozakBehavior> Avtozaks => _avtozaks;

        private void Awake()
        {
            _avtozaks = FindObjectsOfType<AvtozakBehavior>().ToList();
            foreach(var avtozak in _avtozaks)
                avtozak.Destructed += RemoveAvtozak;
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