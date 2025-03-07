using System.Collections;
using Unity;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip point;
    [SerializeField] private AudioClip lvlup;
    [SerializeField] private AudioClip wrong;
    private bool canPlaySound = true;

    public void PlaySound(string action)
    {
        switch (action)
        {
            case "point":
                audioSource.PlayOneShot(point);
                break;
            case "lvlUp":
                audioSource.PlayOneShot(lvlup);
                break;
            case "wrong":
                audioSource.PlayOneShot(wrong);
                break;
        }
    }
}