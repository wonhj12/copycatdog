using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("X�� �� ��谪")]
    public float xUpperBound;
    public float xLowerBound;

    [Header("Y�� �� ��谪")]
    public float yUpperBound;
    public float yLowerBound;


    public void checkBounds(Transform trans)
    {
        if(trans.position.x > xUpperBound)
        {
            trans.position = new Vector2(xLowerBound, trans.position.y);
        }
        if (trans.position.x < xLowerBound)
        {
            trans.position = new Vector2(xUpperBound, trans.position.y);
        }

        if (trans.position.y > yUpperBound)
        {
            trans.position = new Vector2(trans.position.x, yLowerBound);
        }
        if (trans.position.y < yLowerBound)
        {
            trans.position = new Vector2(trans.position.x, yUpperBound);
        }
    }

}
