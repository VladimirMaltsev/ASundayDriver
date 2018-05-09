using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManage : MonoBehaviour {

    public bool gameIsOver;
	// Use this for initialization
	void Start () {
        gameIsOver = false; 
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnGUI()
    {
        if (gameIsOver)
        {
            GUIStyle style = GUI.skin.textField;
            style.fontSize = 25;
            style.alignment = TextAnchor.MiddleCenter;
            float hCenter = Screen.height / 2.0f;
            GUI.Label(new Rect(0, hCenter, Screen.width, Screen.height / 5.0f), "Game over!", style);
            style.fontSize = 20;
            if (GUI.Button(new Rect(0, hCenter + Screen.height / 5f + 5, Screen.width / 2.0f, Screen.height / 10f), "Restart", style))
            {
                Application.LoadLevel(Application.loadedLevel);
            }
            if (GUI.Button(new Rect(Screen.width / 2.0f, hCenter + Screen.height / 5f + 5, Screen.width / 2.0f, Screen.height / 10f), "Exit", style))
            {
                Application.Quit();
            }
        }

        if (GUI.Button(new Rect(0, Screen.height - 100, 200, 50), "Restart"))
        {
            Application.LoadLevel(index: Application.loadedLevel);
            
        }
    }
}
