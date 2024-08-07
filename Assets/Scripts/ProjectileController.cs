using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public float speed = 10f; // Velocidade do proj�til
    public GameObject impactEffect; // Efeito de impacto (sistema de part�culas)

    private Vector3 direction;

    // Inicializa o proj�til com uma dire��o e velocidade
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

        // Ajusta a posi��o inicial do proj�til para evitar o problema do eixo Y
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Instancia o efeito de impacto na posi��o da colis�o
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, transform.rotation);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            ActionSceneManager.neoIsAlive = false;
            Destroy(collision.gameObject); // Destr�i o jogador se atingido
        }
        if (collision.gameObject.CompareTag("Enemies"))
        {
            ActionSceneManager.smithSafe = false;
            Destroy(collision.gameObject); // Destr�i o inimigo se atingido
        }

        // Destr�i o proj�til ap�s o impacto
        Destroy(gameObject);
    }
}
