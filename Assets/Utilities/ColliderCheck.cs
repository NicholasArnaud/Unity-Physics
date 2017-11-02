using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public bool colliding;
    [SerializeField]
    private List<AABB> axisList;
    private Vector2 gameObjectPos;
    private Vector2 gameObjectScale;
    [SerializeField]
    List<AABB> ActiveList = new List<AABB>();

    // Use this for initialization
    void Start()
    {
        axisList = new List<AABB>();

        axisList.AddRange(FindObjectsOfType<AABB>());

    }

    // Update is called once per frame
    void Update()
    {
        ActiveList.Clear();
        axisList.Sort((ob, ob2) => ob._min.x.CompareTo(ob2._min.x)); //biggest to smallest        
        ActiveList.Add(axisList[0]);

        for (int i = 0; i < axisList.Count; i++)
        {
            for (int j = i + 1; j < axisList.Count; j++)
            {
                if (axisList[j]._min.x > axisList[i]._max.x)
                {
                    ActiveList.Remove(axisList[i]);
                    break;
                }
                if (axisList[j]._min.y > axisList[i]._max.y)
                {
                    ActiveList.Add(axisList[i]);
                    ActiveList.Remove(axisList[j]);
                    continue;
                }
                colliding = axisList[0].TestOnLap(axisList[i], axisList[j]);

            }
        }
        Debug.Log("NumOfAABBInActive: " + ActiveList.Count);
    }
}