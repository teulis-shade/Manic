using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCharger : MonoBehaviour
{

    public void Charging(float currentCharge)
    {
        gameObject.SetActive(true);
        transform.localScale = new Vector3(currentCharge, currentCharge * .75f);
        transform.localPosition = new Vector3(0f, .5f + currentCharge / 3f, 0.1f);
    }

    public void StopCharging()
    {
        transform.localScale = new Vector3(0, 0);
        gameObject.SetActive(false);
    }
}
