using UnityEngine;

public class SingleSFXOnTrigger : MonoBehaviour
{
    public AudioClip alertClip;
    public float alertCooldown = 5f; // Tempo de cooldown ajustável para o som de alerta
    private AudioSource audioSource;
    private float nextAlertTime;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        nextAlertTime = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextAlertTime)
        {
            PlayAlertSound();
            nextAlertTime = Time.time + alertCooldown;
        }
    }

    private void PlayAlertSound()
    {
        audioSource.PlayOneShot(alertClip);
    }
}
