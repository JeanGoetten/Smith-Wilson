using UnityEngine;

public class BossController : MonoBehaviour
{
    public float jumpInterval = 2f; // Intervalo de tempo entre os pulos
    public float jumpHeight = 2f; // Altura do pulo
    public float impactRadius = 1f; // Raio da onda de impacto
    public int impactDamage = 10; // Dano causado pela onda de impacto

    private float nextJumpTime = 0f;

    private void Update()
    {
        if (Time.time >= nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }
    }

    private void Jump()
    {
        Vector3 jumpDirection = GetRandomDirection();
        Vector3 jumpTarget = transform.position + jumpDirection;

        // Faz o boss pular para a nova posição
        StartCoroutine(JumpToPosition(jumpTarget));

        // Gera a onda de impacto
        GenerateImpactWave();
    }

    private IEnumerator JumpToPosition(Vector3 targetPosition)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < jumpInterval)
        {
            float t = elapsedTime / jumpInterval;
            transform.position = Vector3.Lerp(startPosition, targetPosition, t) + Vector3.up * jumpHeight * Mathf.Sin(t * Mathf.PI);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
    }

    private Vector3 GetRandomDirection()
    {
        // Define as direções possíveis (frente, trás, esquerda, direita)
        Vector3[] directions = { Vector3.forward, Vector3.back, Vector3.left, Vector3.right };
        int randomIndex = Random.Range(0, directions.Length);
        return directions[randomIndex];
    }

    private void GenerateImpactWave()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, impactRadius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                // Aplique dano ao jogador
                PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(impactDamage);
                }
            }
        }

        // Aqui você pode adicionar um efeito visual para a onda de impacto
        // Exemplo: Instantiate(impactEffectPrefab, transform.position, Quaternion.identity);
    }

    private void OnDrawGizmos()
    {
        // Desenha o raio da onda de impacto na cena para depuração
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }
}
