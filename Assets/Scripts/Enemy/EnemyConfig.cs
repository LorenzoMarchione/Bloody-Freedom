using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private float targetDistance = 15f;
    [SerializeField] private float distanceRange = 1.0f; 
    public float WalkSpeed { get => walkSpeed; }
    public float TargetDistance { get => targetDistance; }
    public float DistanceRange { get => distanceRange; }
}
