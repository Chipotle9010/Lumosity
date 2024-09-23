using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Elevator : MonoBehaviour
{
    [Range(0,1)]
    public float progress;
    public Vector2 positionB;
    public float liftSpeed = 0.2f;

    private Vector2 initialPosition;
    private LiftLock Lock;
    public bool isDebug;
    private float timeCount = 0;

    void Awake()
    {
        initialPosition = transform.position;
        Lock = GetComponent<LiftLock>();
    }

    void Update() 
    {
        //if there is no lock, or the activated bool is true. Make the elevator go up and down 
        if (!Lock || (Lock && Lock.activated == true))
        {
            if (!Application.isPlaying) return;

            timeCount += Time.deltaTime * liftSpeed;

            float progress = Mathf.PingPong(timeCount, 1);
            transform.position = initialPosition + Vector2.Lerp(Vector3.zero, positionB, progress);
        }
    }




    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
           collision.transform.parent = transform;
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            collision.transform.parent = null;
        }
        
    }


    private void OnDrawGizmos()
    {
        //Vector2 initialPosition = Application.isPlaying ? worldspace : transform.position;

        if (!Application.isPlaying)
        {
            initialPosition = transform.position;
        }

        Gizmos.color = new Color(1, 0, 0, .3f);
        Gizmos.DrawCube(initialPosition + positionB, transform.localScale);
    }

}
