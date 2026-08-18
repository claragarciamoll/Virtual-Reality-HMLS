using UnityEngine;

public class MenuInicio : MonoBehaviour
{
    public GameObject canvasInicio; // Aquí arrastraremos tu Canvas de Inicio

    // Esta función se activará al pulsar "START SCAN"
    public void EmpezarJuego()
    {
        // Apagamos el cartel para que el jugador pueda ver la casa y empezar
        canvasInicio.SetActive(false);
    }
}