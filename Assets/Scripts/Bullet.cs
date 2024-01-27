using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector2 direction = Vector2.zero;
    private float charge;
    [SerializeField] float speed;
    [SerializeField] GameObject explosionPrefab;
    public void StartFiring(float charge, Vector2 movement, Vector3 start)
    {
        gameObject.SetActive(true);
        transform.position = start;
        transform.localScale = new Vector3(charge, charge * 1.25f);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, movement);
        this.charge = charge;
        direction = movement;
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            transform.position += transform.up * Time.deltaTime * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null && other.GetComponent<CameraBound>() == null)
        {
            Debug.Log(other.gameObject.name);
            Explode();
        }
    }
    
    private void Explode()
    {
        GameObject explosion = Instantiate(explosionPrefab);
        explosion.transform.position = this.transform.position;
        explosion.transform.localScale = new Vector3(charge * 2f, charge * 2f);
        explosion.GetComponent<Explosion>().StartFade(5f);
        gameObject.SetActive(false);
    }
}
