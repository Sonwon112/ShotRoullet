using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    public GameObject ViewerDoor;
    public GameObject StreamerDoor;

    private Interactable viewerDoorInteract;
    private Interactable streamerDoorInteract;

    private void Start()
    {
        viewerDoorInteract = ViewerDoor.GetComponent<Interactable>();
        streamerDoorInteract = StreamerDoor.GetComponent<Interactable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch(other.tag)
        {
            case "Player":
                    viewerDoorInteract.Interact();
                break;
            case "ViewerPlayer":
                    viewerDoorInteract.Interact();
                    streamerDoorInteract.Interact();
                break;
        }
    }
}
