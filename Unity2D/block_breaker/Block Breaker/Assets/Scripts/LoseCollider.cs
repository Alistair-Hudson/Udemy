using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Ball>())
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (collision.gameObject.GetComponent<Block>())
        {
            Destroy(collision.gameObject);
        }
    }
}
