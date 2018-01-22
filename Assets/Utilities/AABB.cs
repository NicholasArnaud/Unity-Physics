using System.Runtime.CompilerServices;
using UnityEngine;


public class AABB : MonoBehaviour
{
    public Vector2 _min;
    public Vector2 _max;
    private Vector2 gameObjectPos;
    private Vector2 gameObjectScale;
    public new string name;

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
