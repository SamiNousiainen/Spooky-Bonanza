using UnityEngine;

/// <summary>
/// ScriptableObject containing all modifiable player values
/// </summary>

[CreateAssetMenu(fileName = "PlayerProperties", menuName = "Scriptable Objects/PlayerProperties")]
public class PlayerProperties : ScriptableObject {
    [Header("Movement")]
    public float moveSpeed;
    public float jumpHeight;
    public float gravityModifier = -9.81f;

    [Header("Combat")]
    public float hitPoints;
    public float damage;
}
