using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

abstract public class Enemy : MonoBehaviour
{
    public int hp;
    [SerializeField] PlayerController player;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoToPlayer();
    }

    void GoToPlayer(){
        float step = 10f * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position , step);

    }
}
