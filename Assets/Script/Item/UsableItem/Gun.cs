using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Gun : MonoBehaviour, Interactable
{
    private List<int> bulletList = new List<int>();
    private bool canInteract = false;
    private Target target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBulletList(List<int> bulletList)
    {
        this.bulletList = bulletList.OrderBy(x=>Guid.NewGuid()).ToList();
        
    }

    public void shot()
    {

    }

    public void Interact()
    {
        if (!canInteract) return;
        // 줍기, target 선택 UI 표시
    }
}
