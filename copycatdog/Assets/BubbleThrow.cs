using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleThrow : MonoBehaviour
{
    [SerializeField] private bool isThrowed = false;
    public int dir = 3;
    public float speed;
    private MapController map;

    private void Awake()
    {
        map = FindObjectOfType<MapController>();
    }

    private void Update()
    {
        if (isThrowed)
        {
            switch (dir)
            {
                case 0:
                    transform.Translate(new Vector2(0, 0.1f) * speed);
                    break;
                case 1:
                    transform.Translate(new Vector2(0, -0.1f) * speed);
                    break;
                case 2:
                    transform.Translate(new Vector2(-0.1f, 0) * speed);
                    break;
                case 3:
                    transform.Translate(new Vector2(0.1f, 0) * speed);
                    break;
                default:
                    Debug.Log("Throw Error");
                    break;
            }

            map.checkBounds(this.transform);
        }
    }

    public void Throw(int direction)
    {
        isThrowed = true;
        dir = direction;
    }
}
