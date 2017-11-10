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

        }
        public void LateUpdate()
        {
            if (agent == null)
                return;
            transform.forward = agent.Velocity;           
            transform.localPosition = agent.Update_Agent(Time.deltaTime); 
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, transform.forward);
        }
#endif
    }


}
