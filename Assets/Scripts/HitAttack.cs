using UnityEngine;

public class HitAttack : MonoBehaviour
{
    public ParticleSystem hitEffect; 
    public AudioClip hitSound; 
    private AudioSource audioSource;

    public string targetTag;

    private BossController boss; 

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        boss = FindObjectOfType<BossController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z), Quaternion.identity);
            }

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
        if (other.CompareTag("Boss"))
        {
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }

            if (hitEffect != null)
            {
                Instantiate(hitEffect, new Vector3(other.transform.position.x, other.transform.position.y + 1f, other.transform.position.z), Quaternion.identity);
            }

            if (boss != null)
            {
                boss.Hitted();
            }
        }
    }
}
