using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class Warden : Enemy
{
    
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

        if (sanity < 0.5){
            //SHOW CAN BE MELEED
        }else{
            //SHOW CANNOT BE MELEED
        }
    }

    public override void Melee()
    {
        print(sanity);
        //SHOW THIS SOMEHWERE
        if (sanity < 0.5){
            base.Melee();
            // FindObjectOfType<Meter>().GainMeter(gasGain * (1 - sanity / maxSanity));
            // if (GetComponent<Body>() != null)
            // {
            //     GetComponent<Enemy>().enabled = false;
            //     GetComponent<Body>().enabled = true;
            //     GetComponent<BoxCollider2D>().isTrigger = true;
            // } else
            // {/*warden dieee*/
            //     Destroy(this.gameObject);
            // }
        }
    }
}
