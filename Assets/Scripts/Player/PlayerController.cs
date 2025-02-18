using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f; // speed
    public Vector3 moveDirection;
    private Ray playerRay;
    public LayerMask layerMask;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = -1f;

        moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // walk
        rb.linearVelocity = moveDirection * moveSpeed;

        // turn
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
        castRay();
    }

    void castRay()
    {
        Vector3 inFront = transform.forward ; // look a few "steps" in front of penguin
        playerRay = new Ray(transform.position + inFront, (inFront + Vector3.down * 2f).normalized); // look at the ground
        Debug.DrawRay(playerRay.origin, playerRay.direction * 10, Color.red);
    }

    RaycastHit? getRayHit()
    {
        if(Physics.Raycast(playerRay, out RaycastHit hit, 20f, layerMask))
        {
            Debug.Log(hit.transform.name);
            return hit;
        }
        return null; // no object hit
    }
}