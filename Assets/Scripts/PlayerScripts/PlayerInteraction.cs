using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Melee attack config")]
    [SerializeField] private int damage = 25; // daño de la espada
    [SerializeField] private float attackRange = 1.5f; // distancia del jugador
    [SerializeField] private Vector3 attackBoxSize = new Vector3(1f, 3f, 1f); // tamaño de la hitbox
    [SerializeField] private LayerMask enemyLayer; // filtro de colisiones

    [Header("Visualización (Prefab)")]
    [SerializeField] private GameObject hitboxPrefab; // prefab original de la hitbox
    [SerializeField] private float effectDuration = 0.2f; // tiempo de duración de la hitbox

    private GameObject pooledHitbox; // Instancia única que vamos a reutilizar

    private void Start()
    {
        // 1. Instanciamos la hitbox una sola vez al inicio del juego y la dejamos desactivada
        if (hitboxPrefab != null)
        {
            pooledHitbox = Instantiate(hitboxPrefab);
            pooledHitbox.SetActive(false);
        }
    }

    private void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            AttackEvent();
        }
    }

    public void AttackEvent()
    {
        // 2. Calcular la posición frente al jugador
        Vector3 attackPosition = transform.position + (transform.forward * attackRange);

        // 3. Detección física en área (OverlapBox)
        Collider[] hitColliders = Physics.OverlapBox(attackPosition, attackBoxSize / 2, transform.rotation, enemyLayer);

        // 4. Imprimir/aplicar daño a objetos colisionados
        if (hitColliders.Length > 0)
        {
            foreach (Collider hit in hitColliders)
            {
                if (hit.TryGetComponent<Health>(out Health hp))
                {
                    hp.ChangeHealth(-damage);
                    break;
                }
            }
        }

        // 5. Activar y posicionar la hitbox en lugar de instanciar una nueva
        if (pooledHitbox != null)
        {
            // Detenemos corrutinas anteriores por si el jugador ataca muy rápido consecutivamente
            StopAllCoroutines();
            StartCoroutine(ToggleHitbox(attackPosition, transform.rotation));
        }
    }

    private IEnumerator ToggleHitbox(Vector3 position, Quaternion rotation)
    {
        // Colocamos el objeto en la posición y rotación correctas y lo encendemos
        pooledHitbox.transform.position = position;
        pooledHitbox.transform.rotation = rotation;
        pooledHitbox.SetActive(true);

        // Esperamos el tiempo que dura el efecto visual
        yield return new WaitForSeconds(effectDuration);

        // En lugar de destruir, simplemente lo desactivamos
        pooledHitbox.SetActive(false);
    }
}