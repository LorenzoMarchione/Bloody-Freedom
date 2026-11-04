using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [Header("Player Status")]
    public bool hasEquipment = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Call this method to give the player equipment
    public void GrantEquipment()
    {
        hasEquipment = true;
        Debug.Log("Equipment granted to player!");
    }
}