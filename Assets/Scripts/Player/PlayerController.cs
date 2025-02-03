using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed = 5f; // speed

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

        Vector3 moveDirection = new Vector3(moveX, 0f, moveZ).normalized;

        // walk
        rb.linearVelocity = moveDirection * moveSpeed;

        // turn
        if (moveDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }
    }
}