using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Nick
{



    public class Utility
    {
        public static Vector3 RandomVector3
        {
            get
            {
                var x = Random.Range(-1, 1);
                var y = Random.Range(-1, 1);
                var z = Random.Range(-1, 1);
                var newv = new Vector3(x, y, z);
                while (newv.magnitude == 0.0f)
                    newv = RandomVector3;
                return newv;
            }
        }
    }


    public class AgentFactory : MonoBehaviour
    {

        public int Count;
        public Transform Target;
        public List<Agent> agents;
        public static List<AgentBehaviour> agentBehaviours;
        private static List<GameObject> gameObjects;
        public void Update()
        {
            
            foreach (var agent in agents)
            {
                List<Slider> slider = new List<Slider>(FindObjectsOfType<Slider>());
                Vector3 v1 = Cohesion(agent as Boid)* slider[0].value;
                Debug.DrawLine(agent.position,agent.position+v1.normalized);
                Vector3 v2 = Dispersion(agent as Boid)* slider[1].value;
                Debug.DrawLine(agent.position, agent.position+ v2.normalized);
                Vector3 v3 = Alignment(agent as Boid)* slider[2].value;
                Debug.DrawLine(agent.position, agent.position+v3.normalized);
                Vector3 v4 = Bound(agent as Boid);
                Vector3 v5 = targetPlace(agent as Boid);
                //Setting velocity and positions
                agent.Velocity = (agent.Velocity + v1 + v2 + v3 +v4 + v5);
                LimitVelocity(agent as Boid);
                agent.position = agent.position + agent.Velocity;
                //agent.Add_Force(2, v1 + v2 + v3);
            }
        }

        [ContextMenu("Create")]
        public void Create()
        {
            agents = new List<Agent>();
            agentBehaviours = new List<AgentBehaviour>();
            for (int i = 0; i < Count; i++)
            {
                var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                go.transform.SetParent(transform);
                Destroy(go.GetComponent<SphereCollider>());
                go.name = string.Format("{0} {1}", "Agent: ", i);
                go.transform.position = Utility.RandomVector3;

                var behaviour = go.AddComponent<BoidBehaviour>();
                var boid = ScriptableObject.CreateInstance<Boid>();

                boid.name = go.name;
                agents.Add(boid);
                agentBehaviours.Add(behaviour);
                behaviour.SetBoid(boid);
            }
        }

        [ContextMenu("Destroy")]
        public void Destroy()
        {
            foreach (var agentBehaviour in agentBehaviours)
            {
                DestroyImmediate(agentBehaviour.gameObject);
            }
            agents.Clear();
            agentBehaviours.Clear();
        }

        
        protected Vector3 Cohesion(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in agents)
            {
                if (b != bj)
                    Force = Force + b.position;
            }
            Force = Force / (agents.Count -1);
            return (Force - bj.position)/ 100;
        }

        protected Vector3 Dispersion(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in agents)
            {
                if (b != bj)
                    if (Vector3.Magnitude(b.position - bj.position) < 5f)
                    {
                        Force = Force - (b.position - bj.position);
                    }
            }
            return Force;
        }

        protected Vector3 Alignment(Boid bj)
        {
            var Force = Vector3.zero;
            foreach (var b in agents)
            {
                if (b != bj)
                {
                    Force = Force + b.Velocity;
                }
            }
           
            Force = Force / (agents.Count- 1);
            return (Force - bj.Velocity)/8;
        }

        protected void LimitVelocity(Boid bj)
        {
            var vLim =1.0f;
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

        protected Vector3 Bound(Boid b)
        {
            int Xmin = -50, Xmax = 50, Ymin = -50, Ymax= 50, Zmin = -50, Zmax = 50;
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
    }
}