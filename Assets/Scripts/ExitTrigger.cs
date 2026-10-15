using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public ParticleSystem confetti;
    public GameObject congratsCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            confetti.Play();
            congratsCanvas.SetActive(true);
        }
    }
}