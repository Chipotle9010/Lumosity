using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int lockCount = 1;

    private int keyCount;

    public void CheckLock()
    {
        keyCount++;
        if (lockCount <= keyCount)
        {
            //when the amount of keys (keyCount) is the same or greater than the lockCount, destroy the door
            Destroy(gameObject);
        }
    }
}
