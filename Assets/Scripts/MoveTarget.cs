using UnityEngine;

public class MoveTarget : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var transformPosition = GetComponent<Transform>().position;
        if (Input.GetKey(KeyCode.W))
            transformPosition.y += 1;
        if (Input.GetKey(KeyCode.S))
            transformPosition.y -= 1;
        if (Input.GetKey(KeyCode.A))
            transformPosition.x -= 1;
        if (Input.GetKey(KeyCode.D))
            transformPosition.x += 1;
        GetComponent<Transform>().position = transformPosition;
    }

}
