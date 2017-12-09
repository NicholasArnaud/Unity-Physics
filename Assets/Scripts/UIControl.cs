using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HookesLaw;

public class UIControl : MonoBehaviour
{
    public SpringDampBehaviour particleList;
    public HookesLaw.ParticleBehaviour selectedParticle;
    public Slider particleSlider;
    public Text particleNumText;
    public Text particleLockText;
    private bool updated = false;
    public int setter = 0;

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (!updated)
        {
            UpdateUI();
            updated = true;
        }
    }


    public void UpdateUI()
    {
        if (particleList != null || particleList.particles.Count > 0)
        {
            particleSlider.maxValue = particleList.particles.Count - 1;
            selectedParticle = particleList.particles[(int)particleSlider.value].particle;
            particleList.particles[(int)particleSlider.value].gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            particleNumText.text = "Particle: " + selectedParticle.name.ToString();
            if (selectedParticle.Locked == false)
                particleLockText.text = "Unlocked!";
            else
                particleLockText.text = "Locked!";
        }
        else
            updated = false;

        for (int i = 0; i < particleList.particles.Count; i++)
        {
            ParticleBehaviour sPart = particleList.particles[i];
            if (selectedParticle != sPart.particle)
            {
                sPart.gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
    }


    public void LockParticle()
    {
        updated = false;
        if (selectedParticle.Locked == false)
        {
            selectedParticle.Locked = true;
            particleLockText.text = "Locked!";
        }
        else
        {
            selectedParticle.Locked = false;
            particleLockText.text = "Unlocked!";
        }
    }

    public void Reset()
    {
        switch (setter)
        {
            case 0:
                {
                    setter++;
                    particleList.Destroy();
                    particleList.SetUp();
                    for (int i = 0; i < particleList.sizeNByN * particleList.sizeNByN - 1; i++)
                    {
                        if (i % particleList.sizeNByN >= particleList.sizeNByN - 1)
                        {
                            particleList.particles[i].particle.Locked = true;
                        }
                        if (i == particleList.sizeNByN * particleList.sizeNByN - 2)
                            particleList.particles[i + 1].particle.Locked = true;
                    }
                    updated = false;
                    break;
                }
            case 1:
                {
                    setter++;
                    particleList.Destroy();
                    particleList.SetUp();
                    for (int i = 0; i < particleList.sizeNByN * particleList.sizeNByN - 1; i++)
                    {
                        if (i % particleList.sizeNByN * particleList.sizeNByN == 0)
                        {
                            particleList.particles[i].particle.Locked = true;
                        }
                    }
                    updated = false;
                    break;
                }
            case 2:
                {
                    setter++;
                    particleList.Destroy();
                    particleList.SetUp();
                    for (int i = 0; i <= particleList.sizeNByN - 1; i++)
                    {
                        particleList.particles[i].particle.Locked = true;
                    }
                    updated = false;
                    break;
                }
            case 3:
                {
                    setter++;
                    particleList.Destroy();
                    particleList.SetUp();
                    for (int i = particleList.particles.Count - 1; i >= particleList.sizeNByN * particleList.sizeNByN - particleList.sizeNByN; i--)
                    {
                        particleList.particles[i].particle.Locked = true;

                        if (i == particleList.sizeNByN * particleList.sizeNByN - 2)
                            particleList.particles[i + 1].particle.Locked = true;
                    }
                    updated = false;
                    break;
                }

            case 4:
                {
                    setter++;
                    particleList.Destroy();
                    particleList.SetUp();
                    for (int i = 0; i < particleList.sizeNByN * particleList.sizeNByN - 1; i++)
                    {
                        if (i < particleList.sizeNByN || i >= (particleList.sizeNByN * particleList.sizeNByN) - particleList.sizeNByN)
                        {
                            particleList.particles[i].particle.Locked = true;
                            if (i == particleList.sizeNByN * particleList.sizeNByN - 2)
                                particleList.particles[i + 1].particle.Locked = true;
                        }
                    }
                    updated = false;
                    break;
                }



            default:
                {
                    setter = 0;
                    foreach( var p in particleList.particles)
                    {
                        p.particle.Locked = false;
                    }
                    break;
                }

        }

    }
}
