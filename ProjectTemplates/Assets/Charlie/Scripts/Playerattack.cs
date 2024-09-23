using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Playerattack : MonoBehaviour
{
    public List<Enemy> enemies = new List<Enemy>();
    public SpriteRenderer Renderer;

    private Color originalColor;

    private void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        originalColor = Renderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.3f);
            enemies.Add(collision.GetComponent<Enemy>());
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Renderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.05f);
            enemies.Remove(collision.GetComponent<Enemy>());
        }
    }

    // Add an Attack method
    public void Attack()
    {
        for (int i = enemies.Count - 1; i > -1; i--)
        {
            enemies[i].TakeDamage();
        }
        // Short but intermediate way to do it.
        //enemies = enemies.Where(x => x != null).ToList();

    }

}
