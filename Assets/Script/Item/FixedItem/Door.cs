using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Interactable
{
    public AudioClip OpenSound;
    public AudioClip CloseSound;
    

    private const int CLOSE = 0;
    private const int OPEN = 1;
    
    private GameManager gameManager;
    private GameObject Canvas;
    private Animator animator;
    private AudioSource audioSrc;
    private int currState = CLOSE;
    // Start is called before the first frame update
    void Start()
    {
        Canvas = transform.Find("Canvas").gameObject;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        animator = GetComponent<Animator>();
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && currState == CLOSE)
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
        if (audioSrc.isPlaying) return;
        //Open 애니메이션 수행
        switch(currState)
        {
            case CLOSE:
                animator.SetBool("Open", true);
                audioSrc.clip = OpenSound;
                audioSrc.Play();
                currState = OPEN;
                break;
            case OPEN:
                animator.SetBool("Open", false);
                audioSrc.clip = CloseSound;
                audioSrc.Play();
                currState = CLOSE;               
                break;
        }
        Canvas.SetActive(false);
    }

    public void ShowUpDealer()
    {   
        gameManager.ShowUpDealer();
    }
}
