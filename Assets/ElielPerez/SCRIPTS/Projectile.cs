using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Queue<GameObject> projectileQueue;

    private void Start()
    {
        // Obtener la referencia a la cola de proyectiles del script de Disparar
        projectileQueue = FindObjectOfType<Shoot>().projectileQueue;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Desactivar el proyectil y devolverlo a la cola
        gameObject.SetActive(false);
        projectileQueue.Enqueue(gameObject);
    }
}
