using UnityEngine;

public class MagicSpell : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float speed = 30f;
    [SerializeField] private float lifeTime = 4f; // 4 segundos de tiempo de vida

    [Header("Daño")]
    [SerializeField] private int damage = 25;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Asignar velocidad constante hacia adelante
        rb.linearVelocity = transform.forward * speed;

        // tras 4 segundos, se destruye
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ignora al jugador
        if (other.CompareTag("Player")) return;

        // si es enemigo, le quita vida
        if (other.TryGetComponent<Health>(out Health hp))
        {
            hp.ChangeHealth(-damage);

            // luego de quitarle vida, se destruye
            Destroy(gameObject);
        }

        // si choca contra una pared u otro objeto sin la componente Health, no hace nada
    }
}