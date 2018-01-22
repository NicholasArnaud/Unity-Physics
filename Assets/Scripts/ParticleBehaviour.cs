using System;
using UnityEngine;


namespace HookesLaw
{
    public class ParticleBehaviour : MonoBehaviour
    {
        [SerializeField]
        public Particle particle;
        
        // Use this for initialization
        void Awake()
        {
            particle = new Particle(transform.position, Vector3.zero, 1);
        }

        // Update is called once per frame
       void Update()
        {
            particle.position = transform.position;
            transform.position = particle.Update(Time.fixedDeltaTime);
        }

    }

}