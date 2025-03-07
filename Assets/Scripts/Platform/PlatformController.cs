using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Android.LowLevel;

public class PlatformController : MonoBehaviour
{
    private bool isFlipping = false;
    public GameObject platformObject;
    public GameObject floor;
    private Quaternion originalOrientation;
    public MainMenuController mainMenuController;
    private bool hasReset = false;

    public void StartFlip()
    {
        if (!isFlipping)
        {
            Debug.Log("Flipping platform");
            isFlipping = true;
            StartCoroutine(FlipPlatform(80f));
        }
    }

    public void resetPlatform(bool enableReset)
    {
        if(!enableReset) return;
        Quaternion currentRotation = platformObject.transform.rotation;
        Quaternion newRotation = Quaternion.Euler(currentRotation.eulerAngles.x + 180f, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);
        platformObject.transform.rotation = newRotation;
        floor.GetComponent<MeshCollider>().enabled = true;
        hasReset = true;
    }

    private IEnumerator FlipPlatform(float speed)
    {   
        float totalRotation = 0f;
        floor.GetComponent<MeshCollider>().enabled = false; // remove floor so player can "fall" into water
        while (totalRotation < 180f)
        {
            Debug.Log("Flipping platform3");
            float step = speed * Time.deltaTime; // speed
            platformObject.transform.Rotate(Vector3.up, step);
            totalRotation += step;
            yield return null;
        }
        Debug.Log("Flipping platform4");
        // platformObject.transform.rotation = Quaternion.Euler(0f, 0f, 360f); // fit to exactly 180 degrees
        isFlipping = false;
    }
}
