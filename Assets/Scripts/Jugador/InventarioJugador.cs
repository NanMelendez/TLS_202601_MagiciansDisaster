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
        if (InputInventario != null && InputInventario.action != null)
        {
            InputInventario.action.performed += OnToggleInventario;
            InputInventario.action.Enable();
        }
    }

    private void OnDisable()
    {
        if (InputInventario != null && InputInventario.action != null)
        {
            InputInventario.action.performed -= OnToggleInventario;
            InputInventario.action.Disable();
        }
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
            // Alternativa directa por teclado sin usar InputActionReference:
            if (inventario.gameObject.activeSelf)
            {
                inventario.OcultarInventario();
            }
            else
            {
                inventario.MostrarInventario();
            }
        }       
    }
   
    private void OnToggleInventario(InputAction.CallbackContext ctx)
    {
        if (inventario.gameObject.activeSelf)
        {
            inventario.OcultarInventario();
        }
        else
        {
            inventario.MostrarInventario();
        }
    }   
}
