using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streamer : Player
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    float xRotate, yRotate;
    public GameObject MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mode = GameMode.BEFORE;
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == GameMode.PLAY)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float mouseRotateX = Input.GetAxis("Mouse Y") * Time.smoothDeltaTime * rotateSpeed;
            float mouseRotateY = Input.GetAxis("Mouse X") * Time.smoothDeltaTime * rotateSpeed;


            // �յ� �¿� ����
            transform.Translate(Vector3.forward * vertical * speed * Time.smoothDeltaTime);
            transform.Translate(Vector3.right * horizontal * speed * 0.8f * Time.smoothDeltaTime);

            // ĳ���� ȸ�� �� ī�޶� ȸ��
            yRotate = yRotate + mouseRotateY;
            xRotate = xRotate + mouseRotateX;

            xRotate = Mathf.Clamp(xRotate, -90, 90);

            Debug.Log(xRotate + ", " + yRotate);
            MainCamera.transform.eulerAngles = new Vector3(xRotate, MainCamera.transform.eulerAngles.y, 0);
            //Quaternion quat = Quaternion.Euler(new Vector3(0, yRotate, 0));
            transform.eulerAngles = new Vector3(0, yRotate, 0);
        }
    }
}
