using System.Collections;
using UnityEngine;
using TMPro;

public class KeypadManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI displayText;

    [Header("Correct Code")]
    public string correctCode = "475";

    [Header("Door")]
    public GameObject door;

    [Header("Light")]
    public Light keypadLight;

    [Header("Audio")]
    public AudioClip buttonClickSound;
    public AudioClip successSound;
    public AudioClip wrongSound;
    public AudioClip doorSound;

    private string currentInput = "";
    private AudioSource audioSource;
    private bool isSolved = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (keypadLight != null)
            keypadLight.color = Color.white;
        displayText.text = "0";
    }

    public void PressNumber(string number)
    {
        if (isSolved) return;

        if (currentInput.Length < 3)  // max 3 digits
        {
            currentInput += number;
            displayText.text = currentInput;
            if (buttonClickSound != null)
                audioSource.PlayOneShot(buttonClickSound);
        }
    }

    public void PressOpen()
    {
        if (isSolved) return;

        if (currentInput == correctCode)
        {
            StartCoroutine(OpenDoorSequence());
        }
        else
        {
            StartCoroutine(WrongCode());
        }
    }

    IEnumerator WrongCode()
    {
        if (keypadLight != null)
            keypadLight.color = Color.red;
        if (wrongSound != null)
            audioSource.PlayOneShot(wrongSound);
        displayText.text = "WRONG";

        yield return new WaitForSeconds(1f);

        if (keypadLight != null)
            keypadLight.color = Color.white;
        currentInput = "";
        displayText.text = "0";
    }

    IEnumerator OpenDoorSequence()
    {
        isSolved = true;

        if (keypadLight != null)
            keypadLight.color = Color.green;
        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        displayText.text = "OPEN";

        yield return new WaitForSeconds(2f);

        if (doorSound != null)
            audioSource.PlayOneShot(doorSound);
        if (door != null)
            door.GetComponent<DoorAnimator>().Open();

        yield return new WaitForSeconds(5f);

        if (keypadLight != null)
            keypadLight.color = Color.white;
    }

    public void PressClear()
    {
        if (isSolved) return;
        currentInput = "";
        displayText.text = "0";
    }
}