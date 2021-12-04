using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // i know this is slow but the player isnt tagged so :shrug:
        if (collision.GetComponent<MovementScript>() != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

}
