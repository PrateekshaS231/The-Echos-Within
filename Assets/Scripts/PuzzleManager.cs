using System.Collections;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public static PuzzleManager Instance;

    [Header("Spheres in scene (assign in Inspector)")]
    public ColorCycle[] spheres;

    [Header("Target colors matching the pictures (in order)")]
    public Color[] targetColors = new Color[]
    {
        Color.green,
        new Color(0.5f, 0f, 0.5f),
        Color.yellow
    };

    [Header("The door to open")]
    public GameObject door;

    [Header("Light")]
    public Light puzzleLight;

    [Header("Audio")]
    public AudioClip doorOpenSound;
    public AudioClip buttonClickSound;
    public AudioClip successSound;
    public AudioClip wrongSound;        // added this!

    private AudioSource audioSource;
    private bool isSolved = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (puzzleLight != null)
            puzzleLight.color = Color.white;
    }

    public void PlayClickSound()
    {
        if (buttonClickSound != null)
            audioSource.PlayOneShot(buttonClickSound);
    }

    public void CheckSolution()
    {
        if (isSolved) return;

        for (int i = 0; i < spheres.Length; i++)
        {
            Debug.Log($"Sphere {i}: {spheres[i].GetCurrentColor()} | Target: {targetColors[i]}");
            if (!ColorsMatch(spheres[i].GetCurrentColor(), targetColors[i]))
            {
                if (AllSpheresChosen())
                    StartCoroutine(FlashRed());
                return;
            }
        }
        OpenDoor();
    }

    bool AllSpheresChosen()
    {
        foreach (ColorCycle sphere in spheres)
        {
            if (sphere.currentIndex == 0)
                return false;
        }
        return true;
    }

    bool ColorsMatch(Color a, Color b, float tolerance = 0.1f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }

    IEnumerator FlashRed()
    {
        if (isSolved) yield break;
        if (puzzleLight != null)
            puzzleLight.color = Color.red;
        if (wrongSound != null)                 // plays wrong sound!
            audioSource.PlayOneShot(wrongSound);
        yield return new WaitForSeconds(0.5f);
        if (isSolved) yield break;
        if (puzzleLight != null)
            puzzleLight.color = Color.white;
    }

    void OpenDoor()
    {
        isSolved = true;
        Debug.Log("Puzzle solved! Opening door.");
        StartCoroutine(OpenDoorSequence());
    }

    IEnumerator OpenDoorSequence()
    {
        if (puzzleLight != null)
            puzzleLight.color = Color.green;

        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        yield return new WaitForSeconds(2f);

        if (doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);

        if (door != null)
            door.GetComponent<DoorAnimator>().Open();

        yield return new WaitForSeconds(5f);

        if (puzzleLight != null)
            puzzleLight.color = Color.white;
    }
}