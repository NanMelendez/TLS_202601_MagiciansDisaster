using Unity.Cinemachine;
using UnityEngine;

public class CameraShakeManager : MonoBehaviour
{
	public static CameraShakeManager instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
	}

	public void Shake(CinemachineImpulseSource src, float strength)
	{
		src.GenerateImpulseWithForce(strength);
	}
}
