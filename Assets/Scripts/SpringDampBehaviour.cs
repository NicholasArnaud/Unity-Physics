using System.Collections.Generic;
using HookesLaw;
using UnityEngine;

public class SpringDampBehaviour : MonoBehaviour
{
    [SerializeField]
    private SpringDamper sD;

    public ParticleBehaviour particle1;
    public ParticleBehaviour particle2;

    public List<ParticleBehaviour> particles;
    // Use this for initialization
    void Start()
    {
        sD = new SpringDamper(particle1.particle, particle2.particle, 10, .5f, 3);
    }

    // Update is called once per frame
    void Update()
    {
        sD.ApplySpring();
    }
}
