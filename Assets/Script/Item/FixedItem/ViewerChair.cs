using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewerChair : MonoBehaviour
{
    private Transform ViewerSitPos;
    public GameManager GameManager;
    private void Start()
    {
        ViewerSitPos = transform.Find("ViewerSitPos");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "ViewerPlayer")
        {
            Viewer viewer = other.gameObject.GetComponent<Viewer>();
            viewer.Sit(ViewerSitPos);
            GameManager.startGame();
        }
    }

}
