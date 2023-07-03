using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //폭발하기까지 걸리는 시간
    public float explodeTime;

    //필요한 컴포넌트
    public GameObject Water;        //물줄기

    private void Start()
    {
        StartCoroutine(WaterSplash());
        Destroy(this.gameObject, explodeTime);
    }

    private IEnumerator WaterSplash()
    {
        yield return new WaitForSeconds(explodeTime - 0.2f);

        Instantiate(Water, new Vector2(this.transform.position.x + 1, this.transform.position.y), Quaternion.identity);
        Instantiate(Water, new Vector2(this.transform.position.x - 1, this.transform.position.y), Quaternion.identity);
        Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y + 1), Quaternion.identity);
        Instantiate(Water, new Vector2(this.transform.position.x, this.transform.position.y - 1), Quaternion.identity);
    }
}
