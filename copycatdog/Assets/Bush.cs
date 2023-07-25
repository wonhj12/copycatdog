using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Sway");
            var colliders = collision.GetComponentsInChildren<SpriteRenderer>();
            foreach(var col in colliders)
            {
                col.enabled = false;
            }
        }
    }
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anim.SetTrigger("Sway");
            var colliders = collision.GetComponentsInChildren<SpriteRenderer>();
            foreach (var col in colliders)
            {
                col.enabled = true;
            }
        }
    }
}
