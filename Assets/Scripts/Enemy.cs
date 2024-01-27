using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private PlayerController player;

    [SerializeField] float maxSanity;
    float sanity;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        if (maxSanity == 0)
        {
            maxSanity = 1f;
        }
        sanity = maxSanity;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.transform.position) > 2f){
            GoToPlayer();
        }else{
            print("attack");
        }
        
    }

    void GoToPlayer(){
        float step = speed * Time.deltaTime * (sanity / maxSanity);

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);

    }

    public void LoseSanity(float sanity)
    {
        this.sanity -= sanity;
    }
}
