using UnityEngine;

namespace Nick
{
    public abstract class Agent : ScriptableObject
    {
        protected float Mass;
        protected Vector3 Velocity;
        protected float MaxSpeed;
        protected float Speed;
        protected Vector3 Acceleration;
        protected Vector3 Position;
    
        public abstract Vector3 Update_Agent(Transform transform);

        public abstract bool Add_Force(float speed, Vector3 force);
    }
}
