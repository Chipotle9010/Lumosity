using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionKey : MonoBehaviour
{
    public Door door;
    public Enemy enemy;

    private void OnDestroy()
    {
        //when enemy is destroyed, run CheckLock()
        door.CheckLock();
    }

    private void OnDrawGizmos()
    {
        //draws line between enemy and door 
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, door.transform.position);
    }
}
