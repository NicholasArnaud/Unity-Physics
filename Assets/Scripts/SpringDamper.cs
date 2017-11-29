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
        private Vector3 position;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private Vector3 acceleration;
        [SerializeField]
        private float mass;
        [SerializeField]
        private Vector3 force;

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
        private float Ks; //spring constant
        private float Lo; //rest length

        public SpringDamper()
        {
            
        }

        public SpringDamper(Particle particle1, Particle particle2, float springKs, float springLo)
        {
            p1 = particle1;
            p2 = particle2;
            Ks = springKs;
            Lo = springLo;
        }

    }
}