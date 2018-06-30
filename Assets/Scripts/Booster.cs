using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {

    public CarControllerGyro ccg;
	

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Car")
        {
            ccg.car_speed += 50;
        }
        this.GetComponent<SpriteRenderer>().color = new Color32(0, 0, 0, 255);
        ccg.GetComponent<Rigidbody2D>().angularDrag += 0.5f;
    }
}
