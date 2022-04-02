using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Protesters;

namespace City
{
    public class Region : MonoBehaviour
    {
        [SerializeField]
        private Material _regionColor;
        [SerializeField]
        private List<Square> _squares;
        [SerializeField]
        private RevolutionStage[] _stages;
        private RevolutionBar _revolutionBar;
        private int _curStageIndex;
        private bool _revolutionStarted;

        private void Awake()
        {
            foreach(Square square in _squares)
                square.GetComponent<MeshRenderer>().material = _regionColor;
            _revolutionBar = FindObjectOfType<RevolutionBar>();
            _revolutionBar.RevolutionLevelChanged += ChangeStage;
            _revolutionStarted = false;
            ChangeStage(_revolutionBar.Level);
        }

        private void ChangeStage(float level)
        {
            if(_stages[0].MinRevolutionLevel > level)
            {
                _curStageIndex = -1;
                StopAllCoroutines();
                _revolutionStarted = false;
                return;
            }
            for(int i = 0; i < _stages.Length; i++)
            {
                if(_stages[i].MinRevolutionLevel > level)
                {
                    _curStageIndex = i - 1;
                    if(_revolutionStarted == false)
                    {
                        StartCoroutine(RevolutionDelay());
                        _revolutionStarted = true;
                    }
                    return;
                }
            }
            _curStageIndex = _stages.Length - 1;
            if(_revolutionStarted == false) 
            {
                StartCoroutine(RevolutionDelay());
                _revolutionStarted = true;
            }
        }

        private IEnumerator RevolutionDelay()
        {
            var stage = _stages[_curStageIndex];
            float delay = UnityEngine.Random.Range(stage.MinDelay, stage.MaxDelay); 
            yield return new WaitForSeconds(delay);
            
        }
    }

    [Serializable]
    public class RevolutionStage
    {
        [SerializeField]
        private int _minRevolutionLevel;
        [SerializeField]
        private float _minDelay;
        [SerializeField]
        private float _maxDelay;
        [SerializeField]
        private int _minNumber;
        [SerializeField]
        private int _maxNumber;
        [SerializeField]
        private int _minPower;
        [SerializeField]
        private int _maxPower;

        public int MinRevolutionLevel => _minRevolutionLevel;
        public float MinDelay => _minDelay;
        public float MaxDelay => _maxDelay;
        public int MinNumber => _minNumber;
        public int MaxNumber => _maxNumber;
        public int MinPower => _minPower;
        public int MaxPower => _maxPower;
    }
}