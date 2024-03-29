using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Vector3 direction = Vector2.zero;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float falloff;
    [SerializeField] private float curSpeed;
    private AIPath aiPath;

    public void Start(){
        aiPath = GetComponent<AIPath>();
        aiPath.enabled = false;
    }

    public void Thrown(Vector2 movement)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        direction = movement;
        curSpeed = maxSpeed;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += direction * Time.deltaTime * curSpeed;
            if (curSpeed > 0)
            {
                curSpeed -= Time.deltaTime * Mathf.Pow(falloff, 2);
            } else if (curSpeed < 0)
            {
                curSpeed = 0;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null && other.GetComponent<BoxCollider2D>() != null && !other.GetComponent<BoxCollider2D>().isTrigger)
        {
            curSpeed = 0;
            if (other.GetComponent<Enemy>() != null)
            {
                Debug.Log("Hit");
                other.GetComponent<Enemy>().BodyHit();
            }
        }
    }
}
