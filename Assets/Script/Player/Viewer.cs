using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer : Player
{
    public Transform[] movingSpot;
    public float speed;
    public float rotateSpeed;

    private bool isMoving = false;
    private int currSpotIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
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
                isMoving = false;
                currSpotIndex+=1;
            }
        }
    }

    public void move()
    {
        if (movingSpot.Length < 1 || movingSpot.Length <= currSpotIndex) return;
        isMoving = true;
    }
}
