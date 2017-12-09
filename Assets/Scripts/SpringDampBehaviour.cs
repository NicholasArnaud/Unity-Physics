using System.Collections.Generic;
using HookesLaw;
using UnityEngine;

namespace HookesLaw
{
    public class SpringDampBehaviour : MonoBehaviour
    {
        public int sizeNByN;
        public float springTension;
        public float springDamper;
        public float restLength;
        public List<SpringDamper> SpringDampers = new List<SpringDamper>();
        public List<AerodynamicBehaviour.Triangle> triangles = new List<AerodynamicBehaviour.Triangle>();
        [HideInInspector]
        public List<global::ParticleBehaviour> particles;
        // Use this for initialization
        void Start()
        {
            SetUp();
        }

        // Update is called once per frame
        void Update()
        {
            foreach (var sD in SpringDampers)
            {
                sD.ApplySpring();
            }

            foreach (AerodynamicBehaviour.Triangle triangle in triangles)
            {
                AerodynamicBehaviour.WindForce(triangle);
            }
            CheckParticleDistance();
        }

        private void OnDrawGizmos()
        {
            foreach (var damper in SpringDampers)
            {
                Gizmos.DrawLine(damper.p1.position, damper.p2.position);
            }
        }


        void AssignDampers(List<global::ParticleBehaviour> particles)
        {
            SpringDamper sD;

            for (int i = 0; i < sizeNByN * sizeNByN - 1; i++)
            {
                //Setting Verticals
                if (i % sizeNByN < sizeNByN - 1)
                {
                    sD = new SpringDamper(particles[i].particle, particles[i + 1].particle, springTension, springDamper, restLength);
                    SpringDampers.Add(sD);
                    if (i % sizeNByN == 0)
                    {
                        sD = new SpringDamper(particles[i].particle, particles[i + 2].particle, springTension, springDamper, restLength);
                        SpringDampers.Add(sD);
                    }

                }

                //Setting Horizontals
                if (i < sizeNByN * sizeNByN - sizeNByN)
                {
                    sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN].particle, springTension, springDamper, restLength);
                    SpringDampers.Add(sD);
                    if (i % sizeNByN == 0)
                    {
                        sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN + 1].particle, springTension, springDamper, restLength);
                        SpringDampers.Add(sD);
                    }
                }

                //Setting diagnals
                if (i < sizeNByN * sizeNByN - sizeNByN && i % sizeNByN < sizeNByN - 1)
                {
                    sD = new SpringDamper(particles[i + 1].particle, particles[i + sizeNByN].particle, springTension, springDamper, restLength);
                    SpringDampers.Add(sD);
                }
                if (i < sizeNByN * sizeNByN - sizeNByN && i % sizeNByN < sizeNByN - 1)
                {
                    sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN + 1].particle, springTension, springDamper, restLength);
                    SpringDampers.Add(sD);
                }
            }
        }

        void CreateTriangles(List<global::ParticleBehaviour> particles)
        {
            for (int i = 0; i < sizeNByN * sizeNByN - 1; i++)
            {
                if (i % sizeNByN != sizeNByN - 1 && i < particles.Count - sizeNByN)
                {
                    int triBase = i;
                    int triPeak = i + 1;
                    int triEdge = i + sizeNByN;

                    AerodynamicBehaviour.Triangle triangle = new AerodynamicBehaviour.Triangle(particles[triBase].particle, particles[triEdge].particle, particles[triPeak].particle);
                    triangles.Add(triangle);

                    triBase = i + sizeNByN + 1;
                    triPeak = i + 1;
                    triEdge = i + sizeNByN;

                    triangle = new AerodynamicBehaviour.Triangle(particles[triBase].particle, particles[triEdge].particle, particles[triPeak].particle);
                    triangles.Add(triangle);
                }

            }
        }

        void CreateParticles(int sizeNbyN)
        {
            particles = new List<global::ParticleBehaviour>();
            for (int i = 0; i < sizeNbyN; i++)
            {
                for (int j = 0; j < sizeNbyN; j++)
                {
                    GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    particle.gameObject.name = "p" + (i * sizeNbyN + j);
                    particle.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Destroy(particle.gameObject.GetComponent<SphereCollider>());
                    particle.transform.SetParent(this.gameObject.transform);
                    particle.transform.position = new Vector3(i, j, 0);
                    particle.AddComponent<global::ParticleBehaviour>();
                    particles.Add(particle.GetComponent<global::ParticleBehaviour>());
                }
            }
        }

        public void CheckParticleDistance()
        {
            List<SpringDamper> temp = SpringDampers;
            for (int i = 0; i < SpringDampers.Count; i++)
            {
                if (restLength * 2 < Vector3.Distance(SpringDampers[i].p2.position, SpringDampers[i].p1.position))
                {
                    temp.Remove(SpringDampers[i]);
                }
            }
            SpringDampers = temp;
        }

        public void SetUp()
        {
            CreateParticles(sizeNByN);
            foreach (var particleBehaviour in particles)
            {
                particleBehaviour.particle.name = particleBehaviour.name;
            }
            AssignDampers(particles);
            CreateTriangles(particles);
        }

        public void Destroy()
        {
            SpringDampers.Clear();
            foreach (var p in particles)
            {
                Destroy(p.gameObject);
            }
            particles.Clear();
            triangles.Clear();
        }
    }

}