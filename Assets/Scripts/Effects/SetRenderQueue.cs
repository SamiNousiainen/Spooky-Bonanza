using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRenderQueue : MonoBehaviour
{
	[SerializeField] int renderQueue = 2000;
	int activeRenderQueue;
	Material instanceMaterial;
	void Start()
	{
		OverrideRenderQueue();
	}

	void Update()
	{
		if (renderQueue != activeRenderQueue)
		{
			instanceMaterial.renderQueue = renderQueue;
		}
	}

	[ContextMenu("Override render queue")]
	public void OverrideRenderQueue()
	{
		instanceMaterial = GetComponent<Renderer>().material;
		instanceMaterial.renderQueue = renderQueue;
		activeRenderQueue = renderQueue;
	}
}
