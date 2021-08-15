using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0) || Input.anyKeyDown)
        {
            Debug.Log("Start");
        }       
    }
}
