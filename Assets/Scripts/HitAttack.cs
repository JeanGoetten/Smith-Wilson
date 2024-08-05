using UnityEngine;

public class HitAttack : MonoBehaviour
{
    public ParticleSystem hitEffect; // Efeito visual
    public AudioClip hitSound; // Som do impacto
    private AudioSource audioSource;

    public string targetTag; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
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

            // Destroi o objeto alvo
            Destroy(other.gameObject);

            if (targetTag == "Player")
            {
                ActionSceneManager.neoIsAlive = false;
            }
            if (targetTag == "Enemies")
            {
                ActionSceneManager.smithSafe = false;
            }
        }
    }
}
