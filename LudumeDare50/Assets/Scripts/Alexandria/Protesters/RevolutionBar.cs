using UnityEngine;
using UnityEngine.UI;

namespace Protesters
{
    public delegate void ValueChanged(float value);

    public class RevolutionBar : MonoBehaviour
    {
        public event EventHappend RevolutionLevelMaximum;
        public event ValueChanged RevolutionLevelChanged;
        [SerializeField]
        private Slider _bar;
        [SerializeField]
        private float _moodСhanging;

        public float Level => _bar.value;
        public float MoodChanging => _moodСhanging;

        private void FixedUpdate() 
        {
            ChangeRevolutionLevel(_moodСhanging);
        }

        public void ChangeMoodChanging(float value)
        {
            _moodСhanging += value;
        }

        public void SetMoodChanging(float moodChanging)
        {
            _moodСhanging = moodChanging;
        }

        public void ChangeRevolutionLevel(float value)
        {
            int oldValue = Mathf.FloorToInt(_bar.value);
            _bar.value += value;
            if(_bar.value >= _bar.maxValue)
            {
                RevolutionLevelMaximum?.Invoke();
                return;
            }
            if(_bar.value <= 0) _bar.value = 0;
            if(oldValue != Mathf.FloorToInt(_bar.value)) RevolutionLevelChanged?.Invoke(_bar.value);
        }
    }
}