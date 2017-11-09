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
        protected Vector3 position;
        protected Vector3 Force;

        public Vector3 Position
        { get { return position; } }


        protected internal abstract Vector3 Update_Agent(float deltaTime);

        protected internal abstract bool Add_Force(float speed, Vector3 force);

        public Transform _owner;
        public void Initialize(Transform owner)
        {
            Mass = 1;
            Velocity = Utility.RandomVector3;
            Acceleration = Utility.RandomVector3;
            position = Utility.RandomVector3;
            MaxSpeed = 5;
        }
    }
}
