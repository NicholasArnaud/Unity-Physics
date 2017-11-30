using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField]
    public HookesLaw.Particle particle;
    // Use this for initialization
    void Awake()
    {
        particle = new HookesLaw.Particle(transform.position,Vector3.zero, 1);
    }

    // Update is called once per frame
    public void Update()
    {
        transform.position = particle.Update(Time.fixedDeltaTime);
    }
}
