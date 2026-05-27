using System.Collections;
using UnityEngine;

public class FlashController : MonoBehaviour
{
	[SerializeField] private Color color = Color.white;

	[SerializeField] private SpriteRenderer sprRenderer;
	[SerializeField] private Material flashMaterial;
	[SerializeField] private float duration;

	private Material originalMaterial;
	private Coroutine flashRoutine;

	private void Awake()
	{
		originalMaterial = sprRenderer.material;
		flashMaterial = new Material(flashMaterial);
	}

	private IEnumerator FlashRoutine(Color color)
	{
		sprRenderer.material = flashMaterial;
		flashMaterial.color = color;

		yield return new WaitForSeconds(duration);

		sprRenderer.material = originalMaterial;
		flashRoutine = null;
	}

	public void Flash()
	{
		if (flashRoutine != null)
			StopCoroutine(flashRoutine);

		flashRoutine = StartCoroutine(FlashRoutine(color));
	}
}
