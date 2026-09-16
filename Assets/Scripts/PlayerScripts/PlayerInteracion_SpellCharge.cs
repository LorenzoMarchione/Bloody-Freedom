using UnityEngine;
using UnityEngine.InputSystem;

public class SpellCharge : MonoBehaviour
{
    [Header("Spell settings")]
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform upperChest;    // posicion donde sale el proyectil
    [SerializeField] private float maxDistance = 10f;
    [SerializeField] private LayerMask ignoreLayers;

    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

        if (lineRenderer != null)
        {
            lineRenderer.enabled = false;
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        // 1. MANTENER "Q": Dibujar la trayectoria hacia adelante desde el pecho
        if (Keyboard.current.qKey.isPressed)
        {
            UpdateTrajectoryLine();
        }

        // 2. SOLTAR "Q": Disparar
        if (Keyboard.current.qKey.wasReleasedThisFrame)
        {
            CastSpell();
        }
    }

    private void UpdateTrajectoryLine()
    {
        lineRenderer.enabled = true;

        Vector3 origin = upperChest.position;
        Vector3 targetPoint = GetForwardTargetPoint(origin);

        // Dibujar la línea desde el pecho hacia el frente
        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, targetPoint);
    }

    private void CastSpell()
    {
        lineRenderer.enabled = false;

        Vector3 origin = upperChest.position;
        Vector3 direction = upperChest.forward; //punto hacia donde apunta el personaje

        //Instancia desde el punto de spawn hacia su frente
        Instantiate(spellPrefab, origin, Quaternion.LookRotation(direction));
    }

    private Vector3 GetForwardTargetPoint(Vector3 origin)
    {
        //Raycast desde el pecho hacia la direccion en la que esta orientado (upperChest.forward)
        Ray chestRay = new Ray(origin, upperChest.forward);
        int layerMask = ~ignoreLayers;

        if (Physics.Raycast(chestRay, out RaycastHit hit, maxDistance, layerMask))
        {
            // retorna el punto de impacto contra un objeto/pared
            return hit.point;
        }
        else
        {
            //si no choca con nada, retorna el punto maximo al frente
            return origin + (upperChest.forward * maxDistance);
        }
    }
}