using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

    public Button buttonGyroControls;
    public Button buttonTapsControls;

    private AsyncOperation async;
    // Use this for initialization
    void Start () {
        if (!PlayerPrefs.HasKey("Controls"))
            PlayerPrefs.SetString("Controls", ControlType.GyroControls.ToString());
        
        

        Advertisement.Initialize("2582199", false);
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
	}

    public void GyroControlsPressed()
    {

    }

    public void TapsControlsPressed()
    {
        
    }

   

    public void PlayGame()
    {
        async.allowSceneActivation = true;
    }
}
