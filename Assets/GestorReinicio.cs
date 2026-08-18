using UnityEngine;
using UnityEngine.SceneManagement; // OBLIGATORIO para reiniciar escenas

public class GestorReinico : MonoBehaviour
{
    [Header("Configuración de Caída")]
    public float limiteSuelo = -10f; // Si bajas de -10 metros, mueres

    void Update()
    {
        // 1. COMPROBACIÓN DE CAÍDA
        // Si la posición de este objeto (el Player) es menor que el límite...
        if (transform.position.y < limiteSuelo)
        {
            ReiniciarEscena();
        }
    }

    // 2. FUNCIÓN PARA EL BOTÓN (Pública para que el botón la vea)
    public void ReiniciarEscena()
    {
        // Carga la escena que está abierta actualmente
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}