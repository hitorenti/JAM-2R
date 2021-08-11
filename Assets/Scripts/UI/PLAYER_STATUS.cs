using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLAYER_STATUS : MonoBehaviour
{
    public Player_Movement pm;

    private void Start()
    {
        pm.DashingLeftTime += Pm_DashingLeftTime;
    }

    private void Pm_DashingLeftTime(float left)
    {
        Debug.Log(left);
    }
}
