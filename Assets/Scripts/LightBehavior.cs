using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBehavior : MonoBehaviour {

    //public GameManage gm;

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Car")
        {
            GetComponent<SpriteRenderer>().enabled = false; 
        }
    }
}
