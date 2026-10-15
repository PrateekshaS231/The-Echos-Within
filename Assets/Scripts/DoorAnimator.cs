using UnityEngine;

public class DoorAnimator : MonoBehaviour
{
    public Vector3 openOffset = new Vector3(0, 0, 3f); // Slides left
    public float speed = 2f;
    private bool isOpening = false;
    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    public void Open()
    {
        isOpening = true;
        targetPos = transform.position + openOffset;
    }

    void Update()
    {
        if (isOpening)
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }
}