using UnityEngine;

namespace Nick
{
    public class BoidBehaviour : AgentBehaviour
    {
        
        public void SetBoid(Boid b)
        {
            
            agent = b;
            agent.Initialize(transform);
        }

        void Update()
        {
            Vector3.Distance(transform.position, Vector3.zero);
            {
                GetComponent<MeshRenderer>().material.color = Color.black;
                agent.Add_Force(10,transform.position - Vector3.zero);
            }
        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.localPosition = agent.Update_Agent(Time.deltaTime); 
        }
    }


}
