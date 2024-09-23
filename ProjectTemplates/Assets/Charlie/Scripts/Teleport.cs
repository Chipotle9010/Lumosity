using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector2 newLocation;

    private PlayerMoveController player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<PlayerMoveController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.transform.position = newLocation;
            player.lightEnergy = player.maxLight;
            player.health = player.maxhealth;
        }
    }
}
