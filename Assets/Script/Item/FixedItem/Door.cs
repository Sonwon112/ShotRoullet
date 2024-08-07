using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    private const int CLOSE = 0;
    private const int OPEN = 1;
    
    private GameObject Canvas;
    private Animator animator;
    private int currState = CLOSE;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = transform.Find("Canvas").gameObject;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Canvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Canvas.SetActive(false);
        }
    }

    public void Interact()
    {
        //Open 애니메이션 수행
        switch(currState)
        {
            case CLOSE:
                animator.SetBool("Open", true);
                currState = OPEN;
                break;
            case OPEN:
                animator.SetBool("Open", false);
                currState = CLOSE;
                break;
        }
            
        
    }
}
