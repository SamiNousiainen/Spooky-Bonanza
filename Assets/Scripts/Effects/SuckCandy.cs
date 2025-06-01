using UnityEngine;
using Unity.Mathematics;

public class SuckCandy : MonoBehaviour
{
	[SerializeField] ParticleSystem candyParticles;
	[Tooltip("Candy Start Size relative to distance to player.")]
	[SerializeField] AnimationCurve particleStartLifetimeByDistance;
	[Tooltip("The distance that maps to the end range of the curve.")]
	[SerializeField] float curveEndDistance;
	Transform playerTransform;
	ParticleSystem.EmitParams emitParams;

	[Tooltip("Set this to true when the ghost should be stealing the candy.")]
	public bool SuckingCandy;
	bool emitting;

	void Start()
	{
		playerTransform = Player.instance.GetComponent<Transform>();
		emitParams.applyShapeToPosition = true; //Keep non-modified emit params
	}

	void Update()
	{
		emitParams.position = playerTransform.position; //Update the player's position in custom emit params
		if (SuckingCandy && !emitting)
		{
			InvokeRepeating("DoEmit", 0.1f, 0.25f); //Start sucking candies after 0.1 seconds, once every 0.25 seconds
			emitting = true;
		}
		if (emitting && !SuckingCandy)
		{
			CancelInvoke("DoEmit");
			emitting = false;
		}

	}
	void DoEmit()
	{
		float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
		float remappedDistance = distanceToPlayer / curveEndDistance;

		emitParams.startLifetime = particleStartLifetimeByDistance.Evaluate(remappedDistance); //Evaluate the start lifetime curve based on distance on a range 0 to CurveEndDistance

		//Debug.Log($"Distance is: {distanceToPlayer}, remapped distance is {remappedDistance}, so startLifetime is {particleStartLifetimeByDistance.Evaluate(remappedDistance)}");

		candyParticles.Emit(emitParams, 1); //Emit one candy

	}
}
