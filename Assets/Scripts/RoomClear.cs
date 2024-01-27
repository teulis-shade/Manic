using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomClear : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Enemy enemy = FindObjectOfType<Enemy>();
        if (enemy == null){
            SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
