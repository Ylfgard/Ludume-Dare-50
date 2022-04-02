using UnityEngine;
using UnityEngine.UI;

namespace Protesters
{
    public delegate void ValueChanged(float value);

    public class RevolutionBar : MonoBehaviour
    {
        public event ValueChanged RevolutionLevelChanged;
        [SerializeField]
        private Slider _bar;

        public float Level => _bar.value;

        public void ChangeRevolutionLevel(float value)
        {
            int oldValue = Mathf.FloorToInt(_bar.value);
            _bar.value += value;
            if(oldValue != Mathf.FloorToInt(_bar.value)) RevolutionLevelChanged?.Invoke(_bar.value);
        }
    }
}