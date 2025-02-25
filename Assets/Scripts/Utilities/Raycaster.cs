using Unity.VisualScripting;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public RaycastHit ray;
    public LayerMask layerMask;
    public GameObject lookObj;

    public GameObject CastRay(Vector3 start, Vector3 direction) // chnage to game obj return??
    {
        Debug.Log("Casting Ray: "+ start + " to " + direction);
        if(Physics.Raycast(start, direction, out RaycastHit ray, 100f, layerMask))
        {
            Debug.Log("Hit: " + ray.transform.gameObject.name);
            Debug.DrawRay(start, direction * ray.distance, Color.red);
            lookObj = ray.transform.gameObject;
            return ray.transform.gameObject;
        }
        else
        {
            Debug.Log("No hit");
            Debug.DrawRay(start, direction * 100f, Color.green);
            ray = new RaycastHit();
            lookObj = null;
            // no object hit
            return null;
        }
    }

    // object in view of player to be outlined
    public GameObject getLookObject()
    {
        return lookObj;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 50f);
    }
}