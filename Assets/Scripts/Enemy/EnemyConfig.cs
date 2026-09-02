using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private float walkSpeed = 10f;
    public float WalkSpeed { get { return walkSpeed; } }
}
