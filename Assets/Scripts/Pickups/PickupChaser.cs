using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PickupChaser : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private LayerMask targetLayer;

	private Vector2 targetLocation;

	private void Awake()
	{
		StartCoroutine(DetectionCheck());
	}

	private void OnDrawGizmos()
	{
#if UNITY_EDITOR
        Gizmos.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);
#endif
    }

	private void Update()
	{
		if (targetLocation != Vector2.zero)
			transform.position = Vector2.MoveTowards(transform.position, targetLocation, 0.01f);
	}

	private IEnumerator DetectionCheck()
	{
		while (true)
		{
			yield return null;
			DetectPlayer();
		}
	}

	private void DetectPlayer()
	{
		Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

		if (rangeCheck.Length > 0)
		{
			Transform target = rangeCheck[0].transform;

			targetLocation = rangeCheck[0].transform.position;
		}
		else
			targetLocation = Vector2.zero;
	}
}
