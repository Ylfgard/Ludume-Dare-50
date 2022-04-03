using UnityEngine;
using TMPro;
using Laws.Managers;

namespace Laws
{
    public class Law : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _name;
        [SerializeField]
        private TextMeshProUGUI _description;
        private LawContentSO _content;
        [SerializeField]
        private LawContentSO _test;
        private Lawmaker _lawmaker;
        
        private void Start()
        {
            _lawmaker = FindObjectOfType<Lawmaker>();
            Initialize(_test);
        }
        
        public void ActivateLaw()
        {
            _lawmaker.AdoptLaw(_content);
        }

        public void Initialize(LawContentSO influence)
        {
            _content = influence;
            _name.text = _content.Name;
            _description.text = _content.Description;
        }
    }
}