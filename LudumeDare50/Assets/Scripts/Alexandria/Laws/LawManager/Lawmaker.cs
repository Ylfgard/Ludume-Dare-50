using UnityEngine;
using Laws.Effects;

namespace Laws.Managers
{
    public class Lawmaker : MonoBehaviour
    {
        [SerializeField]
        private TaxLawHandler _taxHandler;
        [SerializeField]
        private PublicOpinionLawHandler _publicOpinionHandler;
        [SerializeField]
        private MitingsLawHandler _mitingsHandler;
        [SerializeField]
        private PoliceLawHandler _policeHandler;

        public void AdoptLaw(LawContentSO content)
        {
            foreach(var effect in content.Effects)
            {
                switch(effect.Scope)
                {
                    case LawScope.Tax:
                    _taxHandler.ActivateEffect(effect as TaxLawSO, effect.Duration, effect.Delay);
                    break;

                    case LawScope.PublicOpinion:
                    _publicOpinionHandler.ActivateEffect(effect as PublicOpinionLawSO, effect.Duration, effect.Delay);
                    break;

                    case LawScope.Mitings:
                    _mitingsHandler.ActivateEffect(effect as MitingsLawSO, effect.Duration, effect.Delay);
                    break;

                    case LawScope.Police:
                    _policeHandler.ActivateEffect(effect as PoliceLawSO, effect.Duration, effect.Delay);
                    break;
                }
            }
        }
    }
}