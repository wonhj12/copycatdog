using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCheck : MonoBehaviour
{
    [Header("XÃà ¸Ê °æ°è°ª")]
    [SerializeField] private float xUpperBound;
    [SerializeField] private float xLowerBound;

    [Header("YÃà ¸Ê °æ°è°ª")]
    [SerializeField] private float yUpperBound;
    [SerializeField] private float yLowerBound;

    private bool result = false;

    private MapController map;

    private void Awake()
    {
        map = FindObjectOfType<MapController>();
        xUpperBound = map.xUpperBound;
        xLowerBound = map.xLowerBound;
        yUpperBound = map.yUpperBound;
        yLowerBound = map.yLowerBound;
    }

    public bool checkBounds(Transform trans)
    {
        if (trans.position.x > xUpperBound)
        {
            trans.position = new Vector2(xLowerBound, trans.position.y);
            result = true;
        }

        if (trans.position.x < xLowerBound)
        {
            trans.position = new Vector2(xUpperBound, trans.position.y);
            result = true;
        }

        if (trans.position.y > yUpperBound)
        {
            trans.position = new Vector2(trans.position.x, yLowerBound);
            result = true;
        }

        if (trans.position.y < yLowerBound)
        {
            trans.position = new Vector2(trans.position.x, yUpperBound);
            result = true;
        }

        return result;
    }

}
