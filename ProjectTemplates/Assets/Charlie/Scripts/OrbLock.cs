using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OrbLock : MonoBehaviour
{
    public bool hasKey = false;


    public void CheckLock()
    {
        
        if (hasKey == true)
        {
            Destroy(gameObject);
            hasKey = false;
        }
    }
}
