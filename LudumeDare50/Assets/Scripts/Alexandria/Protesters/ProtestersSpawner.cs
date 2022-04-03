using UnityEngine;
using System.Linq;
using City;
using Protesters;

namespace Protesters
{
    public class ProtestersSpawner : MonoBehaviour
    { 
        [SerializeField]
        private GameObject _protestorsWarning;
        [SerializeField]
        private Transform _protestorsParentTransf;
        private RevolutionBar _revolutionBar;

        private void Awake()
        {
            _revolutionBar = FindObjectOfType<RevolutionBar>();
            var protestersChoosers = FindObjectsOfType<MonoBehaviour>().OfType<IProtestersChooser>();
            foreach(var pc in protestersChoosers)
                pc.ProtestersChoosed += SpawnProtestors;
        }

        public void SpawnProtestors(int maxPeople, int maxPower, Vector3 position, MitingSquare square)
        {
            var protest = Instantiate(_protestorsWarning, position, Quaternion.identity, _protestorsParentTransf).GetComponent<ProtestWarning>();
            square.StartMiting(protest);
            protest.Initialize(maxPeople, maxPower, position, _revolutionBar, square);
        }
    }
}