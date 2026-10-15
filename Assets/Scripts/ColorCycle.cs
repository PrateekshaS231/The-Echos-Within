using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    public Color[] colorOptions = new Color[]
    {
        Color.red,
        Color.green,
        new Color(0.5f, 0f, 0.5f),
        Color.blue,
        Color.yellow
    };

    public int currentIndex = 0;    // changed to public so PuzzleManager can check it
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = new Material(rend.material);
        rend.material.color = colorOptions[currentIndex];

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        if (interactable == null)
            interactable = gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        interactable.selectEntered.AddListener(_ => CycleColor());
    }

    public void CycleColor()
    {
        currentIndex = (currentIndex + 1) % colorOptions.Length;
        rend.material.color = colorOptions[currentIndex];
        Debug.Log(gameObject.name + " changed to: " + colorOptions[currentIndex]);
        PuzzleManager.Instance.PlayClickSound();    // plays click sound
        PuzzleManager.Instance.CheckSolution();
    }

    public Color GetCurrentColor()
    {
        return rend.material.color;
    }
}