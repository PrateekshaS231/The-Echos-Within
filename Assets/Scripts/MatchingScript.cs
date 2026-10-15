using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MatchingScript : XRSocketInteractor
{
    public string socketName;

    protected override void Start()
    {
        base.Start();
        socketName = this.gameObject.name;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        return base.CanSelect(interactable) && socketName == interactable.transform.name;
    }

    public override bool CanHover(IXRHoverInteractable interactable)
    {
        return base.CanHover(interactable) && socketName == interactable.transform.name;
    }
}