using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunray : MonoBehaviour
{
    public ParticleSystem particle;

    private PlayerMoveController player;

    void Update()
    {
        //if the player's light bar isnt full, fill it over time
        if (player && player.lightEnergy <= player.maxLight)
        {
            player.lightEnergy += 1 * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<PlayerMoveController>();
            particle = player.GetComponentInChildren<ParticleSystem>();
            particle.Play();
        } 
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            particle.Stop();
        }
        
    }
}
