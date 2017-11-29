using System.Collections;
using System.Collections.Generic;
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
        [SerializeField]
        public Vector3 position;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private Vector3 acceleration;
        [SerializeField]
        private Vector3 force;
        [SerializeField]
        private float mass;
        

        public void AddForce(Vector3 f)
        {
            force += f;
        }

        public Vector3 Update(float deltaTime)
        {
            acceleration = force / mass;
            velocity += acceleration * deltaTime;
            position += velocity * deltaTime;
            return position;
        }
    }


    public class SpringDamper
    {
        private Particle p1, p2;
        private float Ks; //spring constant or tension
        private float Lo; //rest length

        public SpringDamper(Particle particle1, Particle particle2, float springKs, float springLo)
        {
            p1 = particle1;
            p2 = particle2;
            Ks = springKs;
            Lo = springLo;
        }

        public Vector3 Update()
        {
            Vector3 dir = -(p1.position - p2.position).normalized;
            float Distance = (p1.position - p2.position).magnitude;
            p1.AddForce(-Ks* dir*Lo);
            return dir;
        }
    }
}