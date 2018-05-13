using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {

    private AsyncOperation async;
    // Use this for initialization
    void Start () {
        async = SceneManager.LoadSceneAsync(1);
        async.allowSceneActivation = false;
	}

    // Update is called once per frame
    void Update() {

    }

    public void PlayGame()
    {
        async.allowSceneActivation = true;
    }
}
