using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class MitingsLawHandler : MonoBehaviour
    {
        private StoragesKeeper _storagesKeeper;

        public void ActivateEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyEffect(storagesKeeper, effect, duration, false);
            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, MitingsLawSO effect, float duration, bool reverse)
        {
            var mitingStorage = storagesKeeper.MitingsStorage;
            var value = effect.Value;   
            if(effect.InPercent) value *= 0.01f;
            if(reverse)
            {
                if(effect.InPercent) value = 1 / value;
                else value = -value;
            }
            
            switch(effect.Type)
            {
                case MitingsLawType.People:
                if(effect.InPercent) 
                {
                    foreach(var miting in mitingStorage.Mitings)
                    {
                        var people = miting.Protest.PeopleBar.value;
                        int changes = (int)((value * people) - people);
                        miting.ChangeProtesters(changes, 0);
                    }
                }
                else
                {
                    foreach(var miting in mitingStorage.Mitings)
                        miting.ChangeProtesters((int)value, 0);
                }
                break;

                case MitingsLawType.Power:
                if(effect.InPercent) 
                {
                    foreach(var miting in mitingStorage.Mitings)
                    {
                        var power = miting.Protest.PowerBar.value;
                        var changes = (value * power) - power; 
                        miting.ChangeProtesters(0, changes);
                    }
                } 
                else
                {
                    foreach(var miting in mitingStorage.Mitings)
                        miting.ChangeProtesters(0, value);
                }  
                break;
            }
        }
    }
}