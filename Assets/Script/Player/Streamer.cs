using UnityEngine;

public class Streamer : Player
{
    public float speed = 1.0f;
    public float rotateSpeed = 1.0f;
    public GameObject MainCamera;

    private float xRotate, yRotate;
    private bool isOnDoorTrigger = false;
    private bool isSit = false;
    private bool canCameraMove = true;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        mode = GameMode.BEFORE;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mode == GameMode.PLAY && canCameraMove)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            float mouseRotateX = Input.GetAxis("Mouse Y") * Time.smoothDeltaTime * rotateSpeed;
            float mouseRotateY = Input.GetAxis("Mouse X") * Time.smoothDeltaTime * rotateSpeed;
            // 캐릭터 회전 및 카메라 회전
            yRotate = yRotate + mouseRotateY;
            xRotate = xRotate + mouseRotateX;



            if (!isSit)
            {
                xRotate = Mathf.Clamp(xRotate, -90, 90);
                // 앞뒤 좌우 무빙
                transform.Translate(Vector3.forward * vertical * speed * Time.smoothDeltaTime);
                transform.Translate(Vector3.right * horizontal * speed * 0.8f * Time.smoothDeltaTime);
                transform.eulerAngles = new Vector3(0, yRotate, 0);
                MainCamera.transform.eulerAngles = new Vector3(xRotate, MainCamera.transform.eulerAngles.y, 0);
            }
            else
            {
                xRotate = Mathf.Clamp(xRotate, -90, 40);
                yRotate = Mathf.Clamp(yRotate, -90, 90);
                MainCamera.transform.eulerAngles = new Vector3(xRotate, yRotate, 0);
            }



            //Debug.Log(xRotate + ", " + yRotate);

            //Quaternion quat = Quaternion.Euler(new Vector3(0, yRotate, 0));


            if (Input.GetButtonDown("Interact"))
            {
                CallInteract();
            }
        }
    }

    void CallInteract()
    {
        if (!isOnDoorTrigger) return;
        RaycastHit hit;
        Debug.DrawRay(transform.position, MainCamera.transform.forward, Color.green, 10f);
        if (Physics.Raycast(transform.position, MainCamera.transform.forward, out hit, 10f))
        {
            if (hit.collider.gameObject != null)
            {
                GameObject target = hit.collider.gameObject;
                Interactable interact = target.GetComponentInParent<Interactable>();
                if (interact != null) { interact.Interact(); }
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

    public void Sit(Transform sitPos)
    {
        transform.position = sitPos.position;
        isSit = true;
    }

    // ----------- Getter Setter

    public void setCanCameraMove(bool canCameraMove)
    {
        this.canCameraMove = canCameraMove;
    }
}
