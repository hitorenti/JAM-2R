using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYER_STATUS : MonoBehaviour
{
    public Player_Movement pm;
    public Material DP_Material;

    private void Start()
    {
        pm.DashingLeftTime += Pm_DashingLeftTime;
        pm.OnDashing += Pm_OnDashing;
        DP_Material.SetFloat("_Arc1", 0);

    }

    private void Pm_OnDashing(float left)
    {
        float val = (360*(left*100))/100;
        if(val < 361)
        {
            DP_Material.SetFloat("_Arc1", val);

        }
    }

    private void Pm_DashingLeftTime(float left)
    {
        float val = 360-((360 * (left * 100)) / 100);
        if(val > -1)
        {
            DP_Material.SetFloat("_Arc1", val);

        }
    }
}
