using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform ObjetivoTeleport;
    public KeyCode TeclaTeleport = KeyCode.P;

    private void Update()
    {
        if (Input.GetKeyDown(TeclaTeleport))
        {
            TeleportAlDestino();
        }

        void TeleportAlDestino()
        {
          if(ObjetivoTeleport != null)
            {
                transform.position = ObjetivoTeleport.position;
                Debug.Log("Persona teletransportada a: " + ObjetivoTeleport.position);
            }
        }
    }
}
