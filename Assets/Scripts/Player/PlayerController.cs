using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f; // speed
    public Vector3 moveDirection;

    public Raycaster raycaster;
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

        Vector3 start = transform.position + transform.forward * 1.5f;
        Vector3 direction = (Vector3.down + transform.forward * 0.3f).normalized;
        raycaster.CastRay(start, direction);
    }
}