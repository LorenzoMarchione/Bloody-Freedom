
using System.Collections;
using UnityEngine;

public class FlickLight : MonoBehaviour
{
    [Header("Luz")]
    public Light lig;
    public Color colorLight = new Color(1f, 0.55f, 0.2f);

    [Header("Intensidad")]
    public float min = 1.5f;
    public float max = 3f;
    public float timer = 3f;
    public float smooth = 0.15f;

    [Header("Movimiento de sombras")]
    public bool moveShadow = false;
    public float speedMoveShadow = 1f;
    public float speedSmoothShadow = 50f;

    private Vector3 _startPosLight;
    private float _flickIntensity;

    void Start()
    {
        if (lig == null)
            lig = GetComponent<Light>();

        if (lig == null)
        {
            Debug.LogError("FlickLight: no hay ninguna luz asignada.", this);
            enabled = false;
            return;
        }

        _startPosLight = lig.transform.position;
        _flickIntensity = lig.intensity;
        lig.color = colorLight;

        StartCoroutine(SmoothFlick());
    }

    void Update()
    {
        if (lig == null)
            return;

        lig.intensity = Mathf.Lerp(
            lig.intensity,
            _flickIntensity,
            timer * Time.deltaTime
        );

        MoveShadowLight();
    }

    private IEnumerator SmoothFlick()
    {
        while (true)
        {
            _flickIntensity = Random.Range(min, max);
            yield return new WaitForSeconds(smooth);
        }
    }

    void MoveShadowLight()
    {
        if (moveShadow)
        {
            lig.transform.position = _startPosLight +
                Random.insideUnitSphere *
                (speedMoveShadow / speedSmoothShadow);
        }
        else
        {
            lig.transform.position = _startPosLight;
        }
    }
}

