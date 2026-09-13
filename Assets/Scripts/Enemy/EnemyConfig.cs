using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Enemies/Base Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [Header("Movement Settings")]
    [SerializeField] private float walkSpeed = 2f;

    [Header("Defense Settings")]
    [SerializeField] private float recoilDuration = 0.3f;
    [SerializeField] private float recoilSpeed = 6f;

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
    [SerializeField] private float meleeRange = 2.5f;
    [SerializeField] private int meleeDamage = 10;
    [SerializeField] private float stepInSpeed = 2.5f;
    [SerializeField] private float stepInDuration = 0.20f;

    [Header("Ranged Specifics")]
    [SerializeField] private float rangedRange = 7f;
    [SerializeField] private float projectileSpeed = 5f;
    [SerializeField] private int projectileDamage = 25;
    [SerializeField] private GameObject profectilePrefab;

    public float WalkSpeed => walkSpeed; 
    public float RecoilDuration => recoilDuration; 
    public float RecoilSpeed => recoilSpeed;
    public float TargetDistance => targetDistance; 
    public float EntryDistanceRange => entryDistanceRange; 
    public float ExitDistanceRange => exitDistanceRange; 
    public float MinStanceTime => minStanceTime; 
    public float MaxStanceTime => maxStanceTime; 
    public float MinAttackCooldown => minAttackCooldown; 
    public float MaxAttackCooldown => maxAttackCooldown; 
    public float StepInSpeed => stepInSpeed; 
    public float StepInDuration => stepInDuration; 
    public float MeleeRange => meleeRange;
    public int MeleeDamage => meleeDamage;
    public float RangedRange => rangedRange; 
    public float ProjectileSpeed => projectileSpeed; 
    public int ProjectileDamage => projectileDamage;
    public GameObject ProjectilePrefab => profectilePrefab; 
}
