using UnityEngine;

public class FruitBox : MonoBehaviour
{
    [Header("Correct Fruit")]
    public string correctFruitName;

    [Header("Light")]
    public Light spotLight;

    [Header("Puzzle Manager")]
    public FruitPlacementManager fruitPlacementManager;

    [Header("Sounds")]
    public AudioSource audioSource;
    public AudioClip successSound;
    public AudioClip failureSound;

    private bool isCorrect = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == correctFruitName)
        {
            isCorrect = true;
            spotLight.color = Color.green;

            if (audioSource != null && successSound != null)
                audioSource.PlayOneShot(successSound);
        }
        else
        {
            isCorrect = false;
            spotLight.color = Color.red;

            if (audioSource != null && failureSound != null)
                audioSource.PlayOneShot(failureSound);
        }

        fruitPlacementManager.CheckPuzzle();
    }

    private void OnTriggerExit(Collider other)
    {
        isCorrect = false;
        spotLight.color = Color.white;

        fruitPlacementManager.CheckPuzzle();
    }

    public bool IsCorrect()
    {
        return isCorrect;
    }
}