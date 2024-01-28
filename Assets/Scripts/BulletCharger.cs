using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCharger : MonoBehaviour
{
    public void Start(){
        gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }

    public void Charging(float currentCharge)
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(currentCharge, currentCharge * .75f);
        //transform.localPosition = new Vector3(0f, .5f + currentCharge / 3f, 0.1f);
        transform.localPosition = new Vector3(-.25f - currentCharge / 3f, 0f, 0.1f);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        
    }

    public void StopCharging()
    {
        transform.localScale = new Vector3(0, 0);
        gameObject.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
}
