using UnityEngine;

/// <summary>
/// ScriptableObject containing all modifiable ghost enemy values
/// </summary>

[CreateAssetMenu(fileName = "GhostProperties", menuName = "Scriptable Objects/GhostProperties")]
public class GhostProperties : ScriptableObject {
    [Header("Stats")]
    public float partolMoveSpeed;
    public float fleeMoveSpeed;
    public float chaseMoveSpeed;
    public float attackRange;
    public float detectionRange;
}
