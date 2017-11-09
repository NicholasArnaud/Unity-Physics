using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nick
{



    public class Utility
    {
        public static Vector3 RandomVector3
        {
            get
            {
                var x =Random.Range(-1, 1);
                
                var y =Random.Range(-1, 1);
                var z =Random.Range(-1, 1);
                var newv = new Vector3(x, y, z);
                while (newv.magnitude == 0)
                    newv = RandomVector3;
                return newv;
            }
        }
    }


    public class AgentFactory : MonoBehaviour
    {

        public int Count;
        public List<Agent> agents;
        public static List<AgentBehaviour> agentBehaviours;
        private static List<GameObject> gameObjects;


        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                agents.ForEach(a => a.Add_Force(Random.Range(-1, 1), Utility.RandomVector3));
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
                go.name = string.Format("{0} {1}", "Agent: ", i);

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
    }
}