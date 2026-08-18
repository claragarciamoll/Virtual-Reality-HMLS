using UnityEngine;

public class GestorAvisoVentana : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    [Header("Referencias Sensores")]
    public Transform puntoCriticoVentana;
    private Transform playerCamera;

    [Header("Ajustes")]
    public float sensibilidadMirada = 0.85f;

    // Controles internos
    private bool jugadorCerca = false;
    private bool cartelEstaVisible = false;
    public bool yaCompletadoParaSiempre = false;

    void Start()
    {
        if (Camera.main != null) playerCamera = Camera.main.transform;
    }

    // --- SENSORES DE MOVIMIENTO ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) jugadorCerca = false;
    }

    // --- SENSOR DE MIRADA ---
    void Update()
    {
        if (yaCompletadoParaSiempre || !jugadorCerca || cartelEstaVisible) return;

        Vector3 haciaDondeMira = playerCamera.forward;
        Vector3 haciaLaVentana = (puntoCriticoVentana.position - playerCamera.position).normalized;

        if (Vector3.Dot(haciaDondeMira, haciaLaVentana) > sensibilidadMirada)
        {
            MostrarCartelPrincipal();
        }
    }

    private void MostrarCartelPrincipal()
    {
        canvasEntero.SetActive(true);
        panelAvisoCorto.SetActive(true);
        panelExplicacionLarga.SetActive(false); // Por si acaso

        // Girar hacia el jugador
        canvasEntero.transform.LookAt(new Vector3(playerCamera.position.x, canvasEntero.transform.position.y, playerCamera.position.z));
        canvasEntero.transform.Rotate(0, 180, 0);

        cartelEstaVisible = true;
    }


    // --- FUNCIONES PARA LOS BOTONES ---

    // Función para el botón "MORE INFO"
    public void BotonVerMasInfo()
    {
        panelAvisoCorto.SetActive(false);
        panelExplicacionLarga.SetActive(true);
    }

    // Función para los botones "OK" (del aviso corto) y "UNDERSTOOD" (de la info)
    public void BotonTerminar()
    {
        canvasEntero.SetActive(false);
        yaCompletadoParaSiempre = true; // Bloqueado para siempre
    }
}