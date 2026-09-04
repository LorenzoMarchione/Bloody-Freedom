using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemies/Base Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 2f;

    [Header("Combat Distances")]
    [SerializeField] private float targetDistance = 2f;
    [SerializeField] private float entryDistanceRange = 0.1f;
    [SerializeField] private float exitDistanceRange = 0.8f;

    [Header("Combat Rhythm")]
    [SerializeField] private float minStanceTime = 1.0f;
    [SerializeField] private float maxStanceTime = 5.0f;
    [SerializeField] private float minAttackCooldown = 2.0f;
    [SerializeField] private float maxAttackCooldown = 6.0f;

    [Header("Melee Specifics")]
    [SerializeField] private float stepInSpeed = 2.5f;
    [SerializeField] private float stepInDuration = 0.20f;

    public float WalkSpeed { get => walkSpeed; }
    public float TargetDistance { get => targetDistance; }
    public float EntryDistanceRange { get => entryDistanceRange; }
    public float ExitDistanceRange { get => exitDistanceRange; }
    public float MinStanceTime { get => minStanceTime; }
    public float MaxStanceTime { get => maxStanceTime; }
    public float MinAttackCooldown { get => minAttackCooldown; }
    public float MaxAttackCooldown { get => maxAttackCooldown; }
    public float StepInSpeed { get => stepInSpeed; }
    public float StepInDuration { get => stepInDuration; }
}
