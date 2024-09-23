using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recharge : MonoBehaviour
{
    private PlayerMoveController player;

    public float amount;

    void Update()
    {
        //If the Player's light bar isn't full, it will recharge over time
        if (player && player.lightEnergy <= player.maxLight || player && amount < 0)
        {
            player.lightEnergy += amount * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //detect whether the player is inside the recharge range. if so activate recharge
        if (collision.tag == "Player")
        {
            player = collision.GetComponent<PlayerMoveController>();
        }
    }
    //When the player is outside of the range, the recharge will stop
    private void OnTriggerExit2D(Collider2D collision)
    {
        player = null;
    }
}
