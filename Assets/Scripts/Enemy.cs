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

    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
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
            // print("attack");
        }
        
    }

    void GoToPlayer(){
        float step = speed * Time.deltaTime * (sanity / maxSanity);

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
        var moveDirection = (player.transform.position - transform.position).normalized;
        animator.SetFloat("X", moveDirection.x);
        animator.SetFloat("Y", moveDirection.y);
    }

    public void LoseSanity(float sanity)
    {
        this.sanity -= sanity;
        if (this.sanity < 0)
        {
            this.sanity = 0;
        }
    }
}
