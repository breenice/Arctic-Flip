using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public float flipSpeed = 2f;
    private bool isFlipping = false;
    public GameObject platformObject;

    void Update()
    {
        if (isFlipping)
        {
            FlipPlatform();
        }
    }

    public void StartFlipping()
    {
        isFlipping = true;
    }

    private void FlipPlatform()
    {
        Quaternion targetRotation = Quaternion.Euler(180f, 0f, 0f); // Flips upside down
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * flipSpeed);

        // Stop flipping once close to target
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            transform.rotation = targetRotation;
            isFlipping = false;
        }
    }
}
