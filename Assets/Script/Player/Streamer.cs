using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Streamer : Player
{
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        transform.Translate(transform.forward * vertical *speed*Time.smoothDeltaTime);
        transform.Translate(transform.right * horizontal * speed *1.8f* Time.smoothDeltaTime);

    }
}
