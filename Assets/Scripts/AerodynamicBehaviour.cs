using System;
using System.Collections.Generic;
using UnityEngine;

namespace HookesLaw
{
    public class AerodynamicBehaviour
    {
        public static float windDensity = 1f;
        public static Vector3 Dir = new Vector3(1,1,1);
        public static float dragCo = 0.5f;
        public List<Triangle> triangles;

        [System.Serializable]
        public class Triangle
        {
            public ParticleBehaviour _p1;
            public ParticleBehaviour _p2;
            public ParticleBehaviour _p3;

            public Triangle(ParticleBehaviour p1, ParticleBehaviour p2, ParticleBehaviour p3)
            {
                _p1 = p1;
                _p2 = p2;
                _p3 = p3;
            }
        }

        public static void WindForce(Triangle triangle)
        {
            ParticleBehaviour _p1 = triangle._p1;
            ParticleBehaviour _p2 = triangle._p2;
            ParticleBehaviour _p3 = triangle._p3;

            Vector3 v1 = _p1.velocity;
            Vector3 v2 = _p2.velocity;
            Vector3 v3 = _p3.velocity;

            Vector3 surface = (v1 + v2 + v3) / 3;
            Vector3 v = surface - Dir;
            Vector3 nNorm = Vector3.Cross(_p2.position - _p1.position,
                _p3.position - _p1.position).normalized;
            float a = Vector3.Dot(v, nNorm) / v.magnitude;
            Vector3 vAN = -.5f * windDensity * (v.magnitude * v.magnitude) * dragCo * a * nNorm;
            _p1.AddForce(vAN / 3);
            _p2.AddForce(vAN / 3);
            _p3.AddForce(vAN / 3);
        }
    }
}