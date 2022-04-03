using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class TaxLawHandler : MonoBehaviour
    {
        public void ActivateEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyEffect(storagesKeeper, effect, duration, false);
            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, TaxLawSO effect, float duration, bool reverse)
        {
            var moneySystem = storagesKeeper.MoneySystem;
            var value = effect.Value;   
            if(effect.InPercent) value *= 0.01f;
            if(reverse)
            {
                if(effect.InPercent) value = 1 / value;
                else value = -value;
            }
           
            switch(effect.Type)
            {
                case TaxLawType.Income:
                if(effect.InPercent) 
                    moneySystem.ChangeIncome((int)(value * moneySystem.Income));
                else
                    moneySystem.ChangeIncome((int)value + moneySystem.Income);
                break;

                case TaxLawType.Budget:
                if(effect.InPercent) 
                    moneySystem.MoneyAmount = (int)(value * moneySystem.MoneyAmount);
                else
                    moneySystem.ChangeMoneyCount((int)value);
                break;
            }
        }
    }
}