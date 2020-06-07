using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemyone : MonoBehaviour
{
    [SerializeField] private AudioClip dialougeA;
    [SerializeField] private AudioClip dialougeB;
    [SerializeField] private AudioClip dialougeC;
    [SerializeField] private AudioClip dialougeD;
    [SerializeField] private AudioClip dialougeE;
    [SerializeField] private AudioClip dialougeF;
    [SerializeField] private AudioClip dialougeG;
    [SerializeField] private AudioClip dialougeH;
    [SerializeField] private AudioClip dialougeI;
    [SerializeField] private AudioClip dialougeJ;
    [SerializeField] private AudioClip dialougeK;
    [SerializeField] private AudioClip dialougeL;
    [SerializeField] private AudioClip dialougeM;
    [SerializeField] private AudioClip dialougeN;
    [SerializeField] private AudioClip dialougeO;
    [SerializeField] private AudioClip enemyLoos;
    [SerializeField] private AudioClip bullet;
    [SerializeField] private AudioClip kamoR;
    [SerializeField] private AudioClip kicK;

    AudioManager eamanager;
    public  Animator animator;
    public static float maxHealth = 100;
    public static float currentHealth;
    public static Collider2D enemyCollider2d;
    public static Rigidbody2D enemyRigidbody2d;
    public static Transform enemy;
    public static bool flip;
    public SpriteRenderer enemyFlip;
    public static float distancePlayers;
    public static Vector3 midPosition;

    // enemy ai

    public Transform kattackPoint;
    public LayerMask playerLayers;
    public float kattackRange;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    float aiAttackrate = 0f;
    float aiHitrate = 0f;
    float hurtRate = 0f;
    float kamorRate = 0f;
    float finalKamor = 0f;
    float dialougeTime = 0f;
    float currentPowere;
    float maxPowere =100f;
    float powerreFill;
    float superFire;

    public Transform player;
    public Transform left;
    public Transform right;

    public Transform firePoint;
    public GameObject bulletPrefab;


    public float bulletForce = 20f;
    public static float shootAngle;

    int t;
    int h;
    int D;
    int P;
    int fT;
    bool kk;
    bool fightDelaynear;


    [SerializeField]
    Transform groundCheck;

    [SerializeField]
    Transform groundCheckb;

    bool isGrounded;


    [Header("Unity Staff")]
    public Image eHealthbar;
    public Image ePowerbar;




    // Start is called before the first frame update
    void Start()
    {
        enemyRigidbody2d = GetComponent<Rigidbody2D>();
        enemyCollider2d = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        enemy = GetComponent<Transform>();
        enemyFlip = GetComponent<SpriteRenderer>();
        enemyRigidbody2d.gravityScale = 0;
        enemyCollider2d.enabled = false;
        this.enabled = true;
        currentHealth = maxHealth;
        currentPowere = maxPowere;
        powerreFill = 0;
        flip = false;
        enemyFlip.flipX = true;
        fT = 0;
        superFire = 0f;
        

        // enemy ai

        // player = GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        left = GameObject.FindGameObjectWithTag("Left").transform;
        right = GameObject.FindGameObjectWithTag("Right").transform;

    }

    // Update is called once per frame
    void Update()
    {

        distancePlayers = Vector2.Distance(transform.position, player.position);
        midPosition = new Vector3 ((transform.position.x+player.position.x) /2, -2.6f ,0);


        if (Physics2D.Linecast(transform.position, groundCheckb.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else if( Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

        if (Time.time >= aiAttackrate)
        {
            t = Random.Range(1, 9);
            
            aiAttackrate = Time.time + 3f;
        }

        if(Time.time>= aiHitrate)
        {
            h = Random.Range(1, 5);
            aiHitrate = Time.time + 1f;
        }

        if (Vector2.Distance(transform.position, right.position) + Vector2.Distance(left.position, player.position) > Vector2.Distance(left.position, right.position))
        {
            flip = true;
            enemyFlip.flipX = false;
        }
        else
        {
            flip = false;
            enemyFlip.flipX = true;
        }


        if (GameManager.gameState == GameManager.GameState.PLAY)
        {
           this.enabled = true;
            enemyRigidbody2d.gravityScale = 4;
            enemyCollider2d.enabled = true;

            D = Random.Range(0, 18);

            if(currentPowere < 99 && Time.time>= powerreFill)
            {
                if (currentPowere < 0)
                    currentPowere = 0;

                currentPowere = currentPowere + .5f;
                powerreFill = Time.time + 1f;
                ePowerbar.fillAmount = currentPowere / maxPowere; 

            }

            // enemy ai
            if (currentHealth < 20 && Time.time > finalKamor)
            {
                Kamor();
                finalKamor = Time.time + 15f;
            }

           

            else if (t == 1 || t == 2 || t == 3)
            {

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                    animator.SetTrigger("Walk");
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance)
                {
                    transform.position = this.transform.position;

                    if(fightDelaynear == true)
                    {
                        hurtRate = Time.time + 1f;
                        fightDelaynear = false;
                    }

                    if (Time.time >= hurtRate && isGrounded)
                    {

                        enemyRigidbody2d.velocity = new Vector2(enemyRigidbody2d.velocity.x, 15);
                        Kick();


                        hurtRate = Time.time + .5f;
                    }
                }
               

            }

            else if (t == 4 || t == 5|| t==6)
            {
           
             if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
                    

                    if (Input.GetKey("right") && Vector2.Distance(transform.position, player.position) < retreatDistance/3)
                    {
                        if(Time.time >= kamorRate)
                        {
                            
                            Kamor();
                            

                            kamorRate = Time.time + 10f;
                        }
                    }

                    else
                        animator.SetTrigger("Walk");

                }
                else if (Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;

                    if (P != 2 && Time.time >= hurtRate)
                    {
                      
                            Fire();
                     
                        hurtRate = Time.time + 3f;

                    }

                    if (P==2 && currentPowere > 30)
                    {
                        if (Time.time >= superFire && fT < 6)
                        {
                        Fire();
                            superFire = Time.time + .5f;
                            fT += 1;
                            P = 2;
                            currentPowere -= 5f;
                            ePowerbar.fillAmount = currentPowere / maxPowere;

                        }
                        else if (fT >= 6)
                        {
                            fT = 0;
                            P = 1;
                            
                        }
                    }

                }
               


            }
            
                else if (Vector2.Distance(transform.position, player.position) > 10)
                {
                    if (Time.time > dialougeTime)
                    {

                        if (D == 0)
                            AudioManager.Instance.PlaySFX(dialougeA);
                        if (D == 1)
                            AudioManager.Instance.PlaySFX(dialougeB);
                        if (D == 2)
                            AudioManager.Instance.PlaySFX(dialougeC);
                    if (D == 3)
                        AudioManager.Instance.PlaySFX(dialougeA);
                    if (D == 4)
                        AudioManager.Instance.PlaySFX(dialougeB);
                    if (D == 5)
                        AudioManager.Instance.PlaySFX(dialougeC);
                    if (D == 6)
                        AudioManager.Instance.PlaySFX(dialougeD);
                    if (D == 7)
                        AudioManager.Instance.PlaySFX(dialougeE);
                    if (D == 8)
                        AudioManager.Instance.PlaySFX(dialougeF);
                    if (D == 9)
                        AudioManager.Instance.PlaySFX(dialougeG);
                    if (D == 10)
                        AudioManager.Instance.PlaySFX(dialougeH);
                    if (D == 11)
                        AudioManager.Instance.PlaySFX(dialougeI);
                    if (D == 12)
                        AudioManager.Instance.PlaySFX(dialougeJ);
                    if (D == 13)
                        AudioManager.Instance.PlaySFX(dialougeK);
                    if (D == 14)
                        AudioManager.Instance.PlaySFX(dialougeL);
                    if (D == 15)
                        AudioManager.Instance.PlaySFX(dialougeM);
                    if (D == 16)
                        AudioManager.Instance.PlaySFX(dialougeN);
                    if (D == 17)
                        AudioManager.Instance.PlaySFX(dialougeO);


                    dialougeTime = Time.time + 6f;
                    }
                fightDelaynear = true;
            }
            


           


            else if (h == 1 || h == 2 || h==4)
            {
                if(Time.time >= hurtRate && isGrounded)
                {
                    if (Vector2.Distance(transform.position, player.position) < stoppingDistance*2)
                    {
                        

                        enemyRigidbody2d.velocity = new Vector2(enemyRigidbody2d.velocity.x, 15);
                        Kick();
                    }

                    hurtRate = Time.time + .3f;
                }

                

            }


            else if (h == 3 || h==4)
            {

                if (Time.time >= hurtRate)
                {
                    if (Vector2.Distance(transform.position, player.position) > retreatDistance / 2 + 3)
                    {

                       
                        Fire();
                    }

                    

                    hurtRate = Time.time + 1f;

                }
                   

            }
          

            



        }

        

    }
    void Kick()
    {
        // play attack animation
        animator.SetTrigger("Kick");
        AudioManager.Instance.PlaySFX(kicK);
        // detect enemies in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(kattackPoint.position, kattackRange, playerLayers);


        // Cause Damage
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController2D>().TakeDamage(2);
        }
        // Cause Damage
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        eHealthbar.fillAmount = currentHealth / maxHealth;
        
        //Play hurt animation
        animator.SetTrigger("Hurt");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        GameManager.gameState = GameManager.GameState.DEATH;
        AudioManager.Instance.PlayMusicWithCrossFade(enemyLoos, .5f);

        //Die animation
        animator.SetBool("IsDead",true);


        // Disable the enemy 
        enemyRigidbody2d.gravityScale = 0;
        enemyRigidbody2d.velocity = new Vector2(0,0);
        enemyCollider2d.enabled = false;
        this.enabled = false;
        

    }

    public void EhealthReset()
    {
        animator.SetBool("IsDead", false);
         aiAttackrate = 0f;
         aiHitrate = 0f;
         hurtRate = 0f;
         kamorRate = 0f;
        finalKamor = 0f;
        dialougeTime = 0f;
        this.enabled = true;
        Debug.Log("baaal");
        fT = 0;
        superFire = 0f;
        eHealthbar.fillAmount = maxHealth;
        currentPowere = maxPowere;
        ePowerbar.fillAmount = currentPowere / maxPowere;
    }



    void Kamor()
    {
        // play attack animation
        animator.SetTrigger("Kamor");
        AudioManager.Instance.PlaySFX(kamoR);
        // detect enemies in range of attack
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(kattackPoint.position, kattackRange, playerLayers);


        // Cause Damage
        foreach (Collider2D player in hitPlayer)
        {
            player.GetComponent<PlayerController2D>().TakeDamage(15);
            if(currentHealth < 85)
            {
                currentHealth = currentHealth + 15f;
                eHealthbar.fillAmount = currentHealth / maxHealth;
            }
            
        }
        // Cause Damage
    }

    void Fire()
    {
        P = Random.Range(1, 6);
        shootAngle = Random.Range(-4, 5);
        // play attack animation
        animator.SetTrigger("Fire");
        AudioManager.Instance.PlaySFX(bullet);
        // detect enemies in range of attack
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(fattackPoint.position, fattackRange, playerLayers);


        //   rb.AddForce(firePoint.up*bulletForce, ForceMode2D.Impulse);
        if (enemyFlip.flipX == true)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-25f, shootAngle);
        }

        else if (enemyFlip.flipX == false)
        {
            firePoint.position = new Vector3(firePoint.position.x + 5, firePoint.position.y, firePoint.position.z);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(25f, shootAngle);
             kk = true;
        }

        if (kk == true) 
        
        {
            firePoint.position = new Vector3(firePoint.position.x - 5, firePoint.position.y, firePoint.position.z);
            kk = false;
        }


    }
     
    private void OnDrawGizmosSelected()
    {
        if (kattackPoint == null)
            return;

        Gizmos.DrawWireSphere(kattackPoint.position, kattackRange);



    }



}
