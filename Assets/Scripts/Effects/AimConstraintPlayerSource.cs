using System.Data;
using UnityEngine;
using UnityEngine.Animations;

public class AimConstraintPlayerSource : MonoBehaviour
{
	public Transform playerTransform;
	AimConstraint aimConstraint;
	void Start()
	{
		playerTransform = Player.instance.transform;
		aimConstraint = GetComponent<AimConstraint>();
		ConstraintSource playerSource = new ConstraintSource { sourceTransform = playerTransform, weight = 1f };
		aimConstraint.AddSource(playerSource);
	}
}
