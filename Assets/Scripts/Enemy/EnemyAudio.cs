
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private Health health;

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] hitSounds;
    [SerializeField] private AudioClip[] deathSounds;

    private AudioSource audioSource;

    private int lastHitIndex = -1;
    private int lastDeathIndex = -1;

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
        PlayRandomSound(hitSounds, ref lastHitIndex);
    }

    private void PlayDeathSound()
    {
        PlayRandomSound(deathSounds, ref lastDeathIndex);
    }

    private void PlayRandomSound(AudioClip[] clips, ref int lastIndex)
    {
        if (clips == null || clips.Length == 0)
            return;

        int index = Random.Range(0, clips.Length);

        // Evitar repetir el sonido anterior
        if (clips.Length > 1)
        {
            if (index == lastIndex)
            {
                index = (index + Random.Range(1, clips.Length)) % clips.Length;
            }
        }

        lastIndex = index;

        if (clips[index] != null)
            audioSource.PlayOneShot(clips[index]);
    }
}
