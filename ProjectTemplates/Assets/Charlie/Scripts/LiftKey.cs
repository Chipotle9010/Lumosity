using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftKey : MonoBehaviour
{
    public LiftLock liftLock;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            liftLock.hasKey = true;
            Destroy(gameObject);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(transform.position, liftLock.transform.position);
    }
}
