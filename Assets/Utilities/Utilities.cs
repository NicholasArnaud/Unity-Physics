using UnityEngine;


public class Utilities
{
    public class AABB
    {
        public Vector2 _min;
        public Vector2 _max;

       public AABB(Vector2 min, Vector2 max)
        {
            _max = max;
            _min = min;
        }
    }

    public bool TestOnLap(AABB boxCollider, AABB boxCollidie)
    {
        if (boxCollider._min.x < boxCollidie._max.x &&
            boxCollider._min.x > boxCollidie._min.x ||
            boxCollider._max.x < boxCollidie._max.x &&
            boxCollider._max.x > boxCollidie._min.x)
            if (boxCollider._min.y < boxCollidie._max.y &&
                boxCollider._min.y > boxCollidie._min.y ||
                boxCollider._max.y < boxCollidie._max.y &&
                boxCollider._max.y > boxCollidie._min.y)
                return true;
        return false;
    }
}
