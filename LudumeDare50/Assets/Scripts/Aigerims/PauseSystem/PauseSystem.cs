using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class PauseSystem : MonoBehaviour
{
    public static event Action OnPauseClicked;
    [SerializeField] private GameObject _pausePanel;
    private bool _isPaused;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPaused)
                PauseClicked();
            else
                ResumeClicked();
        }
    }
    public void PauseClicked()
    {
        OnPauseClicked?.Invoke();
        _isPaused = true;
        _pausePanel.SetActive(_isPaused);
        Time.timeScale = 0f;
    }

    public void ResumeClicked()
    {
        _isPaused = false;
        _pausePanel.SetActive(_isPaused);
        Time.timeScale = 1f;
    }


    public void GoToMainMenu()
    {
        //PlayerPrefs.SetInt("NewGameStarted", 1);
        SceneManager.LoadScene("Menu");
    }
}
