using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public GameInit gameInit;
    public SpriteRenderer car;
    public Sprite delorian;

	void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Car")
        {
            int newLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
            gameInit.InitLevel(newLevel);
            PlayerPrefs.SetInt("CurrentLevel", newLevel);
            string locker = "IsLock" + newLevel;
            PlayerPrefs.SetInt(locker, 0);
            PlayerPrefs.SetInt("Car", 1);
            car.sprite = delorian;
        }
    }
}
