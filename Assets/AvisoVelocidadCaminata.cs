using UnityEngine;

public class AvisoVelocidadCaminata : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    [Header("Configuración del Sensor")]
    public float limiteVelocidad = 0.8f;

    private Transform jugadorCamara;
    private Vector3 posicionAnterior;
    public bool yaCompletado = false;
    public bool infraccionCometida = false;
    public bool juegoIniciado = false;

    // El escudo para ignorar el salto de las gafas al arrancar
    private float tiempoDeGracia = 30f;

    void Start()
    {
        if (canvasEntero != null) canvasEntero.SetActive(false);
    }

    void Update()
    {
        if (yaCompletado) return;
        if (!juegoIniciado) return;
        // 1. Buscamos la cámara principal (la cabeza del jugador)
        if (jugadorCamara == null)
        {
            if (Camera.main != null)
            {
                jugadorCamara = Camera.main.transform;
                // Usamos la posición global porque queremos saber si se mueve por el mundo
                posicionAnterior = jugadorCamara.position;
            }
            return;
        }

        // 2. PAUSA DE SEGURIDAD (4 segundos)
        if (tiempoDeGracia > 0)
        {
            tiempoDeGracia -= Time.deltaTime;
            posicionAnterior = jugadorCamara.position; // Actualizamos para que no calcule el salto
            return;
        }

        // 3. Calculamos la velocidad real usando solo los ejes X y Z (ignoramos los saltos hacia arriba/abajo)
        Vector3 posicionActualPlana = new Vector3(jugadorCamara.position.x, 0, jugadorCamara.position.z);
        Vector3 posicionAnteriorPlana = new Vector3(posicionAnterior.x, 0, posicionAnterior.z);

        float distanciaMovida = Vector3.Distance(posicionAnteriorPlana, posicionActualPlana);
        float velocidadActual = distanciaMovida / Time.deltaTime;

        // Si vas más rápido que 0.8...
        if (velocidadActual > limiteVelocidad)
        {
            MostrarAviso();
        }

        posicionAnterior = jugadorCamara.position;
    }

    private void MostrarAviso()
    {
        canvasEntero.SetActive(true);
        panelAvisoCorto.SetActive(true);
        panelExplicacionLarga.SetActive(false);

        // Mover el cartel justo delante de ti estés donde estés
        if (Camera.main != null)
        {
            Transform playerCam = Camera.main.transform;

            // 1. Calcula un punto exactamente 1 metro por delante de hacia donde estés mirando
            Vector3 posicionFrente = playerCam.position + (playerCam.forward * 1.0f);

            // (Opcional) Lo bajamos 15 centímetros para que no te tape los ojos por completo y puedas ver el suelo
            posicionFrente.y -= 0.15f;

            // 2. Teletransporta el cartel a ese punto
            canvasEntero.transform.position = posicionFrente;

            // 3. Gira el cartel para que te mire a ti (para que no lo veas de lado ni del revés)
            canvasEntero.transform.LookAt(new Vector3(playerCam.position.x, canvasEntero.transform.position.y, playerCam.position.z));
            canvasEntero.transform.Rotate(0, 180, 0);
        }
        {
            infraccionCometida = true; // <--- Añadimos la multa instantánea

            canvasEntero.SetActive(true);
            panelAvisoCorto.SetActive(true);
            // ... (el resto de mover el cartel que pusimos antes sigue igual)
        }
    }

    // --- Botones ---
    public void BotonVerMasInfo()
    {
        panelAvisoCorto.SetActive(false);
        panelExplicacionLarga.SetActive(true);
    }

    public void BotonEntendido()
    {
        canvasEntero.SetActive(false);
        yaCompletado = true;
    }
    public void EmpezarJuego()
    {
        juegoIniciado = true; // Ponemos el semáforo en verde
    }
}