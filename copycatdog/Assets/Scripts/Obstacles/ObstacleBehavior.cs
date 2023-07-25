using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    //장애물 위치정보
    private Vector2 obstaclePos;
    private ItemManager itemManage;
    private GameObject item;

    void Start()
    {
        obstaclePos = this.transform.position;
        itemManage = FindObjectOfType<ItemManager>();
        item = itemManage.ChooseItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        Destroy(gameObject, 0.15f);
        StartCoroutine( DropItem() );
    }

    private IEnumerator DropItem()
    {
        yield return new WaitForSeconds(0.14f);

        if(item.transform != null)
        {
            Instantiate(item, new Vector3(Mathf.Round(this.transform.position.x), Mathf.Round(this.transform.position.y)), this.transform.rotation);
        }
    }
}
