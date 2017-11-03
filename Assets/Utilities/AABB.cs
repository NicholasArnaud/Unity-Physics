using System.Runtime.CompilerServices;
using UnityEngine;


public class AABB : MonoBehaviour
{
    public Vector2 _min;
    public Vector2 _max;
    private Vector2 gameObjectPos;
    private Vector2 gameObjectScale;
    public new string name;

    public bool TestOnLap(AABB boxCollider, AABB boxCollidie)
    {
        float d1x = boxCollidie._min.x - boxCollider._max.x;
        float d1y = boxCollidie._min.y - boxCollider._max.y;
        float d2x = boxCollider._min.x - boxCollidie._max.x;
        float d2y = boxCollider._min.y - boxCollidie._max.y;

        if (d1x > 0 || d1y > 0)
            return false;
        if (d2x > 0 || d2y > 0)
            return false;
        return true;
    }

    void Start()
    {
        name = gameObject.name;
        gameObjectPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        gameObjectScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        _min = gameObjectPos - gameObjectScale / 2;
        _max = gameObjectPos + gameObjectScale / 2;
    }
    void Update()
    {
        gameObjectPos = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        gameObjectScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y);
        _min = gameObjectPos - gameObjectScale / 2;
        _max = gameObjectPos + gameObjectScale / 2;
    }
}
