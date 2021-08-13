using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform indicator;

    /// <summary>
    /// In range 0 to 100
    /// </summary>
    public void ChangePercentage(float per)
    {
        if(per < 100 && per > 0)
        {
            indicator.localScale = new Vector3(per/100,indicator.localScale.y, indicator.localScale.z);
        }
        else if( per < 0 || per == 0)
        {
            indicator.localScale = new Vector3(0, indicator.localScale.y, indicator.localScale.z);

        }
        else
        {
            Debug.LogError("The percentage exceeds the limits of the bar");
        }
    }
}