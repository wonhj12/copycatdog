using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderRenderer : MonoBehaviour
{
    private SpriteRenderer sprite;
    public bool isPlayer = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();

        Order();
    }

    private void Update()
    {
        if (isPlayer)
        {
            Order();
        }
    }

    private void Order()
    {
        sprite.sortingOrder = (int)Mathf.Round(this.transform.position.y * (-1));
    }
}
