using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PuzzlePiece : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable == null)
            grabInteractable = gameObject.AddComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnPieceSelected);
    }

    void OnPieceSelected(SelectEnterEventArgs args)
    {
        // play snap sound only when selected by a socket
        if (args.interactorObject is XRSocketInteractor)
        {
            PuzzleCompletion completion = FindObjectOfType<PuzzleCompletion>();
            if (completion != null)
                completion.PlaySnapSound();
        }
    }
}