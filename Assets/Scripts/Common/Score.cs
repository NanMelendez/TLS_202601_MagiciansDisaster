using UnityEngine;

public class Score : MonoBehaviour
{
	[SerializeField] private int score;

	public int CurrentScore
	{
		get { return score; }
	}

	public void IncrementScore(int score)
	{
		this.score += score;
	}

	public void DecrementScore(int score)
	{
		this.score = Mathf.Max(this.score - score, 0);
	}
}
