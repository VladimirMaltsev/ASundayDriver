using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour {

    public Button buttonGyroControls;
    public Button buttonTapsControls;
    public TextMeshProUGUI textGyro;
    public TextMeshProUGUI textTaps;
    public TextMeshProUGUI textLevel;

    public GameObject levelChooseMenu;
    public GameObject mainMenu;
    public GameObject loadingPage;

    private AsyncOperation async;
    

    void Start () {
        PlayerPrefs.SetInt("Audio", 1);
        PlayerPrefs.SetInt("Games", 5);
        if (!PlayerPrefs.HasKey("Car"))
        {
            PlayerPrefs.SetInt("Car", 0);
        }
            if (!PlayerPrefs.HasKey("IsLock1"))
        {
            PlayerPrefs.SetInt("IsLock1", 1);
            PlayerPrefs.SetInt("IsLock2", 1);
            PlayerPrefs.SetInt("IsLock3", 1);
            PlayerPrefs.SetInt("IsLock4", 1);
        }


        if (!PlayerPrefs.HasKey("Controls"))
            PlayerPrefs.SetString("Controls", ControlType.ButtonsControls.ToString());

        if (PlayerPrefs.GetString("Controls") == ControlType.GyroControls.ToString())
            GyroControlsPressed();
        else
            TapsControlsPressed();

        levelChooseMenu.SetActive(false);

        if (!PlayerPrefs.HasKey("Fireflies"))
        {
            PlayerPrefs.SetInt("Fireflies", 0);
        }
        

        if (!PlayerPrefs.HasKey("CurrentLevel")){
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        SetCurrentLevelText();
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
        Advertisement.Initialize("2582199", false);
    }

    public void SetCurrentLevelText()
    {
        switch (PlayerPrefs.GetInt("CurrentLevel"))
        {
            case 0:
                textLevel.text = "EARTH";
                break;
            case 1:
                textLevel.text = "MARS";
                break;
            case 2:
                textLevel.text = "JUPITER";
                break;
            case 3:
                textLevel.text = "SATURN";
                break;
        }
        
    }

    public void GyroControlsPressed()
    {
        Gyroscope gyro = Input.gyro;
        gyro.enabled = true;
        if (gyro.enabled)
        {
            textGyro.color = new Color32(255, 96, 204, 255);
            textTaps.color = new Color32(88, 88, 88, 255);
            PlayerPrefs.SetString("Controls", ControlType.GyroControls.ToString());
        }
    }

    public void TapsControlsPressed()
    {
        textTaps.color = new Color32(255, 96, 204, 255);
        textGyro.color = new Color32(88, 88, 88, 255);
        PlayerPrefs.SetString("Controls", ControlType.ButtonsControls.ToString());
    }
    
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        ActivateAsync();
        async.allowSceneActivation = false;
    }

    public void ActivateAsync()
    {
        loadingPage.SetActive(true);
        SceneManager.LoadScene(1);
        //async.allowSceneActivation = true;
    }

    public void ActivateLevelMenu()
    {
        levelChooseMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        
        
        levelChooseMenu.SetActive(false);
        mainMenu.SetActive(true);
        SetCurrentLevelText();
    }
}
