using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    private PlayerController player;


    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        GoToPlayer();
    }

    void GoToPlayer(){
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position , step);

    }
}
