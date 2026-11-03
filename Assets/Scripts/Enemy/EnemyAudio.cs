using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        health.OnDamaged += PlayHitSound;
        health.OnDeath += PlayDeathSound;
    }

    private void OnDisable()
    {
        health.OnDamaged -= PlayHitSound;
        health.OnDeath -= PlayDeathSound;
    }

    private void PlayHitSound()
    {
        audioSource.PlayOneShot(hitSound);
    }
    private void PlayDeathSound()
    {
        audioSource.PlayOneShot(deathSound);
    }
}