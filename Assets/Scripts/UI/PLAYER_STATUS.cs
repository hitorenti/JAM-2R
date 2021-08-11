using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER_STATUS : MonoBehaviour
{
    public Player_Movement pm;
    public Image img;

    private void Start()
    {
        pm.DashingLeftTime += Pm_DashingLeftTime;
        pm.OnDashing += Pm_OnDashing;
    }

    private void Pm_OnDashing(float left)
    {
        img.fillAmount = left;
    }

    private void Pm_DashingLeftTime(float left)
    {
        img.fillAmount = left;
    }
}
