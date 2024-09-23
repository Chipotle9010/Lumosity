using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftLock : MonoBehaviour
{
    public bool hasKey = false;
    public bool activated = false;
    public Elevator elevator;


    public void CheckLock()
    {

        if (hasKey == true)
        {
            hasKey = false;
            activated = true;
            
        }
    }
}
