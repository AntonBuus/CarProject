using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{


    public GameObject StartButton;
    public GameObject QuitButton;
    public GameObject LevelButtons;
    public GameObject SettingButton;
    public GameObject UISettingsButton;




    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }


    public void RemoveMainUi()
    {
        StartButton.SetActive(false);
        QuitButton.SetActive(false);
        SettingButton.SetActive(false);
        LevelButtons.SetActive(true);
    }

    public void SettingsLoad()
    {
        UISettingsButton.SetActive(true);
        StartButton.SetActive(false);
        SettingButton.SetActive(false);
        QuitButton.SetActive(false);
    }
    

    public void SettingsBack()
    {
        StartButton.SetActive(true);
        QuitButton.SetActive(true);
        SettingButton.SetActive(true);
        UISettingsButton.SetActive(false);
    }
    public void BackButton()
    {
        LevelButtons.SetActive(false);
        StartButton.SetActive(true);
        SettingButton.SetActive(true);
        QuitButton.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
