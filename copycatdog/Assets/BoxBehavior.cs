using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBehavior : MonoBehaviour
{
    public float errorDistance;
    public bool isPushed = false;
    private int dir;
    private Vector2 dirVec = Vector2.zero;
    private Vector2 targetVec = Vector2.zero;
    [SerializeField] private float speed;
    [SerializeField] private float pushDelay;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isPushed)
        {

            int intPosX = (int)targetVec.x;
            int intPosY = (int)targetVec.y;

            float distanceX = (intPosX - this.transform.position.x);
            float distanceY = (intPosY - this.transform.position.y);
            if (distanceX < 0)
                distanceX *= -1;
            if (distanceY < 0)
                distanceY *= -1;


            if (dir < 2)
            {
                if (this.transform.position.y != intPosY)
                {
                    if (distanceY < errorDistance || distanceY > 1.3f)
                    {
                        StartCoroutine(PushFinish());
                        this.transform.position = new Vector2(intPosX, intPosY);
                    }
                    else
                    {
                        this.transform.Translate(dirVec * speed * 0.03f);
                    }
                }
            }
            else
            {
                if (this.transform.position.x != intPosX)
                {
                    if (distanceX < errorDistance || distanceX > 1.3f)
                    {
                        StartCoroutine(PushFinish());
                        this.transform.position = new Vector2(intPosX, intPosY);
                    }
                    else
                    {
                        this.transform.Translate(dirVec * speed * 0.03f);
                    }
                }
            }
        }
    }

    private IEnumerator PushFinish()
    {
        yield return new WaitForSeconds(pushDelay);
        isPushed = false;
    }

    public void Push(int direction)
    {
        if (isPushed == false)
        {
            dir = direction;

            switch (dir)
            {
                case 0:
                    dirVec = Vector2.up;
                    break;
                case 1:
                    dirVec = Vector2.down;
                    break;
                case 2:
                    dirVec = Vector2.left;
                    break;
                case 3:
                    dirVec = Vector2.right;
                    break;
            }
            targetVec = new Vector2(this.transform.position.x, this.transform.position.y) + dirVec;

            Debug.DrawRay(this.transform.position, dirVec, new Color(0, 0, 1));
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(this.transform.position.x, this.transform.position.y) + dirVec, Vector2.zero, 0.1f, LayerMask.GetMask("Wall", "WorldLimit", "Bush"));
            if (hit.transform == null)
            {
                isPushed = true;
            }
            
        }
    }
}
