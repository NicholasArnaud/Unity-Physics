using UnityEngine;

namespace Nick
{
    public abstract class Agent : ScriptableObject
    {
        protected float Mass;
        protected internal Vector3 Velocity;
        protected Vector3 Acceleration;
        protected internal Vector3 position;
        protected internal float MaxSpeed;
        protected Vector3 Force;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        

        protected internal abstract Vector3 Update_Agent(float deltaTime);

        protected internal abstract bool Add_Force(float speed, Vector3 force);

        public Transform _owner;
        public void Initialize(Transform owner)
        {
            Mass = 1;
            MaxSpeed = 3;
            Velocity = Utility.RandomVector3;
            Acceleration = Utility.RandomVector3;

            var x = Random.Range(-50, 50);
            var y = Random.Range(-50, 50);
            var z = Random.Range(-50, 50);
            var newv = new Vector3(x, y, z);
            position = newv;
        }
    }
}
