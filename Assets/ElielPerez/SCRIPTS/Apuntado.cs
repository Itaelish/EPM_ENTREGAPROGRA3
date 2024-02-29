using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntado : MonoBehaviour
{
    [SerializeField] private float maxRange = 200f; // Aumenta el rango de detecci�n del enemigo
    [SerializeField] private float rotationSpeed = 30f; // Reduzca la velocidad de rotaci�n

    [SerializeField] private Transform closestEnemy;
    [SerializeField] private bool aimingEnabled = false; // Variable para controlar si el apuntado est� activo

    void Update()
    {
        // Verificar si se presion� la tecla E para activar o desactivar el apuntado
        if (Input.GetKeyDown(KeyCode.E))
        {
            aimingEnabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            aimingEnabled = false;
        }

        // Solo buscar y apuntar al enemigo si el apuntado est� activo
        if (aimingEnabled)
        {
            FindClosestEnemy();
            AimAtClosestEnemy();
        }
    }

    void FindClosestEnemy()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, maxRange);

        closestEnemy = null;
        float closestDistSqr = Mathf.Infinity;

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                float distSqrToTarget = (collider.transform.position - transform.position).sqrMagnitude;

                if (distSqrToTarget < closestDistSqr)
                {
                    closestDistSqr = distSqrToTarget;
                    closestEnemy = collider.transform;
                }
            }
        }
    }

    void AimAtClosestEnemy()
    {
        if (closestEnemy != null)
        {
            Vector3 directionToEnemy = closestEnemy.position - transform.position;

            // Calcular la rotaci�n sin cambiar el �ngulo en los ejes X y Z
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            lookRotation.x = 0f;
            lookRotation.z = 0f;

            // Reduce la velocidad de rotaci�n usando Slerp para una rotaci�n suave
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
