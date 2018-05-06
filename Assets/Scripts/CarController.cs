using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    // Editor
    [Header("Скорость движения машины")]
    [SerializeField] private float car_speed;
    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;
    public bool mbLockNHideCursor = false;
    private float cameraRotationX, cameraRotationY;

    private enum SnapTo { WorldAxis, InitialRotation };

    private Gyroscope gyro;
    private Quaternion gyro_rotation;
    private Quaternion world_rotation;

    private Quaternion initial_object_rotation;
    private float snap_to_point_offset_rotation = 0f;

    Rigidbody2D car_body;
    public GameManage gm;

    private float score;

    void Awake()
    {
        score = 0;

        gyro = Input.gyro;
        gyro.enabled = true;

        initial_object_rotation = transform.rotation;
        UpdateGyroRotation();

        car_body = GetComponent<Rigidbody2D>();

        //transform.Rotate(Vector3.right, -initial_object_rotation.eulerAngles.x * 2f);
    }

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void Update()
    {
        UpdateGyroRotation();
        //SnapToPoint();
#if UNITY_EDITOR
        cameraRotationX += UnityEngine.Input.GetAxis("Mouse Y") * mouseSensitivityY;
        cameraRotationY += UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivityX;
        gyro_rotation = Quaternion.Euler(0f, 0f, cameraRotationY);
#endif

        world_rotation = Quaternion.Euler(0f, 0f, snap_to_point_offset_rotation) * gyro_rotation;
        //transform.rotation = Quaternion.Euler(0f, 0f, world_rotation.eulerAngles.z);
        float angle = world_rotation.eulerAngles.z;
        angle = angle > 180 ? angle - 360 : angle;
        if (angle >= 90 && angle <= 180)
        {
            angle = 180 - angle;
        } else if (angle >= -180 && angle <= -90)
        {
            angle = -180 - angle;
        }
        
        //transform.Rotate(0f, 0f, angle / 100, Space.Self);
        car_body.AddForce(transform.up * car_speed, ForceMode2D.Impulse);
        car_body.AddTorque(Mathf.Clamp (angle, -45f, 45f), ForceMode2D.Impulse);

        UpdateScore();
    }

    void UpdateGyroRotation()
    {
        gyro_rotation = Quaternion.Euler(90f, 0f, 0f) * new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w); ;
    }

    public void SnapToPoint()
    {
        snap_to_point_offset_rotation = -gyro_rotation.eulerAngles.z;
    }

    public void UpdateScore()
    {
        if (!gm.gameIsOver)
        {
            score += Time.deltaTime;
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Border")
        {
            gm.gameIsOver = true;
            car_speed = 0;
            //gyro.enabled = false;
            car_body.constraints = RigidbodyConstraints2D.FreezeRotation; 

            //mouseSensitivityX = 0f;
            //mouseSensitivityY = 0f;
}
    }

    void OnGUI()
    {
        GUIStyle style = GUI.skin.label;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleLeft;

        GUI.Label(new Rect(10, 10, 200, 50), "AngleX = " + world_rotation.eulerAngles.x, style);
        GUI.Label(new Rect(10, 30, 200, 50), "AngleY = " + world_rotation.eulerAngles.y, style);
        GUI.Label(new Rect(10, 50, 200, 50), "AngleZ = " + world_rotation.eulerAngles.z, style);
        GUI.Label(new Rect(10, 70, 200, 50), "snap = " + snap_to_point_offset_rotation, style);


        GUI.Label(new Rect(10, 130, 500, 50), "RotationRateX = " + gyro.rotationRate.x, style);
        GUI.Label(new Rect(10, 150, 500, 50), "RotationRateY = " + gyro.rotationRate.y, style);
        GUI.Label(new Rect(10, 170, 500, 50), "RotationRateZ = " + gyro.rotationRate.z, style);

      
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(Screen.width / 2.0f  - 125, 100, 250, 50), ""+(Mathf.Ceil(score) -1), style);

    }
}
