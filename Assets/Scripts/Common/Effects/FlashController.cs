using System.Collections;
using UnityEngine;

public class FlashController : MonoBehaviour
{
	[SerializeField] private SpriteRenderer spriteRenderer;
	
	private Material material;
	private Coroutine currFlashCoroutine;
	private Coroutine currTintCoroutine;

	private void Awake()
	{
		material = spriteRenderer.material;
	}

	public void Flash(Color color, float time)
	{
		StopFlash();

		SetColor(color);
		currFlashCoroutine = StartCoroutine(FlashCoroutine(time));
	}

	public void Tint(Color color, float time, bool offOnFinished = false)
	{
		StopTint();

		SetColor(color);
		currTintCoroutine = StartCoroutine(TintCoroutine(time, offOnFinished));
	}

	public void TintThenFlash(Color tintColor, Color flashColor, float tintTime, float flashTime)
	{
		StopTint();
		StopFlash();

		StartCoroutine(TTFCoroutine(tintColor, flashColor, tintTime, flashTime));
	}

	public void StopFlash()
	{
		if (currFlashCoroutine != null)
		{
			StopCoroutine(currFlashCoroutine);
			SetFactor(0.0f);
			currFlashCoroutine = null;
		}
	}

	public void StopTint()
	{
		if (currTintCoroutine != null)
		{
			StopCoroutine(currTintCoroutine);
			SetFactor(0.0f);
			currTintCoroutine = null;
		}
	}

	private void SetColor(Color color)
	{
		material.SetColor("_FlashColor", color);
	}

	private void SetFactor(float factor)
	{
		material.SetFloat("_FlashAmount", factor);
	}

	private IEnumerator FlashCoroutine(float time)
	{
		float elapsed = 0.0f;

		while (elapsed < time)
		{
			elapsed += Time.deltaTime;
			SetFactor(1.0f - elapsed / time);

			yield return null;
		}

		SetFactor(0.0f);
	}

	private IEnumerator TintCoroutine(float time, bool offOnFinished = false)
	{
		float elapsed = 0.0f;

		while (elapsed < time)
		{
			elapsed += Time.deltaTime;

			SetFactor(elapsed / time);

			yield return null;
		}

		if (offOnFinished)
			SetFactor(0.0f);
	}

	private IEnumerator TTFCoroutine(Color colorTint, Color colorFlash, float timeTint, float timeFlash)
	{
		SetColor(colorTint);
		currTintCoroutine = StartCoroutine(TintCoroutine(timeTint));
		yield return currTintCoroutine;

		SetColor(colorFlash);
		currFlashCoroutine = StartCoroutine(FlashCoroutine(timeFlash));
		yield return currFlashCoroutine;
	}
}
