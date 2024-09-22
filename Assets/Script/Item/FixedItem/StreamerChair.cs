using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class StreamerChair : MonoBehaviour,Interactable
{
    private GameObject Canvas;
    public GameObject Player;
    public GameObject Viewer;

    private bool canSit=false;
    private Transform sitPos;
    private Transform standPos;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = transform.Find("Canvas").gameObject;
        sitPos = transform.Find("SitPos");
        standPos = transform.Find("StandPos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCanSit(bool canSit)
    {
        this.canSit = canSit;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag+", "+ canSit);
        if (other.tag == "Player" && canSit)
        {
            Canvas.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        Canvas.SetActive(false);
    }

    public void Interact()
    {
        Streamer sTmp = Player.GetComponent<Streamer>();
        Viewer vTmp = Viewer.GetComponent<Viewer>();
        if (sTmp == null || vTmp == null) Debug.Log("Streamer is null");
        sTmp.Sit(sitPos);
        vTmp.move();
        canSit = false;
        Canvas.SetActive(false);
    }



}
