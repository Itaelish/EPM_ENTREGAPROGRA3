using UnityEngine;
using System.Collections.Generic;

public class Shoot : MonoBehaviour
{
    [SerializeField] private bool isReloading = false;

    public GameObject projectilePrefab;
    public Transform firePoint;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f;
    public int maxProjectiles = 10;
    public float reloadTime = 3f;

    private float nextFireTime;
    private float nextReloadTime;
    public Queue<GameObject> projectileQueue = new Queue<GameObject>();
    private int currentProjectiles;

    void Start()
    {
        // Llenar la pool de proyectiles al inicio
        for (int i = 0; i < maxProjectiles; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectileQueue.Enqueue(projectile);
        }

        currentProjectiles = maxProjectiles;
    }

    void Update()
    {
        // Verificar si se puede disparar
        if (Time.time >= nextReloadTime && Input.GetButtonDown("Fire1"))
        {
            if (currentProjectiles > 0 && Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + fireRate;
                currentProjectiles--;
            }
            else if (currentProjectiles == 0)
            {
                nextReloadTime = Time.time + reloadTime;
                currentProjectiles = maxProjectiles;
            }
        }
    }

    void ShootProjectile()
    {
        // Tomar un proyectil inactivo de la cola
        GameObject projectile = projectileQueue.Dequeue();

        // Disparar si se encontró un proyectil
        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;
            projectile.SetActive(true);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * projectileSpeed;
            }
        }
    }

    // Método para devolver el proyectil disparado a la cola
    void ReturnProjectileToQueue(GameObject projectile)
    {
        projectile.SetActive(false);
        projectileQueue.Enqueue(projectile);
    }
}
