using System.Xml.Serialization;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private int damage;
    [SerializeField] private LayerMask targetLayers;

    public void Initialize(Vector3 direction, float speed, int damage)
    {
        this.direction = direction;
        this.speed = speed;
        this.damage = damage;
    }
    private void Update() => transform.position += direction * speed * Time.deltaTime;
    private void OnTriggerEnter(Collider other)
    {
        if ((targetLayers.value & (1 << other.gameObject.layer)) == 0)
            return;
        if (other.TryGetComponent<Health>(out Health hp))
        {
            hp.ChangeHealth(-damage);
            Destroy(gameObject);
        }
    }
}
