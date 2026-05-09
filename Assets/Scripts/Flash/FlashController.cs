using UnityEngine;

public class FlashController : MonoBehaviour
{
    [SerializeField]
    private ColoredFlash flashEffect;
    [SerializeField]
    private Color flashColor;

    public void Flash()
    {
        flashEffect.Flash(flashColor);
    }
}
