using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderCheck : MonoBehaviour
{
    public Utilities Collide;
	// Use this for initialization
	void Start () {
        Collide = new Utilities();
		Utilities.AABB Box1 = new Utilities.AABB(new Vector2(0,0), new Vector2(1,1));
        Utilities.AABB Box2 = new Utilities.AABB(new Vector2(0.5f, 0.5f), new Vector2(1.5f, 1.5f));
	    bool isHitting =Collide.TestOnLap(Box1, Box2);
	    int one = 1;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
