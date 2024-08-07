using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    protected int HP;
    protected GameMode mode;

    public void setTargetAndShoot()
    {

    }

    public void setGameMode(GameMode mode)
    {
        this.mode = mode;
    }

    public GameMode getGameMode()
    {
        return this.mode;
    }
}
