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
        

        Advertisement.Initialize("2582199", false);

        if (!PlayerPrefs.HasKey("CurrentLevel")){
            PlayerPrefs.SetInt("CurrentLevel", 1);
        }
        SetCurrentLevelText();
        //async = SceneManager.LoadSceneAsync(PlayerPrefs.GetInt("CurrentLevel"));
        //async.allowSceneActivation = false;
	}

    public void SetCurrentLevelText()
    {
        textLevel.text = "level " + (PlayerPrefs.GetInt("CurrentLevel")+1) + " >";
    }

    public void GyroControlsPressed()
    {
        Gyroscope gyro = Input.gyro;
        gyro.enabled = true;
        if (gyro.enabled)
        {
            textGyro.color = new Color32(255, 96, 204, 255);
            textTaps.color = new Color32(0, 0, 0, 255);
            PlayerPrefs.SetString("Controls", ControlType.GyroControls.ToString());
        }
    }

    public void TapsControlsPressed()
    {
        textTaps.color = new Color32(255, 96, 204, 255);
        textGyro.color = new Color32(0, 0, 0, 255);
        PlayerPrefs.SetString("Controls", ControlType.ButtonsControls.ToString());
    }
    
    public void PlayGame()
    {
        loadingPage.SetActive(true);
        mainMenu.SetActive(false);
        SceneManager.LoadScene(1);
        //async.allowSceneActivation = false;
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
