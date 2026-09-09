using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Melee attack config")]
    [SerializeField] private float damage = 25f; //daño de la espada, puede cambiar a futuro
    [SerializeField] private float attackRange = 1.5f; // distancia del jugador
    [SerializeField] private float attackHeightOffset = 1.0f;
    [SerializeField] private Vector3 attackBoxSize = new Vector3(1f, 3f, 1f); // tamaño de la hitbox
    [SerializeField] private LayerMask enemyLayer; // filtro de colisiones (solo detectar colisiones con enemigos)

    [Header("Visualización (Prefab)")]
    [SerializeField] private GameObject hitbox; // prefab de la hitbox
    [SerializeField] private float effectDuration = 0.2f; // tiempo de duracion de la hitbox

    //metodo que llama "Send Messages" del personaje por defecto de unity
    private void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            MeleeAttack();
        }
    }

    private void MeleeAttack()
    {
        // 1. calcular la posicion frente al jugador
        Vector3 attackPosition = transform.position + (transform.forward * attackRange) + (Vector3.up * attackHeightOffset);

        // 2. deteccion fisica en area (OverlapBox)
        Collider[] hitColliders = Physics.OverlapBox(attackPosition, attackBoxSize / 2, transform.rotation, enemyLayer);

        // 3. imprimir objetos colisionados
        if (hitColliders.Length > 0)
        {
            Debug.Log("<color=red>HIT</color> with object: " + hitColliders.Length); //golpeó un objeto

            foreach (Collider hit in hitColliders)
            {
                //logica de hacerle daño a los enemigos aquí (((((((((((((((((!!!!!!!!!!!!!!!))))))))))))))))))))))))))))
            }
        }
        else
        {
            Debug.Log("<color=yellow> didn't hit annything. </color>"); //no golpeo a nada
        }

        // 4. hacer aparecer la hitbox
        if (hitbox != null)
        {
            StartCoroutine(SpawnHitbox(attackPosition));
        }
    }

    private IEnumerator SpawnHitbox(Vector3 position)
    {
        // spawnear la hitbox con la posición y rotación actual del personaje
        GameObject efecto = Instantiate(hitbox, position, transform.rotation);

        yield return new WaitForSeconds(effectDuration);

        // borrar el clon
        Destroy(efecto);
    }
}