using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnTriggerEnter2D(Collider2D col)
    {
        //not working aaaaaaaaaaaa


        //Destroy(col);
        if (col.gameObject.GetComponent<PlayerController>() != null){
            //kill player
            //Destroy(col.gameObject);
            col.gameObject.GetComponent<PlayerController>().ReduceSanity(.5f);
        }else if (col.gameObject.GetComponent<Enemy>() != null && col.gameObject.GetComponent<Enemy>().enabled == false){ //kill self with dead body
            //kill self
            Destroy(gameObject);

        }

        /*
        if (other.GetComponent<PlayerController>() == null && other.GetComponent<CameraBound>() == null)
        {
            curSpeed = 0;
        }

        Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        spriteMove = -0.1f;*/
    }
}
