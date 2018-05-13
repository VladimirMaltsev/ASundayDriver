using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManage : MonoBehaviour {

    private float score;
    public GameObject saveMenuUI;
    public GameObject levelMenuUI;
    public CarControllerGyro ccg;

    private float bestScore;
    private bool wasDouble;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI scoreBestText;

    public static string levelNamep;

    public AudioSource audio;
    public Button audioButton;
    public Sprite audioOn;
    public Sprite audioOff;

    void Awake() {
        saveMenuUI.SetActive(false);
        levelMenuUI.SetActive(false);

        score = 0;
        wasDouble = false;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (ccg.car_speed > 0)
            UpdateScore();
    }

    public void UpdateScore()
    {
        score += 0.1f;
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
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitApplication()
    {
        Application.Quit();
    }

    public void Score_x2()
    {
        if (!wasDouble) 
            score = (Mathf.Ceil(score) - 1) * 2 + 0.001f;
        UpdateTextScore();
        wasDouble = true;
    }

    public void UpdateTextScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
        }
        scoreBestText.text = "best score " + (Mathf.Ceil(bestScore) - 1);
        scoreText.text = "your score " + (Mathf.Ceil(score) - 1);
    }

    public void AudioButtonListener()
    {
        if (audio.mute)
        {
            audioButton.GetComponent<Image>().sprite = audioOn;
        } else
        {
            audioButton.GetComponent<Image>().sprite = audioOff;
        }
        audio.mute = !audio.mute;
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
