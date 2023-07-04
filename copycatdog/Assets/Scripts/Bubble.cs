using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //시간 관련 변수들
    [Header("시간 관련 변수들")]
    public float explodeTime;

    //길이, 방향 관련 변수들
    [Header("길이, 방향 관련 변수들")]
    public int Length;

    //필요한 컴포넌트
    [Header("필요한 컴포넌트")]
    public GameObject Water;
    [SerializeField] private Collider2D col;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        col.isTrigger = true;
        StartCoroutine(WaterSplash());
        Destroy(this.gameObject, explodeTime);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            col.isTrigger = false;
        }
    }

    private IEnumerator WaterSplash()
    {
        yield return new WaitForSeconds(explodeTime - 0.2f);

        GameObject Bubble_Up = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
        Bubble_Up.GetComponent<Water>().remain_Length = Length - 1;
        Bubble_Up.GetComponent<Water>().Direction = (int)Dir.up;

        GameObject Bubble_Down = Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
        Bubble_Down.GetComponent<Water>().remain_Length = Length - 1;
        Bubble_Down.GetComponent<Water>().Direction = (int)Dir.down;

        GameObject Bubble_Left = Instantiate(Water, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
        Bubble_Left.GetComponent<Water>().remain_Length = Length - 1;
        Bubble_Left.GetComponent<Water>().Direction = (int)Dir.left;

        GameObject Bubble_Right = Instantiate(Water, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
        Bubble_Right.GetComponent<Water>().remain_Length = Length - 1;
        Bubble_Right.GetComponent<Water>().Direction = (int)Dir.right;
    }
}
