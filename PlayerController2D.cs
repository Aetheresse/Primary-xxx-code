using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController2D : MonoBehaviour
{

    [SerializeField] private AudioClip move;
    [SerializeField] private AudioClip punch;
    [SerializeField] private AudioClip punchB;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip flyKick;
    [SerializeField] private AudioClip intro;
    [SerializeField] private AudioClip playerLoos;
    [SerializeField] private AudioClip powerSfx;
    



    AudioManager amanager;
    public Animator panimator;
    public Transform attackPoint;
    public Transform kickPoint;
    public Transform enemy;
    public Transform powerPoint;
    public LayerMask enemyLayers;
    public SpriteRenderer playerFlip;
    public GameObject powerPrefab;
    public float attackRange;
    public float kickRange;
    public float attackRate = 2f;
    float firerate;
    float maxHealth = 100;
    float maxPower = 100;
    float currentHealth;
    float currentPower;
    float nextAttackTime = 0f;
    float powerRefill = 0f;


    Rigidbody2D rb2D;
    BoxCollider2D bc2D;
    

    [SerializeField]
    Transform groundCheckp;

    [SerializeField]
    Transform groundCheckpb;

    bool kk;
    bool isGrounded;
    bool power = false;
    bool playOngame;
    bool playHome;
    bool sower;
    
    int P = 0;
    

    [Header("Unity Staff")]
    public Image pHealthbar; 
    public Image pPowerbar;


    void Start()
    {
        panimator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        amanager = GetComponent<AudioManager>();
        playerFlip = GetComponent<SpriteRenderer>();
        AudioManager.Instance.PlayMusic(intro);
        currentHealth = maxHealth;
        currentPower = maxPower;
        playOngame = true;
        firerate = 0;
        powerRefill = 0f;
        playHome = true;
        sower = false;
        playerFlip.flipX = false;

        enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    
    private void FixedUpdate()
    {

        if( Enemyone.flip == true)
        {
            playerFlip.flipX = true;
        }
        else
            playerFlip.flipX = false;

        if (GameManager.gameState == GameManager.GameState.DEATH)
        {
          


            
            playOngame = true;
            playHome = true;
        }

        if (GameManager.gameState == GameManager.GameState.PLAY)
        {

            P = Random.Range(0, 4);

            if (currentPower < 99 && Time.time >= powerRefill)
            {
                if (currentPower <= 0)
                    currentPower = 0;

                currentPower = currentPower + .5f;
                powerRefill = Time.time + 1f;
                pPowerbar.fillAmount = currentPower / maxPower;

            }

            if (Input.GetKey("escape"))
            {
                GameManager.gameState = GameManager.GameState.PAUSE;
            }

            if (playOngame)
             AudioManager.Instance.PlayMusicWithCrossFade(move,.5f);
               
            playOngame = false;
            
            playHome = true;

            if ( Input.GetKey("up") && isGrounded == true)
            {
                rb2D.velocity = new Vector2(rb2D.velocity.x, 20);
                panimator.Play("Jump");
                AudioManager.Instance.PlaySFX(jump);

            }
            else if(Input.GetKey("space") && isGrounded == true && currentPower >= 11)
            {
                if( Time.time >= firerate)
                {
                SuperPower();
                    firerate = Time.time + 1f;

                    currentPower -= 30;
                    pPowerbar.fillAmount = currentPower / maxPower;

                }
            }

            else if (Input.GetKey("down") && Input.GetKey("left"))
            {

                power = true;
                panimator.Play("Super");
                //   AudioManager.Instance.PlaySFX(downMove);
                rb2D.velocity = new Vector2(-15, rb2D.velocity.y);
            }

            else if (Input.GetKey("down") && Input.GetKey("right"))
            {
                power = true;
                panimator.Play("Super");
                // AudioManager.Instance.PlaySFX(downMove);
                rb2D.velocity = new Vector2(15, rb2D.velocity.y);
            }

            if (Input.GetKey("down") && isGrounded == true)
            {

                panimator.Play("Down");
                if (Enemyone.shootAngle >= -4 && Enemyone.shootAngle <= -2)
                {
                   // rb2D.gravityScale = 4;
                    bc2D.isTrigger = false;
                }
                else
                {
                   // rb2D.gravityScale = 0;
                    bc2D.isTrigger = true;
                }



            }
            else
            {
               // rb2D.gravityScale = 4;
                bc2D.isTrigger = false;

            }
                
            if (Input.GetKey("right"))
            {
                if (power == false)
                    rb2D.velocity = new Vector2(20, rb2D.velocity.y);

                if (playerFlip.flipX == false)
                {

                   

                    if (isGrounded)
                    {

                        if (Input.GetKey("f"))
                        {

                            if (P == 0)
                            {
                                if (Time.time >= nextAttackTime)
                                {
                                    Kick();
                                    nextAttackTime = Time.time + 1f / attackRate;


                                }

                                // AudioManager.Instance.PlaySFX(punch);
                            }
                            else if (P == 1)
                            {

                                if (Time.time >= nextAttackTime)
                                {

                                    Punch();

                                    nextAttackTime = Time.time + 1f / attackRate;
                                }


                                //  AudioManager.Instance.PlaySFX(punchB);
                            }

                            else if (P == 2)
                            {
                                if (Time.time >= nextAttackTime)
                                {
                                    Longpunch();


                                    nextAttackTime = Time.time + 1f / attackRate;
                                }

                                // AudioManager.Instance.PlaySFX(flyKick);



                            }


                        }

                        else
                        {


                            panimator.Play("player_run");
                            //  AudioManager.Instance.PlaySFX(move);


                        }

                    }

                    else
                    {
                        if (Input.GetKey("f"))
                        {
                            if (Time.time >= nextAttackTime)
                            {
                                if (sower == false)
                                    AudioManager.Instance.PlaySFX(flyKick);

                                sower = true;
                                Flykick();
                                //  rb2D.velocity = new Vector2(30, rb2D.velocity.y);
                                nextAttackTime = Time.time + 1f / attackRate;
                            }

                        }
                    }

                }

                else
                {
                    {
                        


                        if (isGrounded)
                        {
                            panimator.Play("Playerdefend");
                            if (Enemyone.shootAngle >= -4 && Enemyone.shootAngle <= -2)
                            {
                               // rb2D.gravityScale = 4;
                                bc2D.isTrigger = false;
                            }
                            else
                            {
                              //  rb2D.gravityScale = 0;
                                bc2D.isTrigger = true;
                            }
                            // AudioManager.Instance.PlaySFX(move);
                        }
                        else
                        {
                          //  rb2D.gravityScale = 4;
                            bc2D.isTrigger = false;
                        }

                    }
                }



            }

            else if (Input.GetKey("left"))
            {
                if (power == false)
                    rb2D.velocity = new Vector2(-20, rb2D.velocity.y);

                if (playerFlip.flipX == true)
                {

                    

                    if (isGrounded)
                    {

                        if (Input.GetKey("f"))
                        {

                            if (P == 0)
                            {
                                if (Time.time >= nextAttackTime)
                                {
                                    Kick();
                                    nextAttackTime = Time.time + 1f / attackRate;


                                }

                                // AudioManager.Instance.PlaySFX(punch);
                            }
                            else if (P == 1)
                            {

                                if (Time.time >= nextAttackTime)
                                {

                                    Punch();

                                    nextAttackTime = Time.time + 1f / attackRate;
                                }


                                //  AudioManager.Instance.PlaySFX(punchB);
                            }

                            else if (P == 2)
                            {
                                if (Time.time >= nextAttackTime)
                                {
                                    Longpunch();


                                    nextAttackTime = Time.time + 1f / attackRate;
                                }

                                // AudioManager.Instance.PlaySFX(flyKick);



                            }


                        }

                        else
                        {


                            panimator.Play("player_run");
                            //  AudioManager.Instance.PlaySFX(move);


                        }

                    }

                    else
                    {
                        if (Input.GetKey("f"))
                        {
                            if (Time.time >= nextAttackTime)
                            {
                                if (sower == false)
                                    AudioManager.Instance.PlaySFX(flyKick);

                                sower = true;
                                Flykickleft();
                                //  rb2D.velocity = new Vector2(30, rb2D.velocity.y);
                                nextAttackTime = Time.time + 1f / attackRate;
                            }

                        }
                    }

                }

                else
                {
                    {
                        
                        if (isGrounded)
                        {
                            panimator.Play("Playerdefend");
                            if(Enemyone.shootAngle >= -4 && Enemyone.shootAngle <= -2)
                            {
                               // rb2D.gravityScale = 4;
                                bc2D.isTrigger = false;
                            }
                            else
                            {
                               // rb2D.gravityScale = 0;
                                bc2D.isTrigger = true;
                            }
                            
                            // AudioManager.Instance.PlaySFX(move);
                        }
                        else
                        {
                           // rb2D.gravityScale = 4;
                            bc2D.isTrigger = false;
                        }


                    }
                }



            }

            else if (Input.GetKey("f"))
            {
                if (isGrounded)
                {

                    if (P == 0)
                    {
                        if (Time.time >= nextAttackTime)
                        {
                            Kick();
                            nextAttackTime = Time.time + 1f / attackRate;


                        }

                        // AudioManager.Instance.PlaySFX(punch);
                    }
                    else if (P == 1)
                    {

                        if (Time.time >= nextAttackTime)
                        {

                            Punch();

                            nextAttackTime = Time.time + 1f / attackRate;
                        }


                        //  AudioManager.Instance.PlaySFX(punchB);
                    }

                    else if (P == 2)
                    {
                        if (Time.time >= nextAttackTime)
                        {
                            Longpunch();


                            nextAttackTime = Time.time + 1f / attackRate;
                        }

                        // AudioManager.Instance.PlaySFX(flyKick);
                       


                    }


                }



            }

            else
            {
                if (isGrounded != Input.GetKey("down"))
                {
                    // animator.Play("Player Stand");
                    power = false;
                    sower = false;


                }
                if (power == false)
                    rb2D.velocity = new Vector2(0, rb2D.velocity.y);

            }
        }

        if (GameManager.gameState == GameManager.GameState.HOMEPAGE)
        {
            if (playHome)
            {
                AudioManager.Instance.PlayMusicWithCrossFade(intro, .5f);
                // Enemyone.eHealthbar.fillAmount = Enemyone.maxHealth;
                
            }

            playHome = false;
            playOngame = true;
            
        }
        if (GameManager.gameState == GameManager.GameState.PAUSE)
        {
            if (Input.GetKey("return"))
            {
                GameManager.gameState = GameManager.GameState.PLAY;
            }
        }


            if (Physics2D.Linecast(transform.position,groundCheckp.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
            else if( Physics2D.Linecast(transform.position, groundCheckpb.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
    void Punch()
    {
        // play attack animation
        rb2D.velocity = new Vector2(rb2D.velocity.x, 10);
        panimator.SetTrigger("Punch");
        AudioManager.Instance.PlaySFX(punch);
        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Cause Damage
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemyone>().TakeDamage(2);
            power = false;
        }
    }
    
    void Kick()
    {
        // play attack animation
        rb2D.velocity = new Vector2(rb2D.velocity.x +5, rb2D.velocity.y);
        panimator.SetTrigger("Kick");
        AudioManager.Instance.PlaySFX(punchB);
        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Cause Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemyone>().TakeDamage(2);
            power = false;
        }
        // Cause Damage
    }
  
    void Flykick()
    {
        // play attack animation
        
        panimator.SetTrigger("Flykick");
        rb2D.velocity = new Vector2(30, -2);
        power = true;
        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, enemyLayers);


        // Cause Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemyone>().TakeDamage(3);
        }
        // Cause Damage
    }

    void Flykickleft()
    {
        // play attack animation

        panimator.SetTrigger("Flykick");
        rb2D.velocity = new Vector2(-30, -2);
        power = true;
        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(kickPoint.position, kickRange, enemyLayers);


        // Cause Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemyone>().TakeDamage(3);
        }
        // Cause Damage
    }
    void Longpunch()
    {
        // play attack animation
        rb2D.velocity = new Vector2(rb2D.velocity.x+10, 10);
        panimator.SetTrigger("Longpunch");
        AudioManager.Instance.PlaySFX(flyKick);

        // detect enemies in range of attack
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);


        // Cause Damage
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemyone>().TakeDamage(2);
            power = false;
        }
        // Cause Damage
    }

    void SuperPower()
    {
        
        panimator.SetTrigger("Fire");
        AudioManager.Instance.PlaySFX(powerSfx);

        if (playerFlip.flipX == true)
        {
            powerPoint.position = new Vector3(powerPoint.position.x - 5, powerPoint.position.y, powerPoint.position.z);
            GameObject bullet = Instantiate(powerPrefab, powerPoint.position, powerPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-15f, 0);
            kk = true;
        }

        else if (playerFlip.flipX == false)
        {
            powerPoint.position = new Vector3(powerPoint.position.x, powerPoint.position.y, powerPoint.position.z);
            GameObject bullet = Instantiate(powerPrefab, powerPoint.position, powerPoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(15f, 0);
        }

        if (kk == true)
        {
            powerPoint.position = new Vector3(powerPoint.position.x + 5, powerPoint.position.y, powerPoint.position.z);
           kk = false;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        power = false;
    }
    public void PlayerReposition()
    {
        GetComponent<Transform>().localPosition = new Vector3(-6, -6, 0);
        rb2D.gravityScale = 4;
        bc2D.enabled = true;
        this.enabled = true;
        currentHealth = maxHealth;
        currentPower = maxPower;
        panimator.SetBool("pisDead", false);
        pHealthbar.fillAmount = 1f;
        firerate = 0;
        powerRefill = 0f;
        currentPower = maxPower;
        pPowerbar.fillAmount = currentPower / maxPower;
    }
    

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        pHealthbar.fillAmount = currentHealth / maxHealth;

        //Play hurt animation
        
        panimator.SetTrigger("Phurt");

        if (currentHealth <= 0)
        {
            Die();

        }
    }

    void Die()
    {
        GameManager.gameState = GameManager.GameState.DEATH;
        AudioManager.Instance.PlayMusicWithCrossFade(playerLoos, .5f);

        //Die animation
        panimator.SetBool("pisDead", true);


        // Disable the enemy 
        rb2D.gravityScale = 0;
        rb2D.velocity = new Vector2(0, 0);
        bc2D.enabled = false;
        this.enabled = false;
        

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null || kickPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireSphere(kickPoint.position, kickRange);



    }

}