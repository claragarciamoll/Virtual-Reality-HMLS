using UnityEngine;

public class AvisoUnicoScannerArmario : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    public bool yaSeMostro = false; // El candado para que solo pase 1 vez

    void Start()
    {
        if (canvasEntero != null) canvasEntero.SetActive(false);
    }

    // El sensor detecta cuando ALGO entra en la zona de 0.5m
    private void OnTriggerEnter(Collider other)
    {
        // SI ya se mostró antes, NO hagas nada
        if (yaSeMostro) return;

        // SI lo que ha entrado tiene el tag "Scanner"...
        if (other.CompareTag("Scanner"))
        {
            MostrarAviso();
        }
    }

    private void MostrarAviso()
    {
        canvasEntero.SetActive(true);
        panelAvisoCorto.SetActive(true);
        panelExplicacionLarga.SetActive(false);

        // Orientar el cartel hacia el jugador
        if (Camera.main != null)
        {
            Transform playerCam = Camera.main.transform;
            canvasEntero.transform.LookAt(new Vector3(playerCam.position.x, canvasEntero.transform.position.y, playerCam.position.z));
            canvasEntero.transform.Rotate(0, 180, 0);
        }
    }

    // Funciones para los botones
    public void BotonVerMasInfo()
    {
        panelAvisoCorto.SetActive(false);
        panelExplicacionLarga.SetActive(true);
    }

    public void BotonEntendido()
    {
        canvasEntero.SetActive(false);
        yaSeMostro = true; // Cerramos el candado para siempre
    }
}