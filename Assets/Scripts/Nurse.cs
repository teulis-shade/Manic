using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nurse : Enemy
{


    /*
    void Start()
    {
        
    }*/

    // Update is called once per frame
    
    public override void Update()
    {
        base.Update();

        /*
        if (attackCoroutine != null){
            StopCoroutine(attackCoroutine);
        }*/
        //StopCoroutine(attackCoroutine);
        
        if (attack){
            weapon.GetComponent<SpriteRenderer>().enabled = true;

        }else{
            weapon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    /*
    //IF WANT TO DISABLE HER
    public override void Collision()
    {
        if (attack){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }*/

    
}
