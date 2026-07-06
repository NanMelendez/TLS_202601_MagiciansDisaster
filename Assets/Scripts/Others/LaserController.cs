using System;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private List<Texture> texturesNormal = new();
	[SerializeField] private List<Texture> texturesCharged = new();
	[SerializeField] private float fps = 30.0f;

	private int animationStep;
	private float fpsCounter;

	[NonSerialized] public bool isCharged = false;

	private void Update()
	{
		fpsCounter += Time.deltaTime;

		if (fpsCounter >= 1.0f / fps)
		{
			animationStep++;
			if (animationStep == texturesNormal.Count)
				animationStep = 0;

			lineRenderer.material.SetTexture("_MainTex", isCharged ? texturesCharged[animationStep] : texturesNormal[animationStep]);

			fpsCounter = 0.0f;
		}
	}
}
