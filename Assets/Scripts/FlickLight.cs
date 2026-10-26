
using System.Collections;
using UnityEngine;

public class FlickLight : MonoBehaviour
{
    public Light lig;
    public Color colorLight = Color.white;

    public float min = 0.3f;
    public float max = 2f;
    public float timer = 3f;
    public float smooth = 0.1f;

    public bool moveShadow = false;
    public float speedMoveShadow = 1f;
    public float speedSmoothShadow = 50f;

    private float flickIntensity;
    private Vector3 startPosLight;

    void Start()
    {
        if (lig == null)
            lig = GetComponent<Light>();

        if (lig == null)
        {
            Debug.LogError("FlickLight: no se encontró una Light.", this);
            enabled = false;
            return;
        }

        startPosLight = lig.transform.position;
        flickIntensity = lig.intensity;

        StartCoroutine(SmoothFlick());
    }

    IEnumerator SmoothFlick()
    {
        while (true)
        {
            float target = Random.Range(min, max);
            float elapsed = 0f;
            float initial = lig.intensity;

            while (elapsed < smooth)
            {
                elapsed += Time.deltaTime;
                lig.intensity = Mathf.Lerp(
                    initial, target, elapsed / smooth
                );
                lig.color = colorLight;

                yield return null;
            }

            lig.intensity = target;
            yield return new WaitForSeconds(timer);
        }
    }

    void Update()
    {
        if (lig == null)
            return;

        if (moveShadow)
        {
            lig.transform.position = startPosLight +
                Random.insideUnitSphere *
                (speedMoveShadow / speedSmoothShadow);
        }
        else
        {
            lig.transform.position = startPosLight;
        }
    }
}
