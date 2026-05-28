using Unity.VisualScripting;
using UnityEngine;

public class ControladorParticulas : MonoBehaviour
{
    [Header("Configuracion")]
    public bool reproducirAlInicio = false;

    public bool destruirAlTerminar = true;

    private ControladorParticulas sistemaParticulas;
    private void Awake()
    {
        sistemaParticulas = GetComponent<ControladorParticulas>();

        if (sistemaParticulas != null)
        {

        }
    }
    private void Start()
    {
        
        if (reproducirAlInicio && sistemaParticulas != null)
        {
            IniciarEfecto();
        }
    }

    public void IniciarEfecto()
    {
        if (sistemaParticulas != null)
        {
            sistemaParticulas.IniciarEfecto();
            Debug.Log("Efecto de particulas activado");
        }
    }
    public void DetenerEfecto(bool detenerDeGolpe = false)
    {
        if (sistemaParticulas != null)
        {
            if (detenerDeGolpe)
            {
                sistemaParticulas.DetenerEfecto();
            }
            
        }
        Debug.Log("Efecto de particulas detenido");
    }

    private void OnParticleSystemStopped()
    {
        if (destruirAlTerminar)
        {
            Destroy(gameObject);
            Debug.Log("Objeto de particulas eliminado");
        }
       
    }
}
            


            
           
        

