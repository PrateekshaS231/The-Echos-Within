using UnityEngine;

public class FruitPlacementManager : MonoBehaviour
{
    [Header("Boxes")]
    public FruitBox[] fruitBoxes;

    [Header("Main Light")]
    public Light mainLight;

    [Header("Door")]
    public DoorAnimator doorAnimator;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    private bool opened = false;

    public void CheckPuzzle()
    {
        bool allCorrect = true;

        foreach (FruitBox box in fruitBoxes)
        {
            if (!box.IsCorrect())
            {
                allCorrect = false;
                break;
            }
        }

        if (allCorrect)
        {
            mainLight.color = Color.green;

            if (!opened)
            {
                opened = true;

                if (audioSource != null && doorOpenSound != null)
                    audioSource.PlayOneShot(doorOpenSound);

                if (doorAnimator != null)
                    doorAnimator.Open();
            }
        }
        else
        {
            mainLight.color = Color.white;
        }
    }
}