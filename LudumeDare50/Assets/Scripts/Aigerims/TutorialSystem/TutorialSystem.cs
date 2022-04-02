using System.Collections;
using UnityEngine;
using TMPro;
using FMODUnity;
using FMOD;

public class TutorialSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDisplay;

    [TextArea(3, 5)]
    [SerializeField] private string[] _sentences;

    [SerializeField] private float _typingSpeed;
    [SerializeField] private GameObject _contunieButton;
    [SerializeField] private GameObject _tutorialPanel;

    [SerializeField] [EventRef] private string text;
    
    private int index;

    private void Start()
    {
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (_textDisplay.text == _sentences[index])
            _contunieButton.SetActive(true);

        if (Input.GetKeyDown(KeyCode.Space))
            GoOnNextSentence();
    }

    IEnumerator Type()
    {
        foreach (char letter in _sentences[index].ToCharArray())
        {
            RuntimeManager.PlayOneShot(text);
            _textDisplay.text += letter;
            yield return new WaitForSeconds(_typingSpeed);
        }
    }

    public void GoOnNextSentence()
    {
        _contunieButton.SetActive(false);

        if (index < _sentences.Length - 1)
        {
            index++;
            _textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
            _tutorialPanel.SetActive(false);
    }
}
