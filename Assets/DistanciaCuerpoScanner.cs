using UnityEngine;

public class DistanciaCuerpoScanner : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    [Header("Configuración del Sensor")]
    [Tooltip("Distancia en metros a la que salta el aviso.")]
    public float distanciaMinima = 0.5f;

    private Transform playerCamera;
    private Transform scanner;

    public bool yaCompletado = false;
    public bool juegoIniciado = false;
    public bool infraccionCometida = false;
    private float tiempoDeGracia = 10f;

    void Start()
    {
        if (canvasEntero != null) canvasEntero.SetActive(false);
    }

    void Update()
    {
        if (yaCompletado) return;
        if (!juegoIniciado) return;

        if (playerCamera == null)
        {
            if (Camera.main != null) playerCamera = Camera.main.transform;
            return;
        }

        if (scanner == null)
        {
            GameObject objetoScanner = GameObject.FindGameObjectWithTag("Scanner");
            if (objetoScanner != null) scanner = objetoScanner.transform;
            return;
        }

        if (tiempoDeGracia > 0)
        {
            tiempoDeGracia -= Time.deltaTime;
            return;
        }

        float distancia = Vector3.Distance(playerCamera.position, scanner.position);

        // ¡AQUÍ ESTÁ LA LÍNEA ARREGLADA PARA QUE EL BOTÓN FUNCIONE!
        if (distancia < distanciaMinima && !infraccionCometida)
        {
            MostrarAviso();
        }
    } // <- Esta era la llave rebelde que se había borrado

    public void EmpezarJuego()
    {
        juegoIniciado = true;
    }

    private void MostrarAviso()
    {
        infraccionCometida = true;

        canvasEntero.SetActive(true);
        panelAvisoCorto.SetActive(true);
        panelExplicacionLarga.SetActive(false);

        if (playerCamera != null)
        {
            Vector3 posicionFrente = playerCamera.position + (playerCamera.forward * 1.0f);
            posicionFrente.y -= 0.15f;
            canvasEntero.transform.position = posicionFrente;
            canvasEntero.transform.LookAt(new Vector3(playerCamera.position.x, canvasEntero.transform.position.y, playerCamera.position.z));
            canvasEntero.transform.Rotate(0, 180, 0);
        }
    }

    // --- Funciones para los Botones ---
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
}