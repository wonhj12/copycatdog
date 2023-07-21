using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleThrow : MonoBehaviour
{
    [SerializeField] private bool isThrowed = false;
    public int dir = 4;
    public float speed;
    public float positionRange;

    private bool isReturned = false;
    private bool isLandAvailable = false;

    private MapCheck map;
    private BoxCollider2D col;

    private void Awake()
    {
        map = GetComponent<MapCheck>();
        col = GetComponent<BoxCollider2D>();
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
                    Debug.Log("Throw Stopped");
                    break;
            }

            if (map.checkBounds(this.transform))
            {
                isReturned = true;
            }
        }

        if (isReturned)
        {
            Check();
            if (isLandAvailable)
            {
                ForceLand();
            }
        }
    }

    public void Throw(int direction)
    {
        isThrowed = true;
        dir = direction;
        this.gameObject.layer = 9;
    }

    private IEnumerator Land()
    {
        float randTime = Random.Range(0, 11);
        Debug.Log("·£´ý°ª" + randTime);
        randTime = randTime / 15;

        yield return new WaitForSeconds(randTime);

        isThrowed = false;
        dir = 4;
        this.transform.position = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
        this.gameObject.layer = 0;
    }

    private void ForceLand()
    {
        RaycastHit2D hit_p = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), Vector2.zero, 0.1f, LayerMask.GetMask("Player"));
        if(hit_p.transform != null)
        {
            Debug.Log("ÇÑ¹ÙÄû ´Ù µ¹¾ÒÀ½");
            isThrowed = false;
            dir = 4;
            this.transform.position = new Vector2(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y));
            this.gameObject.layer = 0;
            col.isTrigger = true;
        }
    }

    private void Check()
    {
        int x_intPos = (int)Mathf.Round(this.transform.position.x);
        int y_intPos = (int)Mathf.Round(this.transform.position.y);

        Vector2 direction = new Vector2(0, 0);
        switch (dir)
        {
            case 0:
                direction = Vector2.up;
                break;
            case 1:
                direction = Vector2.down;
                break;
            case 2:
                direction = Vector2.left;
                break;
            case 3:
                direction = Vector2.right;
                break;
        }
        Debug.DrawRay(transform.position, new Vector3(direction.x, direction.y, 0), new Color(0, 1, 0));
        RaycastHit2D hit_1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), direction, 0.1f, LayerMask.GetMask("Wall"));

        if(hit_1.transform == null)
        {
            if (dir < 2)
            {
                if (y_intPos - positionRange < this.transform.position.y && this.transform.position.y < y_intPos + positionRange)
                {
                    Debug.Log(this.transform.position.y + "Can Land on " + y_intPos);
                    isLandAvailable = true;
                    StartCoroutine(Land());
                }
            }
            else if (dir < 4)
            {
                if (x_intPos - positionRange < this.transform.position.x && this.transform.position.x < x_intPos + positionRange)
                {
                    Debug.Log(this.transform.position.x + "Can Land on " + x_intPos);
                    isLandAvailable = true;
                    StartCoroutine(Land());
                }
            }
        }

    }
}
