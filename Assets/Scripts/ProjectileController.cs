using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f; // Velocidade do projétil
    public GameObject impactEffect; // Efeito de impacto (sistema de partículas)

    private Vector3 direction;

    // Inicializa o projétil com uma direção e velocidade
    public void Initialize(Vector3 direction, float projectileSpeed)
    {
        this.direction = direction;
        this.speed = projectileSpeed;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            Debug.LogError("Rigidbody is not attached to the projectile.");
        }

        // Ajusta a posição inicial do projétil para evitar o problema do eixo Y
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Instancia o efeito de impacto na posição da colisão
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            ActionSceneManager.neoIsAlive = false;
            Destroy(collision.gameObject); // Destrói o jogador se atingido
        }
        if (collision.gameObject.CompareTag("Enemies"))
        {
            ActionSceneManager.smithSafe = false;
            Destroy(collision.gameObject); // Destrói o inimigo se atingido
        }

        // Destrói o projétil após o impacto
        Destroy(gameObject);
    }
}
