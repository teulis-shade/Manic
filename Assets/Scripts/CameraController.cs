using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    public bool leftLocked;
    public bool rightLocked;
    public bool topLocked;
    public bool botLocked;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    void Update()
    {
        Vector2 movement = new Vector2(player.transform.position.x, player.transform.position.y);
        if (leftLocked)
        {
            if (player.transform.position.x < transform.position.x)
            {
                movement.x = transform.position.x;
            }
        }
        if (rightLocked)
        {
            if (player.transform.position.x > transform.position.x)
            {
                movement.x = transform.position.x;
            }
        }
        if (topLocked)
        {
            if (player.transform.position.y > transform.position.y)
            {
                movement.y = transform.position.y;
            }
        }
        if (botLocked)
        {
            if (player.transform.position.y < transform.position.y)
            {
                movement.y = transform.position.y;
            }
        }
        transform.position = new Vector3(movement.x, movement.y, -10f);
    }
}
