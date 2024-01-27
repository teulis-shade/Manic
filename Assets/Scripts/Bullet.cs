using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    [SerializeField] float speed;
    public void StartFiring(float charge, Vector2 movement, Vector3 start)
    {
        gameObject.SetActive(true);
        transform.position = start;
        transform.localScale = new Vector3(charge, charge * 1.25f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        direction = movement;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }
}
