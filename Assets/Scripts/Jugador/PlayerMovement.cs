using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    [SerializeField]
    private Rigidbody2D rb2d;
    [SerializeField]
    private InputActionReference inputMovement;
    [SerializeField]
    private InputActionReference inputMouse;
    private Vector2 dirMovement;
    private Vector2 dirAim;

    // Importante: ¡Crear la referencia al InputAction aquí!
    void Awake()
    {
        dirAim = new Vector2(1.0f, 0.0f);
    }

    // Importante: ¡Siempre activar dicha referencia aquí!
    void OnEnable()
    {
        inputMovement.action.Enable();
        inputMouse.action.Enable();
    }

    // Importante: ¡Siempre desactivar dicha referencia aquí!
    void OnDisable()
    {
        inputMovement.action.Disable();
        inputMouse.action.Disable();
    }
    
    void Update()
    {
        dirMovement = inputMovement.action.ReadValue<Vector2>();
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        dirAim = (mousePos - screenPos).normalized;
    }

    void FixedUpdate()
    {
        rb2d.linearVelocity = dirMovement * speed;
    }

    public Vector2 AimDirection
    {
        get { return dirAim; }
    }
}
