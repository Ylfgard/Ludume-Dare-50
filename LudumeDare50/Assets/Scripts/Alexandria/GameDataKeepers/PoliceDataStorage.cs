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
        }
    }
}