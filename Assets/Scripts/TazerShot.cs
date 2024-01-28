using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TazerShot : MonoBehaviour
{
    public float speed = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (gameObject.activeSelf)
        {
            //transform.position += transform.up * Time.deltaTime * speed;
            transform.position += transform.right * Time.deltaTime * speed;
            /*charge -= speed * Time.deltaTime * fallOff;
            if (charge <= 0)
            {
                charge = 0;
                gameObject.SetActive(false);
            }
            transform.localScale = new Vector3(charge, charge * 1.25f);*/
        }
    }
}
