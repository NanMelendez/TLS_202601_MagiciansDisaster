using System.Collections.Generic;
using UnityEngine;

public class TextureAnimator : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private List<Sprite> frames = new();
    [SerializeField] [Min(0.01f)] private float timeBetweenFrames = 0.01f;

    private void Update()
    {
        mat.SetTexture("_MainTex", frames[0].texture);
    }
}
