using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

abstract public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    //private float ogSpeed = speed;
    public float attackRange = 2f;
    private PlayerController player;

    [SerializeField] float maxSanity;
    [SerializeField] float gasGain;
    [SerializeField] float insaneModeSanity = 0.1f;
    [SerializeField] SpriteRenderer normalSprite;
    [SerializeField] SpriteRenderer insaneModeSprite;
    float sanity;   

    private Animator animator;

    public bool attack;

    private float ogSpeed;
    

    //ATTACK 
    public float attackTime = 1f;
    public float attackSpeed = 1f;
    private Coroutine attackCoroutine;

    //ATTACK COOLDOWN
    private Coroutine attackCooldownCoroutine;
    public float attackCooldownTime = 1f;
    public float cooldownSpeed = 1f;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
        if (maxSanity == 0)
        {
            maxSanity = 1f;
        }
        sanity = maxSanity;

        ogSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        //wardens *** too fat
       //if 

        if (Vector2.Distance(transform.position, player.transform.position) < attackRange){
            //if (attackCoroutine == null)
            //{
            //GoToPlayer();
            if (attackCoroutine == null && attackCooldownCoroutine == null)
            {
                attackCoroutine = StartCoroutine(Attack(attackTime));
                //StopCoroutine(grappleCoroutine);
            }
            
            //}
        }//else{

            //if attackAnimation is not playing (start attack animation, that way this would not be retriggered)
            //attack = true;
            
            //grappleCoroutine = StartCoroutine(Grappling(collider));
        //}
        GoToPlayer();

    }

    void GoToPlayer(){
        float step = speed * Time.deltaTime * (sanity / maxSanity);

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        var moveDirection = (player.transform.position - transform.position).normalized;
        animator.SetFloat("X", moveDirection.x);
    }

    public void Flicker(bool flicker)
    {
        if (insaneModeSprite != null && normalSprite != null) 
        {
            //if (sanity < insaneModeSanity) 
            if (flicker) 
            {
                normalSprite.enabled = false;
                insaneModeSprite.enabled = true;
                if (GetComponent<Body>().enabled == true){ insaneModeSprite.GetComponent<SpriteRenderer>().color = Color.green;}
            } 
            else 
            {
                normalSprite.enabled = true;
                insaneModeSprite.enabled = false;
                if (GetComponent<Body>().enabled == true){ normalSprite.GetComponent<SpriteRenderer>().color = Color.green;}
            }
        }

    }

    public void LoseSanity(float sanity)
    {
        this.sanity -= sanity;
        if (this.sanity < 0)
        {
            this.sanity = 0;
        }
    }

    public virtual bool CheckMeleeable()
    {
        return true;
    }

    public virtual void Melee()
    {
        FindObjectOfType<Meter>().GainMeter(gasGain * (1 - sanity / maxSanity));
        if (GetComponent<Body>() != null)
        {
            GetComponent<Enemy>().enabled = false;
            GetComponent<Body>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        } else
        {/*warden dieee*/
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.otherCollider.GetComponent<PlayerController>() != null)
        if (collision.collider.GetComponent<PlayerController>() != null)
        {
            Collision();
        }

    }

    public virtual void Collision()
    {
        if (attack){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    /*
    public void Attack(float attackTime)
    {



        if (grappleCoroutine != null)
        {
            StopCoroutine(grappleCoroutine);
        }
        grappleCoroutine = StartCoroutine(Grappling(collider));
    }*/

    private IEnumerator Attack(float attackTime)
    {
        //set attack speed
        speed = attackSpeed;
        attack = true; //now u vulnerable

        yield return new WaitForSeconds(attackTime);

        //set back to normal
        attackCooldownCoroutine = StartCoroutine(AttackCooldown(attackCooldownTime));
        //speed = 4f;
        attackCoroutine = null;
        yield return null;

    }


    private IEnumerator AttackCooldown(float cooldownTime)
    {
        attack = false; //make cooldown tbh
        speed = cooldownSpeed;
        //attackCooldownCoroutine
        yield return new WaitForSeconds(attackCooldownTime);


        speed = ogSpeed; 
        attackCooldownCoroutine = null;
        yield return null;

    }
}

