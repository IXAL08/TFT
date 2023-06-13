using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject WinnerScreen;

    public void Pause()
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void Options()
    {
        optionsMenu.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void Resume()
    {
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Guardar()
    {
        optionsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }
    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Juego()
    {
        SceneManager.LoadScene(1);
    }

    public void Creditos()
    {
        creditsMenu.SetActive(true);
    }
    
    public void regresar()
    {
        creditsMenu.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            WinnerScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
