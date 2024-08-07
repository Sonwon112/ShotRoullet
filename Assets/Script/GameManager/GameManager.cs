using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Streamer streamer;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Player");
        if(objects.Length > 0 ) 
            streamer = objects[0].GetComponent<Streamer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            if(streamer != null)
            {
                streamer.setGameMode(GameMode.PLAY);
            }
        }
    }
}
