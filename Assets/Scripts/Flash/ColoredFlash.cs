using System.Collections;
using UnityEngine;

public class ColoredFlash : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Material flashMaterial;
    [SerializeField]
    private float duration;

    private Material originalMaterial;
    private Coroutine flashRoutine;

    private void Start()
    {
        originalMaterial = spriteRenderer.material;
        flashMaterial = new Material(flashMaterial);
    }

    private IEnumerator FlashRoutine(Color color)
    {
        spriteRenderer.material = flashMaterial;
        flashMaterial.color = color;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }

    public void Flash(Color color)
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine(color));
    }
}
