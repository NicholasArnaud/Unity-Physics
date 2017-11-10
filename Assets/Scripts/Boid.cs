using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

namespace Nick
{
    [CreateAssetMenu(menuName = "Agents/Boid")]
    public class Boid : Agent
    {


        protected internal override bool Add_Force(float mag, Vector3 dir)
        {
            if (mag == 0)
                return false;
            var f = mag * dir;
            Force += f;
            return true;
        }

        protected internal override Vector3 Update_Agent(float deltaTime)
        {
            UnityEngine.Assertions.Assert.IsTrue(Mass > 0);
            Acceleration = Force * 1 / Mass;
            Velocity += Acceleration * deltaTime;
            Velocity = Vector3.ClampMagnitude(Velocity, MaxSpeed);            
            position += Velocity * deltaTime;
            return position;
        }

        
    }
}
