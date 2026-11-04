using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance { get; private set; }

    [Header("Player Status")]
    public bool hasEquipment = false;

    [SerializeField] GameObject player;
    [SerializeField] GameObject sword;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Call this method to give the player equipment
    public void GrantEquipment()
    {
        hasEquipment = true;

        player.GetComponent<Animator>().SetLayerWeight(1, 1f);
        sword.GetComponent<MeshRenderer>().enabled = true;

        Debug.Log("Equipment granted to player!");
    }
}