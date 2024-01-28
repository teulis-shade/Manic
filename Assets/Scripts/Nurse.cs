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
        
        if (attack){
            weapon.GetComponent<SpriteRenderer>().enabled = true;

        }else{
            weapon.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    
}
