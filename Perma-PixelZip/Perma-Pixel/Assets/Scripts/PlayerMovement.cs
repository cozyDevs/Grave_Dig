//Milo Wilson
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    //Calling the DeathCounter Script
    [SerializeField]
    private DeathCounter deathCounter;
    [SerializeField]
    private float speed, speedLimit;
    [SerializeField]
    private Transform playerTransform;
    [SerializeField]
    Rigidbody2D RB;
    [SerializeField]
    float mineTime, timeToBreak, attackTime, attackCooldown, mineDamage;
    [SerializeField] 
    int damage;
    [SerializeField]
    private InteractPoint interactBox;
    [SerializeField]
    private BlockBreaker pickBox;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    Animator animator;
    [SerializeField]
    private bool dying, hasControl;
    [SerializeField]
    private PlayerHealth playerHealth;
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private GameObject body;
    [SerializeField]
    private AudioManager audioManager;

    void Start()
    {
        //assigns variables
        playerHealth = GetComponent<PlayerHealth>();
        playerTransform = this.GetComponent<Transform>();
        interactBox = GetComponentInChildren<InteractPoint>();
        interactBox.setDamage(damage);
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        //Calling the death wrapper everytime the player dies.
        // deathCounter = GameObject.FindGameObjectWithTag("DeathScreen").GetComponent<DeathCounter>();
        //  deathCounter.CallOnDeath(gameObject);
    }

    void FixedUpdate()
    {
        if (hasControl)
        {
            //sees what axis is wantinf to move
            float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");
            //Reduces attack cooldown
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0)
            {
                attackCooldown = -1;
                interactBox.StopAttacking();
                animator.SetBool("Attacking", false);
            }
            else
            {
                animator.SetBool("Moving", false);
                animator.SetBool("Mining", false);
                animator.SetBool("Attacking", true);
            }
            //Attack is LMB
            //attacks if cooldown is at or below 0
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (attackCooldown <= 0)
                {
                    audioManager.PlayAt(3);
                    animator.SetBool("Moving", false);
                    animator.SetBool("Mining", false);
                    animator.SetBool("Attacking", true);
                    Debug.Log("attack");
                    attackCooldown = attackTime;
                    interactBox.Attack();

                }
            }
            else if (Input.GetKey(KeyCode.Mouse1))//[this is where mining happpens] Mine is RMB
            {
                //counts up mineing time if the player is mineing
                animator.SetBool("Moving", false);
                animator.SetBool("Mining", true);
                animator.SetBool("Attacking", false);
                //increases time if player is holdong down buton
                mineTime += Time.deltaTime;
                //stops movement if mining
                xAxis = 0;
                yAxis = 0;
                audioManager.PlayAt(1);
                if (timeToBreak < mineTime)
                {
                    Debug.Log("mine");
                    mineTime = 0;
                    interactBox.DamageBlock(mineDamage);
                }
            }
            else
            {
                //sets mine time to zero
                mineTime = -1;
                animator.SetBool("Mining", false);
            }

            if (xAxis >= .1 || yAxis >= .1 || xAxis <= -.1 || yAxis <= -.1)
            {
                if(!particleSystem.isPlaying)
                    particleSystem.Play();
                audioManager.PlayAt(4);
                //moves player
                if (RB.velocity.x < speedLimit || RB.velocity.y < speedLimit)
                {
                    RB.AddForce(new Vector2(xAxis * speed, yAxis * speed));
                    RB.velocity = Vector2.ClampMagnitude(RB.velocity, speedLimit);
                }
                if(attackCooldown <= 0)
                    animator.SetBool("Moving", true);

                //moves interact box and sets animation direction
                if (xAxis > 0)
                {
                    //move Right
                    animator.SetFloat("X", 1);
                    animator.SetFloat("Y", 0);
                    interactBox.MoveRight();
                }
                else if (xAxis < 0)
                {
                    //move Left
                    animator.SetFloat("X", -1);
                    animator.SetFloat("Y", 0);
                    spriteRenderer.flipX = true;
                    interactBox.MoveLeft();
                }
                else if (yAxis > 0)
                {
                    //move Up
                    animator.SetFloat("X", 0);
                    animator.SetFloat("Y", 1);
                    interactBox.MoveUp();
                }
                else if (yAxis < 0)
                {
                    //move Down
                    animator.SetFloat("X", 0);
                    animator.SetFloat("Y", -1);
                    interactBox.MoveDown();
                }
            }
            else if (mineTime <= -1 && attackCooldown <= 0)
            {
                //idle
                animator.SetBool("Moving", false);
                animator.SetBool("Mining", false);
                animator.SetBool("Attacking", false);
            }

            //to be added pause menu
            if (Input.GetKey(KeyCode.Escape))
            {
                OpenPauseMenu();
            }
        }
        else
        {
            if (dying)
            {
                audioManager.PlayAt(0);
                audioManager.StopPlayAt(1);
                audioManager.StopPlayAt(2);
                audioManager.StopPlayAt(3);
                audioManager.StopPlayAt(4);
                animator.SetBool("Moving", false);
                animator.SetBool("Mining", false);
                animator.SetBool("Attacking", false);
                animator.SetBool("Dying", true);
            }
        }
    }

    void OpenPauseMenu()
    {
        Debug.Log("Add pause menu");
    }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.isTrigger == true && other.CompareTag("block"))
		{
            other.GetComponent<BlockBreaker>().Picked(damage);
		}
	}
    public void LosePlayerControl()
    {
        hasControl = false;
    }

    public void GainPlayerControl()
    {
        hasControl = true;
    }

    public void PlayerDies()
    {
        LosePlayerControl();
        playerHealth.StopTrackingHealth();
        dying = true;
        deathCounter.CallOnDeath(this.gameObject);
    }

    public void ResetPlayer(bool spawnBody)
    {
        dying = false;
        animator.SetBool("Dying", false);
        playerHealth.ResetHealth();
        if (spawnBody)
            Instantiate(body, transform.position, Quaternion.identity);
        GameObject.Find("FloorGen").GetComponent<FloorGenerator>().PlayerToSpawn();

    }

    public void WinGame()
    {
        LosePlayerControl();
        playerHealth.StopTrackingHealth();
        GameObject.Find("UIOverlay").GetComponent<WinAnimation>().YouWin();
    }
}
