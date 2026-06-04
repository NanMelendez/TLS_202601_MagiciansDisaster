using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
  public int VidaMaxima = 100;
    public int Puntuacion = 0;
    public int NivelActual = 1;

    public enum EstadoJuego { Jugando, Pausado, GameOver, Victoria }
    public EstadoJuego estadoActual;

    public static GameManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        estadoActual = EstadoJuego.Jugando;
        Time.timeScale = 1f;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (estadoActual == EstadoJuego.Jugando)
            {
                PausarJuego();
            }
            else if (estadoActual == EstadoJuego.Pausado)
            {
                ReanudarJuego();
            }
        }
    }
    public void SumarPuntuacion(int cantidad)
    {
        if (estadoActual != EstadoJuego.Jugando) return;
        Puntuacion += cantidad;
        Debug.Log("Puntuacion actual" + Puntuacion);

        if(Puntuacion >= 100)
        {
            Victoria();
        }
        
    }

    public void PerderVida()
    {
        if(estadoActual != EstadoJuego.Jugando) return;
        VidaMaxima -= 10;
        Debug.Log("Vida actual: " + VidaMaxima);
        if (VidaMaxima <= 0)
        {
            PerderJuego();
        }
    }
    public void PausarJuego()
    {
        estadoActual = EstadoJuego.Pausado;
        Time.timeScale = 0f;
        Debug.Log("Juego pausado");
    }
    public void ReanudarJuego()
    {
        estadoActual = EstadoJuego.Jugando;
        Time.timeScale = 1f;
        Debug.Log("Juego reanudado");
    }
    public void PerderJuego()
    {
        estadoActual = EstadoJuego.GameOver;
        Time.timeScale = 0f;
        Debug.Log("¡Has perdido!");
    }
    public void Victoria()
    {
        estadoActual = EstadoJuego.Victoria;
        Time.timeScale = 0f;
        Debug.Log("¡Has ganado!");
    }
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        VidaMaxima = 100;
        Puntuacion = 0;
        NivelActual = 1;
        estadoActual = EstadoJuego.Jugando;
        Time.timeScale = 1f;
        Debug.Log("Juego reiniciado");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void SiguienteNivel()
    {
        Time.timeScale = 1f;
        int nivelActual = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(nivelActual + 1);
    }
    public void GuardarDatos()
    {
        PlayerPrefs.SetInt("Puntuacion", Puntuacion);
        PlayerPrefs.SetInt("NivelActual", NivelActual);
        PlayerPrefs.Save();
        Debug.Log("Datos guardados");
    }
    public void CargarDatos()
    {
        if(PlayerPrefs.HasKey("Puntuacion guardada"))
        {
            Puntuacion = PlayerPrefs.GetInt("Puntuacion guardada");
            VidaMaxima = PlayerPrefs.GetInt("VidaMaxima guardada");
        }
    }
   
}
