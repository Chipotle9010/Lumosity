using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class PlayerMoveController : MonoBehaviour
{
    public float moveForce = 10;
    public float jumpForce = 20;
    public float extraGravity = 10;
    public int doublejump = 2;
    public float health = 10;
    public Image uiHealth;
    public float lightEnergy = 30;
    public Image uiLight;
    public float maxLight;
    public Vector2 playerPosition;
    public Transform directionDummy;
    public float maxhealth;
    public List<Transform> contacts = new List<Transform>();
    public List<Enemy> enemies = new List<Enemy>();

    private float h;
    private bool isGrounded;
    private Rigidbody2D rb;
    private bool isJump;
    private int jumpcounter;
    private bool isFacingRight;
    private Playerattack pAttack;
    private Vector2 hitDistance;

    private void Awake()
    {
        hitDistance = directionDummy.position - transform.position;
    }

    void Start()
    {
        maxLight = lightEnergy;
        maxhealth = health;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        playerPosition = transform.position;
        h = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (health < maxhealth && lightEnergy >= 5)
            {
                health++;
                lightEnergy -= 5;
            }
        }
            
        uiLight.fillAmount = lightEnergy / (float)maxLight;
        uiHealth.fillAmount = health / (float)maxhealth;
        if (health == 0)
        {
            Respawn();
        }
        if (enemies.Count > 0)
        {
            TakeDmg();
        }

        if (Input.GetAxisRaw("Horizontal") != 0 && Mathf.Abs(rb.velocity.x) > .1f)
        {
            isFacingRight = rb.velocity.x > 0;
        }

        //float dir = isFacingRight ? hitDistance : -hitDistance;
        Vector2 newHitPosition = (Vector2)transform.position + hitDistance;
        newHitPosition.x = transform.position.x + (isFacingRight ?  -hitDistance.x : hitDistance.x);
        directionDummy.transform.position = newHitPosition;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            pAttack = GetComponentInChildren<Playerattack>();
            pAttack.Attack();
        }
    }


    private void Respawn()
    {
        //get the location of the spawnpoint and move to the location 
        P2DTrggerEvents triggerevent = GetComponent<P2DTrggerEvents>();
            transform.position = triggerevent.spawnPoint;
            health = maxhealth;
            lightEnergy = maxLight;
    }

    private void TakeDmg()
    {
        if (health >= 1)
        {
            health -= Time.deltaTime * 0.1f;
        }
        else if (health <= 1)
        {
            Respawn();
        } 
    }

    private void FixedUpdate()
    {
        isGrounded = GetIsGrounded();

        //if a jump hasnt occured yet, jump. If one jump has occured and there is sufficient amount of light energy, do another jump
        if (isJump && (isGrounded || jumpcounter < doublejump))
        {
            if (jumpcounter == 0)
            {
                rb.velocity += new Vector2(0, jumpForce);
                jumpcounter++;
            }
            else if (jumpcounter == 1 && lightEnergy > 0)
            {
                //Double Jump will only work when there is light energy to be used
                jumpcounter++;
                rb.velocity += new Vector2(0, jumpForce);
                lightEnergy--;
            }
        }

        Vector3 inputVector = new Vector3(h, 0);
        Vector3 force = inputVector * moveForce;
        force.y = rb.velocity.y;

        rb.AddForce(inputVector * moveForce);

        // Apply extra gravity. Maybe you only want this while jumping.
        rb.velocity += Vector2.down * extraGravity * Time.fixedDeltaTime;
        isJump = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemies.Remove(other.GetComponent<Enemy>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckForGroundContact(collision);

        void CheckForGroundContact(Collision2D collision)
        { 

            Vector2 normal = collision.GetContact(0).normal;
            float angle = Vector2.Angle(normal, Vector2.up);

            //Debug.Log($"normal({normal}), angle({angle})");
            if (angle < 20f)
            {
                contacts.Add(collision.transform);
                jumpcounter = 0;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contacts.Remove(collision.transform);
    }

    private bool GetIsGrounded()
    {
        return contacts.Count > 0;
    }

   
}
