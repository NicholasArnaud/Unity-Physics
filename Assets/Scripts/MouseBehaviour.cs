using HookesLaw;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class MouseBehaviour : MonoBehaviour
{
    public Vector3 mousePos, target;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Input.mousePosition;
        transform.position = mousePos;
        target = Camera.main.ScreenToViewportPoint(new Vector3(mousePos.x, mousePos.y, 0));
        transform.position = target;

        if (Input.GetMouseButton(0))
        {

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (!Physics.Raycast(ray, out hit))
                return;
            GameObject selected = hit.collider.gameObject;
            selected.GetComponent<Renderer>().material.color = Color.green;

            selected.transform.position = ray.GetPoint(Vector3.Distance(selected.transform.position, ray.origin));

        }
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mousePos);
            if (!Physics.Raycast(ray, out hit))
                return;
            GameObject selected = hit.collider.gameObject;
            selected.GetComponent<Renderer>().material.color = Color.green;

            if (!selected.GetComponent<ParticleBehaviour>().particle.Locked)
                selected.GetComponent<ParticleBehaviour>().particle.Locked = true;
            else
                selected.GetComponent<ParticleBehaviour>().particle.Locked = false;
        }
    }
}
