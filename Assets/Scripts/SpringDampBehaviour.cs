using System;
using System.Collections.Generic;
using HookesLaw;
using UnityEngine;

public class SpringDampBehaviour : MonoBehaviour
{
    public int sizeNByN;
    public List<SpringDamper> SpringDampers = new List<SpringDamper>();

    //  public ParticleBehaviour particle1;
    //  public ParticleBehaviour particle2;

    private List<ParticleBehaviour> particles;
    // Use this for initialization
    void Start()
    {
        CreateParticles(sizeNByN);
        foreach (var particleBehaviour in particles)
        {
            particleBehaviour.particle.name = particleBehaviour.name;
        }
        AssignDampers(particles);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var sD in SpringDampers)
        {
            sD.ApplySpring();
        }

    }
    private void OnDrawGizmos()
    {
        foreach (var damper in SpringDampers)
        {
            Gizmos.DrawLine(damper.p1.position, damper.p2.position);
        }
    }


    void AssignDampers(List<ParticleBehaviour> particles)
    {
        int phase1 = 0;
        SpringDamper sD;

        for (int i = 0; i < sizeNByN * sizeNByN-1; i++)
        {
            if (i % sizeNByN != sizeNByN)
            {
                sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN - 1].particle, 10, 0.5f, 1);
                SpringDampers.Add(sD);
                if (i < sizeNByN * sizeNByN - 1)
                {
                    sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN].particle, 10, 0.5f, 1);
                    SpringDampers.Add(sD);
                }
            }

            if (i % sizeNByN != sizeNByN * sizeNByN - 1)
            {
                sD = new SpringDamper(particles[i].particle, particles[i + sizeNByN - 1].particle, 10, 0.5f, 1);
                SpringDampers.Add(sD);
            }
        }


        //foreach (var particle in particles)
        //{
        //    if (phase1 >= particles.Count)
        //        phase1 = 0;
        //    if (particle.particle == particles[phase1].particle)
        //        phase1++;
        //    sD = new SpringDamper(particle.particle, particles[phase1].particle, 10, 0.5f, 1);
        //    SpringDampers.Add(sD);
        //    phase1 += 1;
        //}

    }

    void CreateParticles(int sizeNByN)
    {
        particles = new List<ParticleBehaviour>();
        for (int i = 0; i < sizeNByN; i++)
        {
            for (int j = 0; j < sizeNByN; j++)
            {
                int test = i * sizeNByN + j;
                GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                particle.gameObject.name = "p" + (i * sizeNByN + j);
                particle.gameObject.GetComponent<MeshRenderer>().enabled = false;
                particle.gameObject.GetComponent<SphereCollider>().enabled = false;
                particle.transform.SetParent(this.gameObject.transform);
                particle.transform.position = new Vector3(i, j, 0);
                particle.AddComponent<ParticleBehaviour>();
                particles.Add(particle.GetComponent<ParticleBehaviour>());
            }
        }
    }

}
