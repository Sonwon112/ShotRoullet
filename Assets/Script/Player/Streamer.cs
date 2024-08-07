using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streamer : Player
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    float xRotate, yRotate;
    public GameObject MainCamera;

    private bool isOnDoorTrigger = false;

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


            // 앞뒤 좌우 무빙
            transform.Translate(Vector3.forward * vertical * speed * Time.smoothDeltaTime);
            transform.Translate(Vector3.right * horizontal * speed * 0.8f * Time.smoothDeltaTime);

            // 캐릭터 회전 및 카메라 회전
            yRotate = yRotate + mouseRotateY;
            xRotate = xRotate + mouseRotateX;

            xRotate = Mathf.Clamp(xRotate, -90, 90);

            //Debug.Log(xRotate + ", " + yRotate);
            MainCamera.transform.eulerAngles = new Vector3(xRotate, MainCamera.transform.eulerAngles.y, 0);
            //Quaternion quat = Quaternion.Euler(new Vector3(0, yRotate, 0));
            transform.eulerAngles = new Vector3(0, yRotate, 0);

            if(Input.GetButtonDown("Interact"))
            {
                CallInteract();
            }
        }
    }

    void CallInteract()
    {
        if (!isOnDoorTrigger) return;
        RaycastHit hit;
        if(Physics.Raycast(transform.position,transform.forward, out hit, 10f))
        {
            if(hit.collider.gameObject != null)
            {
                GameObject target = hit.collider.gameObject;
                Interactable interact = target.GetComponentInParent<Interactable>();
                if (interact != null){ interact.Interact(); }
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Interact")
        {
            isOnDoorTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interact")
        {
            isOnDoorTrigger = false;
        }
    }

}
