using UnityEngine;
using FMODUnity;
using GameDataKeepers;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _gameOverWindow;
    [SerializeField] [EventRef]
    private string _loseSound;
    private StoragesKeeper _storagesKeeper;

    private void Awake()
    {
        _storagesKeeper = FindObjectOfType<StoragesKeeper>();
        _storagesKeeper.RevolutionBar.RevolutionLevelMaximum += GameOver;
        _gameOverWindow.SetActive(false);
        _storagesKeeper.PauseSystem.ResumeClicked();
    }

    private void GameOver()
    {
        Time.timeScale = 0f;
        RuntimeManager.PlayOneShot(_loseSound);
        _gameOverWindow.SetActive(true);
    }
}
