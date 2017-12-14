using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using HookesLaw;

public class UIControl : MonoBehaviour
{
    public SpringDampBehaviour particleList;
    private bool updated;
    public int setter;
    public float timer;
    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (!updated || timer >=3)
        {
            timer = 0;
            UpdateUI();
            updated = true;
        }
    }


    public void UpdateUI()
    {
        if (particleList != null || particleList.particles.Count > 0)
        {
            for (int i = 0; i < particleList.particles.Count; i++)
            {
                if (particleList.particles[i].particle.Locked)
                    particleList.particles[i].GetComponent<MeshRenderer>().material.color = Color.black;
                else
                {
                    particleList.particles[i].GetComponent<MeshRenderer>().material.color = Color.cyan;
                }
            }
        }
        else
            updated = false;
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
