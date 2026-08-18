using UnityEngine;

public class GestorAvisoSimple : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    public bool yaCompletadoParaSiempre = false;
    public bool infraccionCometida = false;

    void Start()
    {
        // El cartel empieza apagado
        if (canvasEntero != null) canvasEntero.SetActive(false);
    }

    // SENSOR DE PROXIMIDAD
    private void OnTriggerEnter(Collider other)
    {
        // Salta en cuanto el jugador toca el cubo invisible
        if (other.CompareTag("Player") && !yaCompletadoParaSiempre)
        {
            MostrarCartelPrincipal();
        }
    }

    private void MostrarCartelPrincipal()
    {
        canvasEntero.SetActive(true);
        panelAvisoCorto.SetActive(true);
        panelExplicacionLarga.SetActive(false);

        // Hace que el cartel mire directamente al jugador
        if (Camera.main != null)
        {
            Transform playerCamera = Camera.main.transform;
            canvasEntero.transform.LookAt(new Vector3(playerCamera.position.x, canvasEntero.transform.position.y, playerCamera.position.z));
            canvasEntero.transform.Rotate(0, 180, 0);
        }
        infraccionCometida = true;
    }

    // FUNCIONES PARA LOS BOTONES
    public void BotonVerMasInfo()
    {
        panelAvisoCorto.SetActive(false);
        panelExplicacionLarga.SetActive(true);
    }

    public void BotonTerminar()
    {
        canvasEntero.SetActive(false);
        yaCompletadoParaSiempre = true; // Se bloquea y no vuelve a molestar
    }
}