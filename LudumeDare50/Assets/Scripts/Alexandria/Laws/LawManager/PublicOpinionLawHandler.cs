using UnityEngine;
using System.Collections;
using Laws.Effects;
using GameDataKeepers;

namespace Laws.Managers
{
    public class PublicOpinionLawHandler : MonoBehaviour
    {
        private StoragesKeeper _storagesKeeper;

        public void ActivateEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, float delay)
        {
            StartCoroutine(HandleEffect(storagesKeeper, effect, duration, delay));
        }

        private IEnumerator HandleEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, float delay)
        {
            yield return new WaitForSeconds(delay);
            ApplyEffect(storagesKeeper, effect, duration, false);
            if(duration > 0)
            {
                yield return new WaitForSeconds(duration);
                ApplyEffect(storagesKeeper, effect, duration, true);
            }
        }

        private void ApplyEffect(StoragesKeeper storagesKeeper, PublicOpinionLawSO effect, float duration, bool reverse)
        {
            var revolution = storagesKeeper.RevolutionBar;
            var value = effect.Value;   
            if(effect.InPercent) value *= 0.01f;
            if(reverse)
            {
                if(effect.InPercent) value = 1 / value;
                else value = -value;
            }
            
            switch(effect.Type)
            {
                case PublicOpinionLawType.Income:
                if(effect.InPercent) 
                    revolution.SetMoodChanging(value * revolution.MoodChanging);
                else
                    revolution.ChangeMoodChanging(value);
                break;

                case PublicOpinionLawType.Budget:
                if(effect.InPercent) 
                {
                    var changes = (value * revolution.Level) - revolution.Level;
                    revolution.ChangeRevolutionLevel(changes);
                } 
                else
                {
                    revolution.ChangeRevolutionLevel(value);
                }  
                break;
            }
        }
    }
}