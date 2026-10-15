using System.Collections;
using UnityEngine;

public class PuzzleCompletion : MonoBehaviour
{
    [Header("All 9 sockets")]
    public MatchingScript[] sockets;

    [Header("Door to open")]
    public GameObject door;

    [Header("Light")]
    public Light puzzleLight;

    [Header("Audio")]
    public AudioClip successSound;
    public AudioClip snapSound;
    public AudioClip doorSound;

    private AudioSource audioSource;
    private bool isSolved = false;

    void Awake()
    {
        if (puzzleLight != null)
            puzzleLight.color = Color.white;    // set white early
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!isSolved)
            CheckCompletion();
    }

    public void PlaySnapSound()
    {
        if (snapSound != null)
            audioSource.PlayOneShot(snapSound);
    }

    void CheckCompletion()
    {
        foreach (MatchingScript socket in sockets)
        {
            if (!socket.hasSelection)
                return;
        }
        StartCoroutine(PuzzleSolved());
    }

    IEnumerator PuzzleSolved()
    {
        isSolved = true;
        Debug.Log("Puzzle Complete!");

        if (puzzleLight != null)
            puzzleLight.color = Color.green;

        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        yield return new WaitForSeconds(2f);

        if (doorSound != null)
            audioSource.PlayOneShot(doorSound);

        if (door != null)
            door.GetComponent<DoorAnimator>().Open();

        yield return new WaitForSeconds(5f);

        if (puzzleLight != null)
            puzzleLight.color = Color.white;
    }
}