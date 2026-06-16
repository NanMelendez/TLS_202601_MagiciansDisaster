using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
	[SerializeField] private Animator spriteAnimator;

	private static readonly int hashCharged = Animator.StringToHash("charged");
	private static readonly int hashBlast = Animator.StringToHash("blast");

	public void Init(bool charged)
	{
		spriteAnimator.SetFloat(hashCharged, charged ? 1.0f : 0.0f);
	}

	public void Blast()
	{
		spriteAnimator.SetTrigger(hashBlast);
	}
}
