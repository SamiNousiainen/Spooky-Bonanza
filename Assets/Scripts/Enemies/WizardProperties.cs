using UnityEngine;

[CreateAssetMenu(fileName = "WizardProperties", menuName = "Scriptable Objects/WizardProperties")]
public class WizardProperties : ScriptableObject {
    [Header("Stats")]
    public float detectionRange;
    public float damage;
    public float attackRate;
    public float projectileSpeed;
}
