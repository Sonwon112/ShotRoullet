using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerDoor_CallViewerMovement : MonoBehaviour
{
    private Viewer Viewer;
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] tmp = GameObject.FindGameObjectsWithTag("ViewerPlayer");
        if(tmp != null)
        {
            Viewer = tmp[0].GetComponent<Viewer>();
        }
    }

    public void CallViewerMovement()
    {
        if (Viewer != null) {
            Viewer.move();
        }
    }
}
