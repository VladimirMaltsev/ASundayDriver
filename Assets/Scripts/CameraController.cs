using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform target;
    public CarControllerGyro ccg;
	void Start () {
		
	}
   

    private void LateUpdate()
    {
        Vector3 newPos = target.position;
        newPos -= target.right * ccg.current_angle_rotation / 10;
        newPos.z = transform.position.z;

        transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);
        transform.rotation = target.rotation;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
