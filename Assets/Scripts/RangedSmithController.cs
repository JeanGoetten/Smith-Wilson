using UnityEngine;

public class RangedSmithController : MonoBehaviour
{
    public Transform gunBarrel; 
    public GameObject projectilePrefab; 
    public float projectileSpeed = 10f; 
    public float fireRate = 1f; 
    private float nextFireTime = 0f; 

    public Transform target; 

    public float rotationSpeed = 2f; 
    public Vector2 rotationRange = new Vector2(-45f, 45f); 

    private void Update()
    {
        HandleRotation();
        HandleShooting();
    }

    private void HandleRotation()
    {
        float angle = Mathf.PingPong(Time.time * rotationSpeed, rotationRange.y - rotationRange.x) + rotationRange.x;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }

    private void HandleShooting()
    {
        if (Time.time >= nextFireTime)
        {
            if (target != null)
            {
                Vector3 direction = -gunBarrel.right;
                RaycastHit hit;
                if (Physics.Raycast(gunBarrel.position, direction, out hit))
                {
                    Debug.DrawRay(gunBarrel.position, direction * 100f, Color.red);

                    //Debug.Log($"Raycast hit: {hit.collider.name} em {hit.point}, Surface normal: {hit.normal}");

                    if (hit.collider.CompareTag("Player"))
                    {
                        ShootProjectile(direction);
                        nextFireTime = Time.time + 1f / fireRate;
                    }
                }
                else
                {
                    Debug.DrawRay(gunBarrel.position, direction * 100f, Color.green);
                }
            }
        }
    }

    private void ShootProjectile(Vector3 direction)
    {
        if (projectilePrefab == null || gunBarrel == null)
        {
            Debug.LogError("Prefab de projétil ou gunBarrel não assinados");
            return;
        }

        Vector3 spawnPosition = gunBarrel.position;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        if (projectile != null)
        {
            ProjectileController projectileController = projectile.GetComponent<ProjectileController>();
            if (projectileController != null)
            {
                projectileController.Initialize(direction, projectileSpeed);
            }
            else
            {
                Debug.LogError("Projétil instaciado não contem um ProjectileController");
            }
        }
        else
        {
            Debug.LogError("Falha ao instanciar projétil.");
        }
    }
}
