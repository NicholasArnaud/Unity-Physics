using HookesLaw;
using UnityEngine;

public class SpringDampBehaviour : MonoBehaviour
{
    [SerializeField]
    private SpringDamper springDamper;

    public ParticleBehaviour particle1;
    public ParticleBehaviour particle2;
    // Use this for initialization
    void Start()
    {
        springDamper = new SpringDamper(particle1.particle,particle2.particle, 2,1);
    }

    // Update is called once per frame
    void Update()
    {
        springDamper.Update();
    }
}
