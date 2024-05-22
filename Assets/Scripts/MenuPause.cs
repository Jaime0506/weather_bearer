using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    private bool gamePused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamePused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        gamePused = true;
        Time.timeScale = 0f;
        menuPause.SetActive(true);
    }

    public void Play()
    {
        gamePused = false;
        Time.timeScale = 1f;
        menuPause.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale = 1f;
        gamePused = false;
        SceneManager.LoadScene("Menu");
    }
}
