using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {

    public GameObject[] toEnable;
    public GameObject[] toDisable;

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Car")
        {
            foreach (GameObject i in toEnable){
                i.GetComponent<EdgeCollider2D>().isTrigger = false;
            }
            foreach (GameObject i in toDisable)
            {
                i.GetComponent<EdgeCollider2D>().isTrigger = true;
            }
        }
    }
}
