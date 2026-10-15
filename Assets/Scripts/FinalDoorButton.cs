using System.Collections;
using UnityEngine;

public class FinalDoorButton : MonoBehaviour
{
    public GameObject door;
    public AudioClip buttonClickSound;
    public AudioClip doorSound;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor()
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);

        if (door != null)
        {
            if (doorSound != null)
                audioSource.PlayOneShot(doorSound);
            door.GetComponent<DoorAnimator>().Open();
        }
    }
}