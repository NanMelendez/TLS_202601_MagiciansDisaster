using UnityEngine;

public class AttackAnimation : MonoBehaviour
{
	[SerializeField] private Animator spriteAnimator;

	private static readonly string chargedStr = "charged";
	private static readonly string blastStr = "blast";
	private static readonly int hashCharged = Animator.StringToHash(chargedStr);
	private static readonly int hashBlast = Animator.StringToHash(blastStr);

	public void Init(bool charged)
	{
		if (ExistsInAnimator(chargedStr))
			spriteAnimator.SetFloat(hashCharged, charged ? 1.0f : 0.0f);
	}

	public void Blast()
	{
		if (ExistsInAnimator(blastStr))
			spriteAnimator.SetTrigger(hashBlast);
	}

	private bool ExistsInAnimator(string name)
	{
		foreach (AnimatorControllerParameter param in spriteAnimator.parameters)
			if (param.name == name)
				return true;
		return false;
	}
}
