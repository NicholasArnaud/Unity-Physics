using UnityEngine;


namespace HookesLaw
{
    [System.Serializable]
    public class Particle
    {
        public Particle()
        {
            force =Vector3.zero;
            position = Vector3.zero;
            velocity = Vector3.zero;
            acceleration = Vector3.zero;
            mass = 1;
        }

        public Particle(Vector3 p, Vector3 v, float m)
        {
            force = Vector3.zero;
            position = p;
            velocity = v;
            mass = m;
            acceleration = Vector3.zero;
        }
        [HideInInspector]
        public Vector3 position;
        [HideInInspector]
        public Vector3 velocity;
        private Vector3 acceleration;
        private Vector3 force;
        private float mass;
        public string name;
        public bool Locked;

        public void AddForce(Vector3 f)
        {
            if (!Locked)
            {
                Vector3 Gravity = new Vector3(0, -9.81f, 0);
                Gravity = Gravity *.5f;
                force += f + Gravity;
            }
        }
        public Vector3 Update(float deltaTime)
        {
            acceleration = force / mass;
            velocity += acceleration * deltaTime;
            position += velocity * deltaTime;
            force = Vector3.zero;            
            return position;
        }
    }

    [System.Serializable]
    public class SpringDamper
    {
        public Particle p1, p2;
        [HideInInspector]
        public float Ks; //spring constant or tension
        [HideInInspector]
        public float Kd; //damping factor
        [HideInInspector]
        public float Lo; //rest length

        public SpringDamper(Particle particle1, Particle particle2, float springKs,float springKd, float springLo)
        {
            p1 = particle1;
            p2 = particle2;
            Ks = springKs;
            Kd = springKd;
            Lo = springLo;
        }
        public void ApplySpring()
        {
            var e = p2.position - p1.position;
            var l = Vector3.Magnitude(e);
            var eUnit = e / l;

            var v1 = Vector3.Dot(eUnit, p1.velocity);
            var v2 = Vector3.Dot(eUnit, p2.velocity);

            var F = -Ks * (Lo - l) - Kd * (v1 - v2);
            var f1 = F * eUnit;
            var f2 = -f1;

            p1.AddForce(f1);
            p2.AddForce(f2);
        }
    }
}