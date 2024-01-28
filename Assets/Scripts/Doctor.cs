using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doctor : Enemy
{
    [SerializeField] private bool maskOn;
    /*
    void Start()
    {
        
    }*/

    // Update is called once per frame
    /*
    void Update()
    {
      
    }*/
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
