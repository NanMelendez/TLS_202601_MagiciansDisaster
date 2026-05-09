using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventarioJugador : MonoBehaviour
{
    public Inventario inventario;
    private InputActionReference InputInventario;
    void Awake()
    {
       
    }

    private void OnEnable()
    {
        InputInventario.action.Enable();
    }

    private void OnDisable()
    {
        InputInventario.action.Disable();
    }
    private void Start()
    {
        inventario.AñadirItem("Hechizo Bola de Fuego", null, 2);
        inventario.AñadirItem("Baston mejorado", null, 1);

        inventario.MostrarInventario();
       
    }


    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.I))
        {
            inventario.MostrarInventario();
        }
        else
        {
            
            inventario.OcultarInventario();
        }
       
      
    }
   
    
}
