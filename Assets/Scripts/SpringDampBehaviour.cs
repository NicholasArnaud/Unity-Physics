﻿using System.Collections.Generic;
using HookesLaw;
using UnityEngine;

public class SpringDampBehaviour : MonoBehaviour
{
    public int sizeNByN;
    public float springTension;
    public float springDamper;
    public float restLength;
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

            //Locking particles
            if (i % sizeNByN == 0 || i % sizeNByN == sizeNByN - 1)
            {
                particles[i].particle.Locked = true;
            }
            if (i == sizeNByN* sizeNByN-2)
            {
                particles[i+1].particle.Locked = true;
            }
        }
    }

    void CreateParticles(int sizeNByN)
    {
        particles = new List<ParticleBehaviour>();
        for (int i = 0; i < sizeNByN; i++)
        {
            for (int j = 0; j < sizeNByN; j++)
            {
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
