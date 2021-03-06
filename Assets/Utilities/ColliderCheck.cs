﻿using System.Collections;
using System.Collections.Generic;
using System.Deployment.Internal;
using System.Linq;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public bool colliding;
    [SerializeField]
    private List<AABB> axisList;
    [SerializeField]
    List<AABB> ActiveList = new List<AABB>();
    public List<AABB> CollidingObj = new List<AABB>();

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
        CollidingObj.Clear();
        colliding = false;
        axisList.Sort((ob, ob2) => ob._min.x.CompareTo(ob2._min.x)); //biggest to smallest        
        ActiveList.Add(axisList[0]);

        for (int i = 0; i < axisList.Count; i++)
        {
            for (int j = i + 1; j < axisList.Count; j++)
            {
                if (axisList[i]._max.x < axisList[j]._min.x)
                {
                    //ActiveList.Remove(axisList[i]);
                    break;
                }
                //Checks overlapping on x and y axis
                if (axisList[i]._min.x < axisList[j]._max.x && axisList[i]._max.x > axisList[j]._min.x &&
                    axisList[i]._min.y < axisList[j]._max.y && axisList[i]._max.y > axisList[j]._min.y)
                {
                    ActiveList.Add(axisList[i]);
                    //Handling collision here
                    colliding = TestOnLap(axisList[i], axisList[j]);
                    if (colliding)
                    {
                        CollidingObj.Add(axisList[i]);
                        CollidingObj.Add(axisList[j]);
                    }
                }
            }
        }
        if (CollidingObj.Count > 2)
        {
            List<AABB> tempList = CollidingObj;
            for (int i = CollidingObj.Count - 1; i >= 0; i--)
            {
                for (int j = 0; j < CollidingObj.Count - 1; j++)
                {
                    if (CollidingObj[j].name == CollidingObj[i].name && i != j)
                        tempList.RemoveAt(j);
                }
            }
            CollidingObj = tempList;
        }
        foreach (var aabb in CollidingObj)
        {
            aabb.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        
        Debug.Log("NumOfAABBInActive: " + ActiveList.Count);
    }


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
}