using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{

    public GameObject MainMenu;
    public GameObject ControlPanel;


    public void Play()
    {
        GameState.Instance.state=State.Playing;
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ShowControl()
    {
        ControlPanel.SetActive(true);
    }

    public void HideControl()
    {
        ControlPanel.SetActive(false);
    }
}
