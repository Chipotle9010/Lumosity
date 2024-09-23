using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class Enemy : MonoBehaviour
{
    public float patrolDistance = 5f;
    public int health;
    public bool isDebug;
    public float enemySpeed = 2;
    public Vector2 maxPosition;
    public bool abort;
    public SpriteRenderer spr;
    public Color dmgColor;
    public ParticleSystem particle;


    private Color originalColor;
    //private float initialSpeed;
    private Vector2 alternatePosition;
    private Vector2 initialPosition;
    private int maxhealth = 5;
    private PlayerMoveController player;
    //private Door door;
    //private GameObject tutorialEnemy;
    //private ConfettiScript tutorial;


    // Start is called before the first frame update
    void Awake()
    {
        if (!Application.isPlaying) return;
        initialPosition = transform.position;
         health = maxhealth;
        UpdateAlternatePosition();
        maxPosition.x = initialPosition.x + patrolDistance;
    }

    private void Start()
    {

        player = FindAnyObjectByType<PlayerMoveController>();
        //initialSpeed = enemySpeed;

        spr = GetComponent<SpriteRenderer>();
        originalColor = spr.color;


    }

    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying) return;

        if (Vector2.Distance (transform.position, alternatePosition) < 1f )
        {
          UpdateAlternatePosition();
        }
        if (!Application.isPlaying) return;

        if (Vector2.Distance(initialPosition, player.playerPosition) >= patrolDistance)
        {
            //when far from player, patrol
            Patrol();
        }
        else if (Vector2.Distance(initialPosition, player.playerPosition) <= patrolDistance)
        {
            //when close to player, attack
            Attack();
        }


        spr.color = new Color(
            Mathf.MoveTowards(spr.color.r, originalColor.r, Time.deltaTime),
            Mathf.MoveTowards(spr.color.g, originalColor.g, Time.deltaTime),
            Mathf.MoveTowards(spr.color.b, originalColor.b, Time.deltaTime)
        );
    }

    private void Patrol()
    {
        //Enemy will move towards alternatePosition over time using enemy speed
        transform.position = Vector2.MoveTowards(transform.position, alternatePosition, Time.deltaTime * enemySpeed);
    }

    private void Attack()
    {
        Vector2 playerPosition = player.playerPosition;
         if (Vector2.Distance(transform.position, playerPosition) > 0.9f)
        {
        playerPosition.y = initialPosition.y;
            //Enemy will move towards the player
        transform.position = Vector2.MoveTowards(transform.position, playerPosition, Time.deltaTime * enemySpeed);
        }
     }




    private void UpdateAlternatePosition()
    {
        alternatePosition.y = initialPosition.y;
        if (Mathf.Approximately(alternatePosition.x, initialPosition.x + patrolDistance))
        {
            alternatePosition.x = initialPosition.x - patrolDistance;
        }
        else
        {
            alternatePosition.x = initialPosition.x + patrolDistance;
        }
    }

    private void OnDrawGizmos()
    {
        //Vector2 initialPosition = Application.isPlaying ? worldspace : transform.position;

        if (!Application.isPlaying)
        {
            initialPosition = transform.position;
        }
        Gizmos.color = new Color(0, 0, 1, .3f);
        Gizmos.DrawLine(initialPosition, initialPosition + (Vector2.right * patrolDistance));
        Gizmos.DrawLine(initialPosition, initialPosition - (Vector2.right * patrolDistance));
        Gizmos.DrawWireSphere(initialPosition, 1);
    }

    // Add a method to TakeDamage(int _maybeHowMuchDamage)
    // do damage

    public void TakeDamage()
    {
        spr.color = dmgColor;
        particle = player.transform.Find("EnemyParticle").GetComponentInChildren<ParticleSystem>();
        particle.Play();
        health--;
        CheckDeath();
    }      

    private void CheckDeath()
    {
        if (health <= 0) {
            Destroy(gameObject);
        } 
    }


}
