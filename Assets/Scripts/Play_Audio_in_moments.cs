
using System.Collections;
using UnityEngine;

public class Play_Audio_in_moments : MonoBehaviour
{
    public float minSeconds = 5f;
    public float maxSeconds = 15f;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        StartCoroutine(PlaySound());
    }

    private IEnumerator PlaySound()
    {
        while (true)
        {
            float waitTime = Random.Range(minSeconds, maxSeconds);

            yield return new WaitForSeconds(waitTime);

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
    }
}

