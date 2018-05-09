using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControllerGyro : MonoBehaviour {

    // Editor
    [Header("Скорость движения машины")]
    [SerializeField] private float car_speed;
    public float mouseSensitivityX = 3.5f;
    public float mouseSensitivityY = 3.5f;
    public bool mbLockNHideCursor = false;
    private float cameraRotationX, cameraRotationY;
    
    private Gyroscope gyro;
    private Quaternion phone_rotation;
    private float acceleration;
    private float current_angle_rotation;
    private float angle_range = 90;

    Rigidbody2D car_body;
    public GameManage gm;

    private float score;
    private float previous_gyro_rotation_z;
    private float current_gyro_rotation_z;
    private float inital_phone_rotation_z;

    void Awake()
    {
        score = 0;

        gyro = Input.gyro;
        gyro.enabled = true;

        car_body = GetComponent<Rigidbody2D>();
        inital_phone_rotation_z = GetGyroRotationZ();
        current_angle_rotation = 0;
        previous_gyro_rotation_z = 0;
    }

    void Start()
    {
        gyro = Input.gyro;
        gyro.enabled = true;
    }

    void Update()
    {
        UpdateCurrentAngle();

#if UNITY_EDITOR
        cameraRotationX += UnityEngine.Input.GetAxis("Mouse Y") * mouseSensitivityY;
        cameraRotationY += UnityEngine.Input.GetAxis("Mouse X") * mouseSensitivityX;
        current_angle_rotation = cameraRotationY;
#endif
        
        car_body.AddForce(transform.up * car_speed, ForceMode2D.Impulse);
        car_body.AddTorque(Mathf.Clamp(current_angle_rotation,  -angle_range/2, angle_range/2), ForceMode2D.Impulse);

        UpdateScore();
    }

    public void UpdateCurrentAngle()
    {
        acceleration = gyro.rotationRate.z;
        if (WasRotation())
            current_angle_rotation += acceleration * Time.deltaTime * 360 / Mathf.PI;
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

            car_body.constraints = RigidbodyConstraints2D.FreezeRotation; 
            
}
    }

    public bool WasRotation()
    {
        bool was_rotatiton = false;
        current_gyro_rotation_z = GetGyroRotationZ() - inital_phone_rotation_z;
        if (current_gyro_rotation_z > 180)
            current_gyro_rotation_z -= 360;

        if (Mathf.Abs(previous_gyro_rotation_z - current_gyro_rotation_z) > 0.5)
            was_rotatiton = true;

        previous_gyro_rotation_z = current_gyro_rotation_z;

        return was_rotatiton;
    }

    public float GetGyroRotationZ()
    {
        return (Quaternion.Euler(90f, 0f, 0f) * new Quaternion(gyro.attitude.x, gyro.attitude.y, -gyro.attitude.z, -gyro.attitude.w)).eulerAngles.z;
    }
   
    void OnGUI()
    {
        GUIStyle style = GUI.skin.label;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleLeft;
        
        GUI.Label(new Rect(10, 30, 500, 50), "RotationRateX = " + gyro.rotationRate.x, style);
        GUI.Label(new Rect(10, 50, 500, 50), "RotationRateY = " + gyro.rotationRate.y, style);
        GUI.Label(new Rect(10, 70, 500, 50), "RotationRateZ = " + gyro.rotationRate.z, style);
        GUI.Label(new Rect(10, 100, 500, 50), "Angle = " + current_angle_rotation, style);
        GUI.Label(new Rect(10, 130, 500, 50), "AngleAttitudeZ = " + current_gyro_rotation_z, style);
        style.fontSize = 60;
        style.alignment = TextAnchor.MiddleCenter;
        GUI.Label(new Rect(Screen.width / 2.0f  - 125, 100, 250, 50), ""+(Mathf.Ceil(score) -1), style);

    }
}
