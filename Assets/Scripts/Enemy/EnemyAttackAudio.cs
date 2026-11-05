
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EnemyAttackAudio : MonoBehaviour
{
    [Header("Ranged Attack Sounds")]
    [SerializeField] private AudioClip[] rangedAttackSounds;

    private AudioSource audioSource;
    private int lastRangedIndex = -1;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayRangedAttackSound()
    {
        if (rangedAttackSounds == null || rangedAttackSounds.Length == 0)
            return;

        int index = Random.Range(0, rangedAttackSounds.Length);

        // Evitar repetir el mismo sonido consecutivamente
        if (rangedAttackSounds.Length > 1 && index == lastRangedIndex)
        {
            index = (index + Random.Range(1, rangedAttackSounds.Length))
                    % rangedAttackSounds.Length;
        }

        lastRangedIndex = index;

        if (rangedAttackSounds[index] != null)
            audioSource.PlayOneShot(rangedAttackSounds[index]);
    }
}
