using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public int buttonID;

    private AudioSource sound;
    private bool isPressed = false;
    private XRSimpleInteractable interactable;

    void Start()
    {
        sound = GetComponent<AudioSource>();

        interactable = GetComponent<XRSimpleInteractable>();
        if (interactable == null)
            interactable = gameObject.AddComponent<XRSimpleInteractable>();

        interactable.selectEntered.AddListener(_ => OnButtonPressed());
        interactable.selectExited.AddListener(_ => OnButtonReleased());
    }

    void OnButtonPressed()
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0, 0.743f, -1.007f);  // pushes down
            if (sound != null) sound.Play();
            isPressed = true;
            DoorCodeManager.instance.ButtonPressed(buttonID);
        }
    }

    void OnButtonReleased()
    {
        // button stays down until reset
    }

    public void ResetButton()
    {
        button.transform.localPosition = new Vector3(0, 0.753f, -1.007f);  // pops back up
        isPressed = false;
    }
}