using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //�ð� ���� ������
    [Header("�ð� ���� ������")]
    public float explodeTime;

    //����, ���� ���� ������
    [Header("����, ���� ���� ������")]
    public int Length;

    //�ʿ��� ������Ʈ
    [Header("�ʿ��� ������Ʈ")]
    public GameObject Water;
    public Character player;
    [SerializeField] private BoxCollider2D box_Col;
    [SerializeField] private GameObject circle_Col;

    [Header("���� ����")]
    public bool isExploded = false;
    public bool isPlayerIn = true;

    private void Start()
    {
        box_Col = GetComponent<BoxCollider2D>();

        box_Col.enabled = false;
        circle_Col.SetActive(true);

        StartCoroutine(WaterSplash());
        Destroy(this.gameObject, explodeTime);
    }

    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            box_Col.isTrigger = false;
            isPlayerIn = false;
        }
    }
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            isPlayerIn = true;
            player = collision.GetComponent<Character>();       //����ִ� �÷��̾��� ĳ���� ������Ʈ ��������
        }
    }


    public IEnumerator WaterSplash()
    {
        yield return new WaitForSeconds(explodeTime - 0.2f);

        isExploded = true;

        if (isPlayerIn)
        {
            player.Damage();
        }


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

    public IEnumerator WaterSplash_Immediate()
    {

        isExploded = true;

        if (isPlayerIn)
        {
            player.Damage();
        }


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

        Destroy(this.gameObject);

        yield return null;
    }
}
