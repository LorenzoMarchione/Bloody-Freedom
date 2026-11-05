using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Health health;
    private Animator anim;

    private void Awake()
    {
        health = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        health.OnDamaged += PlayHurtAnimation;
        health.OnDeath += PlayDeathAnimation; // Listen for death
    }

    private void OnDisable()
    {
        health.OnDamaged -= PlayHurtAnimation;
        health.OnDeath -= PlayDeathAnimation; // Clean up subscription
    }

    private void PlayHurtAnimation()
    {
        anim.SetTrigger("Hit");
    }

    private void PlayDeathAnimation()
    {
        anim.SetTrigger("Die"); // Replace "Die" with your animator parameter name
    }
}