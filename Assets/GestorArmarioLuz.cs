using UnityEngine;

public class GestorArmarioLuz : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    [Header("Referencias de la Luz")]
    public GameObject luzArmario; // Aquí arrastras tu luz

    public bool yaCompletadoParaSiempre = false;

    void Start()
    {
        // Nos aseguramos de que el cartel y la luz empiecen apagados
        if (canvasEntero != null) canvasEntero.SetActive(false);
        if (luzArmario != null) luzArmario.SetActive(false);
    }

    // --- SENSOR DE LA PUERTA (Proximidad) ---
    private void OnTriggerEnter(Collider other)
    {
        // Al entrar, AHORA SOLO MOSTRAMOS EL CARTEL (a oscuras)
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

        // Hacemos que el cartel mire directamente al jugador
        if (Camera.main != null)
        {
            Transform playerCamera = Camera.main.transform;
            canvasEntero.transform.LookAt(new Vector3(playerCamera.position.x, canvasEntero.transform.position.y, playerCamera.position.z));
            canvasEntero.transform.Rotate(0, 180, 0);
        }
    }

    // --- FUNCIONES PARA LOS BOTONES ---

    public void BotonVerMasInfo()
    {
        panelAvisoCorto.SetActive(false);
        panelExplicacionLarga.SetActive(true);
    }

    // ¡AQUÍ ESTÁ LA MAGIA AHORA!
    public void BotonTerminar()
    {
        canvasEntero.SetActive(false); // Apagamos el cartel
        yaCompletadoParaSiempre = true; // Lo bloqueamos

        // ENCENDEMOS LA LUZ JUSTO AL DARLE A "OK" O "UNDERSTOOD"
        if (luzArmario != null)
        {
            luzArmario.SetActive(true);
        }
    }
}