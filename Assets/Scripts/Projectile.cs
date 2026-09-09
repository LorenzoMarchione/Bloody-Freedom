using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Vector3 direction;
    private float speed;

    public void Initialize(Vector3 direction, float speed)
    {
        this.direction = direction;
        this.speed = speed;
    }
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
