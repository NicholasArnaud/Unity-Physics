using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Nick
{

    public class FlockingBehaviour : MonoBehaviour
    {
        public GameObject agentFactory;
        public float cohVal, disVal, aligVal, tarVal, bounVal;
        public Text boundText;
        public Transform Target;
        private List<Boid> boids= new List<Boid>();

        // Use this for initialization
        void Start()
        {
            agentFactory.GetComponent<AgentFactory>().Create();
            boids = agentFactory.GetComponent<AgentFactory>().GetBoids();
        }

        // Update is called once per frame
        void Update()
        {
            if (boids.Count == 0||boids != agentFactory.GetComponent<AgentFactory>().GetBoids())
                UpdateBoids();
            foreach (var agent in boids)
            {
                Vector3 v1 = Cohesion(agent) * cohVal;
                Vector3 v2 = Dispersion(agent) * disVal;
                Vector3 v3 = Alignment(agent) * aligVal;
                Vector3 v4 = Bound(agent);
                Vector3 v5 = targetPlace(agent) * tarVal;
                //Setting velocity and positions
                agent.Velocity = (agent.Velocity + v1 + v2 + v3 + v4 + v5);
                LimitVelocity(agent);
                agent.position = agent.position + agent.Velocity;
            }
        }

        public void UpdateBoids()
        {
            boids = agentFactory.GetComponent<AgentFactory>().GetBoids();
        }

        protected Vector3 Cohesion(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in boids)
            {
                if (b != bj)
                    Force = Force + b.position;
            }
            Force = Force / (boids.Count - 1);
            return (Force - bj.position) / 100;
        }
        public void setCohesion(float newValue)
        {
            cohVal = newValue;
        }

        protected Vector3 Dispersion(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in boids)
            {
                if (b != bj)
                    if (Vector3.Magnitude(b.position - bj.position) < 5f)
                    {
                        Force = Force - (b.position - bj.position);
                    }
            }
            return Force;
        }
        public void setDispersion(float newValue)
        {
            disVal = newValue;
        }

        protected Vector3 Alignment(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in boids)
            {
                if (b != bj)
                {
                    Force = Force + b.Velocity;
                }
            }

            Force = Force / (boids.Count - 1);
            return (Force - bj.Velocity) / 8;
        }
        public void setAlignment(float newValue)
        {
            aligVal = newValue;
        }

        protected void LimitVelocity(Boid bj)
        {
            var vLim = 1.0f;
            var vector = Vector3.zero;
            if (bj.Velocity.magnitude > vLim)
            {
                bj.Velocity = (bj.Velocity / bj.Velocity.magnitude) * vLim;
            }
        }

        protected Vector3 targetPlace(Boid b)
        {
            var Place = Target.position;
            return (Place - b.position) / 100;
        }
        public void setTarget(float newValue)
        {
            tarVal = newValue;
        }


        protected Vector3 Bound(Boid b)
        {
            float Xmin = -bounVal, Xmax = bounVal, Ymin = -bounVal, Ymax = bounVal, Zmin = -bounVal, Zmax = bounVal;
            var Force = Vector3.zero;
            if (b.position.x < Xmin)
                Force.x = 10;
            else if (b.position.x > Xmax)
                Force.x = -10;
            if (b.position.y < Ymin)
                Force.y = 10;
            else if (b.position.y > Ymax)
                Force.y = -10;
            if (b.position.z < Zmin)
                Force.z = 10;
            else if (b.position.z > Zmax)
                Force.z = -10;
            return Force;
        }
        public void setBound(float newValue)
        {
            bounVal = newValue;
            boundText.text = "Boundary Size: " + bounVal;
        }
    }
}