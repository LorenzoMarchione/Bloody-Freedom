using UnityEngine;

public class Billboard : MonoBehaviour
{
    private void LateUpdate()
    {
        // Forces the sprite to rotate toward the main camera every frame
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward,
                         Camera.main.transform.rotation * Vector3.up);
    }
}