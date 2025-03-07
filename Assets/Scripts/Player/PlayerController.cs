using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float moveSpeed = 5f; // speed
    private Vector3 moveDirection;

    public Raycaster raycaster;
    public LayerMask layerMask;
    [SerializeField] private GameObject penguinModel; 
    private Vector3 ogPosition;

    [SerializeField] private GameObject wizardModel; 
    private Vector3 wizardogPosition;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ogPosition = penguinModel.transform.position;
        wizardogPosition = wizardModel.transform.position;
    }

    void Update()
    {
        float moveX = 0f;
        float moveY = 0f;
        float moveZ = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) moveZ = -1f;
        if (Input.GetKey(KeyCode.DownArrow)) moveZ = 1f;
        if (Input.GetKey(KeyCode.LeftArrow)) moveX = 1f;
        if (Input.GetKey(KeyCode.RightArrow)) moveX = -1f;
        
        // jump
        if (Input.GetKey(KeyCode.Space) && transform.position.y < 2f) moveY = 1f;


        moveDirection = new Vector3(moveX, moveY, moveZ).normalized;

        // walk
        rb.linearVelocity = moveDirection * moveSpeed;
        rb.AddForce(Vector3.down * 9.81f * 2f, ForceMode.Acceleration); // bigger gravity acceleration

        // turn
        if ((moveDirection != Vector3.zero) && (!Input.GetKey(KeyCode.Space)))
        {
            Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, Time.deltaTime * 10f);
        }

        Vector3 start = transform.position + transform.forward * 1.5f;
        Vector3 direction = (Vector3.down + transform.forward * 0.3f).normalized;
        raycaster.CastRay(start, direction);
    }
    public void ResetPosition(bool enableReset)
    {
        if (!enableReset) return;
        penguinModel.transform.position = ogPosition;
        wizardModel.transform.position = wizardogPosition;
    }
}