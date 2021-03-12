using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Detection and Config")]
    public GameObject playerDetection;
    public float moveDistance = 8f;
    public float attackDistance = 2f;
    public float attackRate = 2;
    public int damage = 10;
    public bool showGizmo = false;

    [Header("Life")]
    public float healt = 100f;
    public bool isAlive = true;

    [Header("Move")]
    public float speedMove = 5;

    private Animator anim;
    //private bool grounded = true;
    private SpriteRenderer enemyRenderer;
    private CapsuleCollider2D colliderEnemySimple;
    private Enemy enemyScript;
    private float timeLastAttack = 0;


    // Start is called before the first frame update
    void Start()
    {
        
        anim = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        colliderEnemySimple = GetComponent<CapsuleCollider2D>();
        enemyScript = GetComponent<Enemy>();


        if (PlayerPrefs.HasKey(name))
            Dead();
    }

    // Update is called once per frame
    void Update()
    {
        //Vida + Muerte
        HealtEnemy();

        if (playerDetection.GetComponent<LifePlayer>().isAlive == true)
        {
            WalkAttack();
        }
       // else
           // anim.SetBool("Walk", false);
    }

    public void HealtEnemy()
    {
        if (healt <= 0)
        {
            Dead();
        }
    }
    public void WalkAttack()
    {
        //WALK + Attack
        float distance = Vector2.Distance(playerDetection.transform.position, transform.position);
        if (distance <= moveDistance)
        {
            if (distance >= attackDistance)
                EnemyMove();
            else
                Attack();
        }
        //else
            //anim.SetBool("Walk", false);
        
    }

    //Flip + MOVE
    public void EnemyMove()
    {
        //anim.SetBool("Walk", true);

        float moveX;
        moveX = speedMove * Time.deltaTime;

        //Si el enemigo está a la izda del player
        if (transform.position.x < playerDetection.transform.position.x)
        {
            enemyRenderer.flipX = false;
            transform.Translate(Vector2.right * moveX);
        }

        //Si el enemigo está a la derecha del player
        if (transform.position.x > playerDetection.transform.position.x)
        {
            enemyRenderer.flipX = true;
            transform.Translate(Vector2.left * moveX);
        }

    }

    //Daño
    public void Attack()
    {
        float elapsed = (Time.time - timeLastAttack);

        if(elapsed >= attackRate)
        {
         anim.SetTrigger("Attack Enemy");
         timeLastAttack = Time.time;
        }
    }
    // El sonido se ejecuta desde la animacion con un evento FPS:23
    public void SoundEnemy()
    {
       GameManager.instance.audioControl.attackEnemy1.Play();
    }
    public void Dead()
    {
        // anim.SetTrigger("Dead");
        colliderEnemySimple.enabled = false;
        enemyScript.enabled = false;
        isAlive = false;
        Destroy(gameObject);
        PlayerPrefs.SetInt(gameObject.name, 0);
    }

    //Vida + animacion
    private void OnDrawGizmos()
    {
        if (showGizmo == true)
        {
           Gizmos.color = Color.green;
           Gizmos.DrawWireSphere(transform.position, moveDistance);
           Gizmos.color = Color.red;
           Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<LifePlayer>().DamagePlayer(damage);
        }
    }

}
