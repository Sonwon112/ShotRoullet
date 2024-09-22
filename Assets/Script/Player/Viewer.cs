using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : Player
{
    public Transform[] movingSpot;
    public float speed;
    public float rotateSpeed;
    public Animator animator;

    private bool isMoving = false;
    private int currSpotIndex = 0;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            Vector3 targetPos = new Vector3(movingSpot[currSpotIndex].position.x, transform.position.y, movingSpot[currSpotIndex].position.z);
            //transform.LookAt(targetPos);
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed*Time.smoothDeltaTime);
            if(transform.position.x == targetPos.x && transform.position.z == targetPos.z) {
                moveEnd();
            }
        }
    }

    public void move()
    {
        if (movingSpot.Length < 1 || movingSpot.Length <= currSpotIndex) return;
        isMoving = true;
        animator.SetBool("walk", true);
    }

    void moveEnd()
    {
        isMoving = false;
        currSpotIndex += 1;
        animator.SetBool("walk", false);
    }

    public void Sit(Transform sitPos)
    {
        transform.position = sitPos.position;
        moveEnd();
       
    }
}
