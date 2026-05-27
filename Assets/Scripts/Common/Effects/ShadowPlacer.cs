using UnityEngine;

public class ShadowPlacer : MonoBehaviour
{
    [SerializeField] private float distance;
    [SerializeField] private Vector2 scale = new(1.0f, 1.0f);

    private GameObject shadowInstance;

    private void Awake()
    {
        shadowInstance = Instantiate(Resources.Load<GameObject>("Misc/Shadow"));
        shadowInstance.transform.SetParent(transform);
        shadowInstance.transform.localPosition = new Vector3(0.0f, -distance, 0.0f);
        shadowInstance.transform.localScale *= scale;
    }
}
