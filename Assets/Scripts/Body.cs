using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    private Vector3 direction = Vector2.zero;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float falloff;
    private float curSpeed;
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
        if (other.GetComponent<PlayerController>() == null && other.GetComponent<CameraBound>() == null)
        {
            curSpeed = 0;
        }
    }
}
