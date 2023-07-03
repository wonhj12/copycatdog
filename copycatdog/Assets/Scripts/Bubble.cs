using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    //�����ϱ���� �ɸ��� �ð�
    public float explodeTime;

    //�ʿ��� ������Ʈ
    public GameObject Water;        //���ٱ�

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
