using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManage : MonoBehaviour {

    private float score;
    public GameObject saveMenuUI;
    public CarControllerGyro ccg;

    public static string levelNamep;

	void Awake () {
        saveMenuUI.SetActive(false);
        score = 0;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (ccg.car_speed > 0)
            UpdateScore();
	}

    public void UpdateScore()
    {
            score += 0.1f;
    }


    public void ReloadLevel()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(1);
    }

    void OnGUI()
    {
        GUIStyle style = GUI.skin.label;
        style.fontSize = 30;
        style.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(10, 30, 500, 50), "RotationRateX = " + ccg.gyro.rotationRate.x, style);
        GUI.Label(new Rect(10, 60, 500, 50), "RotationRateY = " + ccg.gyro.rotationRate.y, style);
        GUI.Label(new Rect(10, 90, 500, 50), "RotationRateZ = " + ccg.gyro.rotationRate.z, style);
        GUI.Label(new Rect(10, 120, 500, 50), "Angle = " + ccg.current_angle_rotation, style);
        GUI.Label(new Rect(10, 150, 500, 50), "AngleAttitudeZ = " + ccg.current_gyro_rotation_z, style);
        style.fontSize = 80;
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(Screen.width / 2.0f - 125, 50, 250, 100), "" + (Mathf.Ceil(score) - 1), style);
    }
}
