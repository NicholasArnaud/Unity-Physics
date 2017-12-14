using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HookesLaw;
using Particle = HookesLaw.Particle;

public class Clothe : MonoBehaviour
{

    private SpringDampBehaviour springDB;
    public GameObject ThisGameObject;

    // Use this for initialization
    void Start()
    {
        ThisGameObject = gameObject;
        springDB = new SpringDampBehaviour();
        springDB.SetUp();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var sD in springDB.SpringDampers)
        {
            sD.ApplySpring();
        }

        foreach (AerodynamicBehaviour.Triangle triangle in springDB.triangles)
        {
            AerodynamicBehaviour.WindForce(triangle);
        }
        springDB.CheckParticleDistance();
    }

    private void OnDrawGizmos()
    {
        foreach (var damper in springDB.SpringDampers)
        {
            Gizmos.DrawLine(damper.p1.position, damper.p2.position);
        }
    }

    void CreateParticles(int sizeNbyN)
    {
        springDB.particles = new List<Particle>();
        for (int i = 0; i < sizeNbyN; i++)
        {
            for (int j = 0; j < sizeNbyN; j++)
            {
                GameObject particle = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                particle.gameObject.name = "p" + (i * sizeNbyN + j);
                particle.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                Destroy(particle.gameObject.GetComponent<SphereCollider>());
                particle.transform.SetParent(gameObject.transform);
                particle.transform.position = new Vector3(i, j, 0);
                particle.AddComponent<ParticleBehaviour>();
                springDB.particles.Add(particle.GetComponent<Particle>());
            }
        }
    }

    public void Destroy()
    {
        springDB.SpringDampers.Clear();
        foreach (var p in springDB.particles)
        {
          //  Destroy(p.gameObject);
        }
        springDB.particles.Clear();
        springDB.triangles.Clear();
    }
}
