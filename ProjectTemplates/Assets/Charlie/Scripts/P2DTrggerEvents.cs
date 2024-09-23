using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class P2DTrggerEvents : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI txtScore;
    public Vector2 spawnPoint;
    public ParticleSystem particle;


    private void OnTriggerEnter2D(Collider2D collision)
    {
            PlayerMoveController control = GetComponent<PlayerMoveController>();

        if (collision.tag == "Checkpoint")
        {
            Destroy(collision.gameObject);
            spawnPoint = gameObject.transform.position;
        }

        if (collision.tag == "Pitfall")
        {
            transform.position = spawnPoint;
            control.health = 10;
            control.lightEnergy = 30;
        }

       if (collision.tag == "LockedDoor")
        {
           OrbLock orbLock = collision.GetComponent<OrbLock>();

            orbLock.CheckLock();
        }

        if (collision.tag == "Elevator")
        {
            LiftLock liftLock = collision.GetComponent<LiftLock>();
            liftLock?.CheckLock();
        }

        if (collision.tag == "Confetti")
        {
            if (!particle)
            {
                particle = GetComponentInChildren<ParticleSystem>();
            }
            particle.Play();
        }

    }  
}
