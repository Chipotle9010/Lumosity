using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbKey : MonoBehaviour
{
    public OrbLock orbLock;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            orbLock.hasKey = true;
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, orbLock.transform.position);
    }
}
