using UnityEngine;

/// <summary>
/// Enum representing current state of an enemy in state machine
/// </summary>
public enum EnemyState {
    Default,
    Attack,
    Chase,
    Flee
}
