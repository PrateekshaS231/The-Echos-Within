using System.Collections;
using UnityEngine;
using TMPro; 

public class DoorCodeManager : MonoBehaviour
{
    public static DoorCodeManager instance;

    public int[] correctCode = { 0, 2, 4, 1, 3 };
    public GameObject door;
    public Light doorLight;
    public AudioClip successSound;
    public AudioClip wrongSound;
    public AudioClip doorSound;
    public float doorSlideDistance = 3f;
    public float doorSlideSpeed = 2f;
    public TextMeshProUGUI orderText;

    private int currentStep = 0;
    private AudioSource audioSource;
    private ButtonVR[] allButtons;
    private string pressedOrder = "";
    private bool isSolved = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        doorLight.color = Color.white;
        allButtons = FindObjectsOfType<ButtonVR>();
        Debug.Log("Found buttons: " + allButtons.Length);
        UpdateOrderText();
    }

    public void ButtonPressed(int buttonID)
    {
        if (isSolved) return;

        Debug.Log("Button pressed: " + buttonID + " Step: " + currentStep);

        pressedOrder += buttonID + " ";
        UpdateOrderText();

        if (buttonID == correctCode[currentStep])
        {
            currentStep++;

            if (currentStep == correctCode.Length)
            {
                OpenDoor();
            }
        }
        else
        {
            currentStep = 0;
            if (wrongSound != null)
                audioSource.PlayOneShot(wrongSound);
            StartCoroutine(FlashRed());
            ResetAllButtons();
            pressedOrder = "";
            UpdateOrderText();
        }
    }

    void UpdateOrderText()
    {
        if (orderText != null)
            orderText.text = "Order: " + pressedOrder;
    }

    void ResetAllButtons()
    {
        foreach (ButtonVR btn in allButtons)
        {
            btn.ResetButton();
        }
    }

    IEnumerator FlashRed()
    {
        if (isSolved) yield break;
        doorLight.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        if (isSolved) yield break;
        doorLight.color = Color.white;
    }

    void OpenDoor()
    {
        isSolved = true;
        Debug.Log("Door Opening!");
        Debug.Log("Light color changing to green!");
        StartCoroutine(OpenDoorSequence());
    }

    IEnumerator OpenDoorSequence()
    {
        doorLight.color = Color.green;      // turns green

        if (successSound != null)
            audioSource.PlayOneShot(successSound);

        yield return new WaitForSeconds(2f);    // short wait before door slides

        if (doorSound != null)
            audioSource.PlayOneShot(doorSound);

        // slide door open
        Vector3 startPosition = door.transform.position;
        Vector3 targetPosition = startPosition + new Vector3(doorSlideDistance, 0, 0);

        while (Vector3.Distance(door.transform.position, targetPosition) > 0.01f)
        {
            door.transform.position = Vector3.MoveTowards(
                door.transform.position,
                targetPosition,
                doorSlideSpeed * Time.deltaTime
            );
            yield return null;
        }

        door.transform.position = targetPosition;

        yield return new WaitForSeconds(5f);    // green stays for 5 seconds after door opens

        doorLight.color = Color.white;          // turns off after 5 seconds
    }

    public void ResetAll()
    {
        if (isSolved) return;
        currentStep = 0;
        doorLight.color = Color.white;
        allButtons = FindObjectsOfType<ButtonVR>();
        Debug.Log("Resetting " + allButtons.Length + " buttons");
        ResetAllButtons();
        pressedOrder = "";
        UpdateOrderText();
    }
}