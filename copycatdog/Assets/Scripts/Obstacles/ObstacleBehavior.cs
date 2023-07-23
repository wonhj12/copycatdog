using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehavior : MonoBehaviour
{
    //장애물 위치정보
    private Vector3 obstaclePos;
    public GameObject item;

    void Start()
    {
        obstaclePos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        Destroy(gameObject, 0.15f);
        DropItem();
    }

    private void DropItem()
    {
        if( Random.Range(0.0f, 1.0f) >= 0.7 ){
            // 아이템 소환, 수정 필요할듯 ..
            GameObject itemPrefab = Instantiate(item);
            itemPrefab.transform.position = obstaclePos;
        }
    }
}
