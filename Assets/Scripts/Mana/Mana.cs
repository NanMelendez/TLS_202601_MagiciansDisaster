using UnityEngine;
using UnityEngine.UI;

public class Mana : MonoBehaviour
{
    public int ManaMaxima = 100;
    public int ManaActual;
    public float RadioDeRecuperacion = 2f;
    public Text TextoMana;

    private void Start()
    {
        ManaActual = ManaMaxima;
        ActualizarManaUI();
    }

    private void Update()
    {
        RegeneracionDeMana();
    }
    public bool UsarMana(int cantidad)
    {
        if(ManaActual >= cantidad)
        {
            ManaActual -= cantidad;
            ActualizarManaUI();
            Debug.Log("Mana usada: " + cantidad + " / " + ManaActual);
            return true;
        }
        else
        {
            Debug.Log("Mana insuficiente recolectar para utilizar habilidades");
            return false;
        }
    }
    void RegeneracionDeMana()
    {
      if( ManaActual < ManaMaxima)
        {
            ManaActual += Mathf.RoundToInt(RadioDeRecuperacion * Time.deltaTime);
            ManaActual = Mathf.Clamp(ManaActual, 0, ManaMaxima);
            ActualizarManaUI();
        }
    }

    public void RegenerarMana(int cantidad)
    {
        ManaActual += cantidad;
        ManaActual = Mathf.Clamp(ManaActual, 0, ManaMaxima);
        ActualizarManaUI();
        Debug.Log("Se restauraron" + cantidad + "De mana.Actual" + ManaActual);
    }

    void ActualizarManaUI()
    {
        if(TextoMana != null)
        {
            TextoMana.text = "Mana" + ManaActual + "/" + ManaMaxima;
        }
    }
}
