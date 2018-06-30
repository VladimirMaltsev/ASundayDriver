using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Advertisements;

public class GameManage : MonoBehaviour {

    private float score;
    public int fireflies;
    public GameObject saveMenuUI;
    public GameObject levelMenuUI;
    public CarControllerGyro ccg;
    public GameInit gameInit;

    private float bestScore;
    private bool wasDouble;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreBestText;
    public TextMeshProUGUI scoreFireFlies;
    public TextMeshProUGUI scoreFireFliesWhole;
    

    public AudioSource audioManager;
    public Button audioButton;
    public Sprite audioOn;
    public Sprite audioOff;

    private AsyncOperation async;

    void Awake() {
        saveMenuUI.SetActive(false);
        levelMenuUI.SetActive(false);

        score = 0;
        fireflies = 0;
        wasDouble = false;

        if (PlayerPrefs.GetInt("Audio") == 0)
        {
            audioManager.Stop();
            audioButton.GetComponent<Image>().sprite = audioOff;
        }
        else
        {
            audioButton.GetComponent<Image>().sprite = audioOn;
            audioManager.Play();
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (ccg.car_speed > 0)
            UpdateScore();
    }

    public void UpdateScore()
    {
        score = Time.timeSinceLevelLoad;
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                
                ccg.ActivateProtection();
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    private void HandleShowResultFire(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");

                PlayerPrefs.SetInt("Fireflies", PlayerPrefs.GetInt("Fireflies") + 30);
                scoreFireFliesWhole.text = "" + PlayerPrefs.GetInt("Fireflies");
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }

    public void ShowAds()
    {
        var options = new ShowOptions { resultCallback = HandleShowResult };
        Advertisement.Show(options);
        System.Threading.Thread.Sleep(2000);
    }

    public IEnumerator Timer(float seconds)
    {
        float startTime = Time.time;
        if (!saveMenuUI.activeSelf)
        {
            yield break;
        }

        while (Time.time - startTime < seconds) {
            yield return new WaitForSeconds(0.1f);
        }
        if (saveMenuUI.activeSelf)
        {
            
            UpdateTextScore();
            saveMenuUI.SetActive(false);
            levelMenuUI.SetActive(true);
            ccg.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ReloadLevel()
    {
        Time.timeScale = 1f;
        //gameInit.InitLevel(PlayerPrefs.GetInt("CurrentLevel"));
        SceneManager.LoadScene(1);
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        //async.allowSceneActivation = true;
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void Score_x2()
    {
        if (!wasDouble)
        {
            var options = new ShowOptions { resultCallback = HandleShowResultFire };
            Advertisement.Show(options);
            System.Threading.Thread.Sleep(2000);

        }
        UpdateTextScore();
        wasDouble = true;
    }

    public void UpdateTextScore()
    {
        if (PlayerPrefs.HasKey("bestScore")){
            bestScore = PlayerPrefs.GetFloat("bestScore");
        }
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetFloat("bestScore", bestScore);
            PlayerPrefs.Save();
        }
        scoreBestText.text = "best dist " + (Mathf.Ceil(bestScore) - 1);
        scoreText.text = "dist " + (Mathf.Ceil(score) - 1);

        scoreFireFliesWhole.text = "" + PlayerPrefs.GetInt("Fireflies");
    }

    public void AudioButtonListener()
    {
        if (audioManager.mute)
        {
            audioButton.GetComponent<Image>().sprite = audioOn;
            PlayerPrefs.SetInt("Audio", 1);
            audioManager.Play();
        } else
        {
            audioButton.GetComponent<Image>().sprite = audioOff;
            PlayerPrefs.SetInt("Audio", 0);
            audioManager.Stop();
        }
        audioManager.mute = !audioManager.mute;
            
    }

    public void PlusFirefly()
    {
        fireflies += 1;
        scoreFireFlies.SetText("" + fireflies);
    }
    
    public float GetScore()
    {
        return score;
    }

    void OnGUI()
    {
        if (!levelMenuUI.activeSelf)
        {
            GUIStyle style = GUI.skin.label;
            style.fontSize = 30;
            style.alignment = TextAnchor.MiddleLeft;
            /*
            GUI.Label(new Rect(10, 30, 500, 50), "RotationRateX = " + ccg.gyro.rotationRate.x, style);
            GUI.Label(new Rect(10, 60, 500, 50), "RotationRateY = " + ccg.gyro.rotationRate.y, style);
            GUI.Label(new Rect(10, 90, 500, 50), "RotationRateZ = " + ccg.gyro.rotationRate.z, style);
            GUI.Label(new Rect(10, 120, 500, 50), "Angle = " + ccg.current_angle_rotation, style);
            GUI.Label(new Rect(10, 150, 500, 50), "AngleAttitudeZ = " + ccg.current_gyro_rotation_z, style);*/
            style.fontSize = 80;
            style.normal.textColor = Color.white;
            style.alignment = TextAnchor.MiddleCenter;
            GUI.Label(new Rect(Screen.width / 2.0f - 125, 50, 250, 100), "" + (Mathf.Ceil(score) - 1), style);
        }
    }
}
