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
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy == null && spriteRenderer.enabled == false){
            spriteRenderer.enabled = true;
            //SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (spriteRenderer.enabled && col.GetComponent<PlayerController>() != null){
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
        //Debug.Log(col.gameObject.name + " : " + gameObject.name + " : " + Time.time);
        //spriteMove = -0.1f;
    }
}
