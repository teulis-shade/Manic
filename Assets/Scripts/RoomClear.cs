using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomClear : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy enemy = FindObjectOfType<Enemy>();
        //gotta check for loop all enemies and that none of them have main script
        if (spriteRenderer.enabled == false){
            Enemy[] enemyList = FindObjectsOfType<Enemy>();

            int dead_cnt = 0;
            foreach(Enemy enemy in enemyList)
            {
                //if enemy dead
                if (enemy.GetComponent<Enemy>().enabled == false){
                    dead_cnt += 1;
                }

                //objet.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alphaLevel);
            }

            if (dead_cnt == enemyList.Length){
                spriteRenderer.enabled = true;
            }
        }
        // for(var i = 0; i < firstList.Length; i++)
        // {
        //     if(firstList[i].gameObject.name == nameToLookFor)
        //     {
        //         finalList.Add(firstList[i]);
        //     }
        // }


        // if (enem == null && spriteRenderer.enabled == false){
        //     spriteRenderer.enabled = true;
        //     //SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
        // List<Object> finalList = new List<Object>();
        // string nameToLookFor = "name of game object";

        // for(var i = 0; i < firstList.Length; i++)
        // {
        //     if(firstList[i].gameObject.name == nameToLookFor)
        //     {
        //         finalList.Add(firstList[i]);
        //     }
        // }


        if (spriteRenderer.enabled && col.GetComponent<PlayerController>() != null){
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //spriteMove = -0.1f;
    }
}
