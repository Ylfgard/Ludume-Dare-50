using UnityEngine;
using System.Collections.Generic;
using GameDataKeepers;

namespace Laws.Managers
{
    public class LawChooser : MonoBehaviour
    {
        [SerializeField]
        private GameObject _law;
        [SerializeField]
        private Transform _lawsPoint;
        [SerializeField] [Range (0, 100)]
        private int _emptyChance;
        private StoragesKeeper _storagesKeeper;

        private void Start()
        {
            _storagesKeeper = FindObjectOfType<StoragesKeeper>();
            var squares = _storagesKeeper.MitingsStorage.MitingSquares;
            foreach(var square in squares)
                square.MitingEnded += SpawnLaw;
        }

        private LawContentSO ChooseRandomLawContent()
        {
            LawTier lawTier = ChooseRandomLawTier();
            if(lawTier == null) return null;
            int randomLawIndex = Random.Range(0, lawTier.LawsContent.Length);
            return lawTier.LawsContent[randomLawIndex];
        }

        private LawTier ChooseRandomLawTier()
        {
            var lawTiers = _storagesKeeper.LawsKeeper.LawTiers;
            float level = _storagesKeeper.RevolutionBar.Level;
            List<LawTier> availableTiers = new List<LawTier>(); 
            int totalWeight = 0;
            for(int i = 0; i < lawTiers.Length; i++)
            {
                if(lawTiers[i].MinimalLevel > level) 
                {
                    break;
                }
                else 
                {
                    availableTiers.Add(lawTiers[i]);
                    totalWeight += lawTiers[i].Weight;
                }
            }
            if(availableTiers.Count == 0) return null;
            int randomResult = Random.Range(0, totalWeight);
            for(int i = 0; i < availableTiers.Count; i++)
            {
                if(randomResult <= availableTiers[i].Weight) return availableTiers[i]; 
                else randomResult -= availableTiers[i].Weight;
            }
            return availableTiers[availableTiers.Count - 1];
        }

        private void SpawnLaw()
        {
            if(Random.Range(1, 101) <= _emptyChance) return;
            LawContentSO lawContent = ChooseRandomLawContent();
            if(lawContent == null) return;
            var law = Instantiate(_law, _lawsPoint.position, Quaternion.identity, _lawsPoint).GetComponent<Law>();
            law.Initialize(lawContent);
        }
    }
}