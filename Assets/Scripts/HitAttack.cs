using UnityEngine;

public class HitAttack : MonoBehaviour
{
    public ParticleSystem hitEffect; // Efeito visual
    public AudioClip hitSound; // Som do impacto
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemies"))
        {
            // Reproduz o som do impacto
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            // Reproduz o efeito visual
            if (hitEffect != null)
            {
                Instantiate(hitEffect, new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z), Quaternion.identity);
            }

            // Destroi o objeto com a tag "Enemies"
            Destroy(other.gameObject);
        }
    }
}
