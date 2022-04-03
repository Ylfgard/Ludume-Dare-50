using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using City;
using Police;

namespace Protesters
{
    public class Miting : MonoBehaviour
    {
        [SerializeField]
        private ProtestWarning _protestWarning;
        [SerializeField]
        private float _revolutionPeriod;
        [SerializeField]
        private float _resistPeriod;
        private RevolutionBar _revolutionBar;
        private Square _square;
        private bool _resisting;

        public ProtestWarning Protest => _protestWarning;
        public bool Resisting => _resisting;

        public void Initialize(RevolutionBar revolutionBar, Square square)
        {
            _revolutionPeriod *= 100;
            _revolutionBar = revolutionBar;
            _square = square;
            _resisting = false;
        }

        public void StartResist()
        {
            _resisting = true;
            StartCoroutine(AttackAvtozaks());
        }

        public void EndResist()
        {
            StopAllCoroutines();
            _resisting = false;
        }

        private IEnumerator AttackAvtozaks()
        {
            yield return new WaitForSeconds(_resistPeriod);
            List<AvtozakBehavior> avtozaks = new List<AvtozakBehavior>();
            foreach(var avtozak in _square.AvtozaksOnSquare)
                avtozaks.Add(avtozak);
            var damage = _protestWarning.PowerBar.value / avtozaks.Count;
            foreach(var avtozak in avtozaks)
                avtozak.TakeDamage(damage);
            if(avtozaks.Count > 0) StartCoroutine(AttackAvtozaks());
            else EndResist();
        }

        private void FixedUpdate()
        {
            if(_resisting) return;
            var value = _protestWarning.PowerBar.value * Time.deltaTime / _revolutionPeriod;
            _revolutionBar.ChangeRevolutionLevel(value);
        }

        public void AddProtesters(int newPeople, float newPower)
        {
            _protestWarning.PeopleBar.maxValue += newPeople;
            _protestWarning.PeopleBar.value += newPeople;
            _protestWarning.PowerBar.maxValue += newPower;
            _protestWarning.PowerBar.value += newPower;
            _protestWarning.ShowCount();
        }

        public void ArrestPeople()
        {
            _protestWarning.PeopleBar.value --;
            if(_protestWarning.PeopleBar.value <= 0) 
            {
                _protestWarning.EndProtest();
                return;
            }
            float percent = _protestWarning.PeopleBar.value / _protestWarning.PeopleBar.maxValue;
            _protestWarning.PowerBar.value = _protestWarning.PowerBar.maxValue * percent;
            _protestWarning.ShowCount();
        }
    }
}