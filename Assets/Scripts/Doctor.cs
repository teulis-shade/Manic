using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Doctor : Enemy
{
    [SerializeField] private bool maskOn;

    //ATTACK COOLDOWN
    private Coroutine shootCoroutine;
    public float shootCooldown = 0.1f;
    //public float cooldownSpeed = 1f;


    [SerializeField] GameObject TazerShot;

    /*
    void Start()
    {
        
    }*/

    // Update is called once per frame
    /*
    void Update()
    {
      
    }*/

    public override void LoseSanity(float sanity)
    {
        //only lose sanity with mask
        if (!maskOn){
            base.LoseSanity(sanity);
            // this.sanity -= sanity;
            // if (this.sanity < 0)
            // {
            //     this.sanity = 0;
            // }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Enemy>() != null && col.GetComponent<Enemy>().enabled == false){
            maskOn = false;
        }

        // List<Object> finalList = new List<Object>();
        // string nameToLookFor = "name of game object";

        // for(var i = 0; i < firstList.Length; i++)
        // {
        //     if(firstList[i].gameObject.name == nameToLookFor)
        //     {
        //         finalList.Add(firstList[i]);
        //     }
        // }


        // if (spriteRenderer.enabled && col.GetComponent<PlayerController>() != null){
        //     SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }
    

    /*
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        print("asdf");
        print(collision.collider.GetComponent<Enemy>());
        print(collision.collider.GetComponent<Enemy>().enabled);

        //if collide with body and have no mask lose mask, can take damage
        if (collision.collider.GetComponent<Enemy>() != null && collision.collider.GetComponent<Enemy>().enabled == false){
            maskOn = false;
        }
            // Enemy[] enemyList = FindObjectsOfType<Enemy>();

            // int dead_cnt = 0;
            // foreach(Enemy enemy in enemyList)
            // {
            //     //if enemy dead
            //     if (enemy.GetComponent<Enemy>().enabled == false){

        //if (collision.otherCollider.GetComponent<PlayerController>() != null)



    }*/


    public override void Collision()
    {
        //do nothing, we shooting babyyy
        /*
        if (attack){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }


    public override void Melee()
    {
        if (!maskOn){
            base.Melee();
        }
        /*
        FindObjectOfType<Meter>().GainMeter(gasGain * (1 - sanity / maxSanity));
        if (GetComponent<Body>() != null)
        {
            GetComponent<Enemy>().enabled = false;
            GetComponent<Body>().enabled = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        } else
        {
            Destroy(this.gameObject);
        }*/
    }

    public override void Update()
    {
        base.Update();
        
        if (attack){
            //shoot
            weapon.GetComponent<SpriteRenderer>().enabled = true;
            if (shootCoroutine == null){
                //REMOVE TO MAKE SANE
                shootCoroutine = StartCoroutine(Shoot(shootCooldown));
            }
            

        }else{
            weapon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }



    private IEnumerator Shoot(float shootTime)
    {
        //CREATE A THINGY AND SHOOTIT
        GameObject tazershot = Instantiate(TazerShot);
        tazershot.transform.position = this.transform.position;
        
        Vector3 playerDirection = player.transform.position - transform.position;
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        tazershot.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //tazershot.transform.rotation = Quaternion.LookRotation(Vector3.forward, new Vector3(Vector2.Distance(transform.position, player.transform.position), 0));


        //tazershot.transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        //tazershot.transform.forward = new Vector3(Vector2.Distance(transform.position, player.transform.position), 0);
        // explosion.transform.localScale = new Vector3(charge * 2f, charge * 2f);
        // explosion.GetComponent<Explosion>().SetCharge(charge);
        // explosion.GetComponent<Explosion>().StartFade(5f);
        // gameObject.SetActive(false);
        // tazershot.gameObject.transform


        //tazershot.transform = gameObject.transform;
        /*
        gameObject.SetActive(true);
        transform.position = start;
        transform.localScale = new Vector3(charge, charge * 1.25f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        this.charge = charge;
        direction = movement;
        */


        yield return new WaitForSeconds(shootTime);
        shootCoroutine = null;
        yield return null;
    }
}
