using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apuntado : MonoBehaviour
{
    [SerializeField] private float maxRange = 200f; // Aumenta el rango de detección del enemigo
    [SerializeField] private float rotationSpeed = 30f; // Reduzca la velocidad de rotación

    [SerializeField] private Transform closestEnemy;
    [SerializeField] private bool aimingEnabled = false; // Variable para controlar si el apuntado está activo

    void Update()
    {
        // Verificar si se presionó la tecla E para activar o desactivar el apuntado
        if (Input.GetKeyDown(KeyCode.E))
        {
            aimingEnabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            aimingEnabled = false;
        }

        // Solo buscar y apuntar al enemigo si el apuntado está activo
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

            // Calcular la rotación sin cambiar el ángulo en los ejes X y Z
            Quaternion lookRotation = Quaternion.LookRotation(directionToEnemy);
            lookRotation.x = 0f;
            lookRotation.z = 0f;

            // Reduce la velocidad de rotación usando Slerp para una rotación suave
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRange);
    }
}
