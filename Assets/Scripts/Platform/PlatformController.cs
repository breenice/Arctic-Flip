using System.Collections;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    private bool isFlipping = false;
    public GameObject platformObject;
    public GameObject floor;

    public void StartFlip()
    {
        if (!isFlipping)
        {
            Debug.Log("Flipping platform");
            isFlipping = true;
            StartCoroutine(FlipPlatform());
        }
    }

    private IEnumerator FlipPlatform()
    {   
        float totalRotation = 0f;

        floor.GetComponent<MeshCollider>().enabled = false; // remove floor so player can "fall" into water
        while (totalRotation < 180f)
        {
            Debug.Log("Flipping platform3");
            float step = 80f * Time.deltaTime; // speed
            platformObject.transform.Rotate(Vector3.up, step);
            totalRotation += step;
            yield return null;
        }
        Debug.Log("Flipping platform4");
        // platformObject.transform.rotation = Quaternion.Euler(0f, 0f, 360f); // fit to exactly 180 degrees
        isFlipping = false;
    }
}
