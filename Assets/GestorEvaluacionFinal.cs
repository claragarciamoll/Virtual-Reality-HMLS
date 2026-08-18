using UnityEngine;
using UnityEngine.InputSystem;

public class GestorEvaluacionFinal : MonoBehaviour
{
    [Header("El Botón del Mando")]
    public InputActionReference botonX;

    [Header("El Canvas Final")]
    public GameObject canvasEvaluacion;

    [Header("Scripts a Vigilar")]
    public GestorAvisoVentana avisoVentanas;
    public GestorAvisoVentana avisoEspejo;
    public GestorAvisoVentana avisoParedBlanca;
    public GestorArmarioLuz avisoLuzArmario;
    public GestorAvisoSimple avisoPuerta;
    public GestorAvisoSimple avisoNevera;
    public AvisoUnicoScannerArmario avisoDistanciaMuebles;
    public DistanciaCuerpoScanner avisoDistanciaCuerpo;
    public AvisoMovimientoBrusco avisoGiroMano;
    public AvisoVelocidadCaminata avisoVelocidadPaso;

    [Header("Filas de Logros (Izquierda)")]
    public GameObject filaLogroVentanas;
    public GameObject filaLogroEspejo;
    public GameObject filaLogroPared;
    public GameObject filaLogroLuz;
    public GameObject filaLogroPuerta;
    public GameObject filaLogroNevera;
    public GameObject filaLogroMuebles;
    public GameObject filaLogroCuerpo;
    public GameObject filaLogroGiro;
    public GameObject filaLogroPaso;

    [Header("Paneles de Texto (Derecha)")]
    public GameObject[] panelesInfoDerecha;

    void OnEnable() { if (botonX != null) botonX.action.Enable(); }

    void Update()
    {
        if (botonX != null && botonX.action.WasPressedThisFrame() && !canvasEvaluacion.activeSelf)
        {
            EvaluarYMostrarPantalla();
        }
    }

    private void EvaluarYMostrarPantalla()
    {
        canvasEvaluacion.SetActive(true);
        // Posicionar frente al jugador
        if (Camera.main != null)
        {
            Transform pc = Camera.main.transform;
            canvasEvaluacion.transform.position = pc.position + pc.forward * 0.9f;
            canvasEvaluacion.transform.LookAt(pc);
            canvasEvaluacion.transform.Rotate(0, 180, 0);
        }

        ApagarTodosLosTextosDerecha();

        // Si NO saltó el aviso (!), activamos la fila de logro (✅)
        if (avisoVentanas != null) filaLogroVentanas.SetActive(!avisoVentanas.yaCompletadoParaSiempre);
        if (avisoEspejo != null) filaLogroEspejo.SetActive(!avisoEspejo.yaCompletadoParaSiempre);
        if (avisoParedBlanca != null) filaLogroPared.SetActive(!avisoParedBlanca.yaCompletadoParaSiempre);
        if (avisoLuzArmario != null) filaLogroLuz.SetActive(!avisoLuzArmario.yaCompletadoParaSiempre);
        if (avisoPuerta != null) filaLogroPuerta.SetActive(!avisoPuerta.infraccionCometida);
        if (avisoNevera != null) filaLogroNevera.SetActive(!avisoNevera.infraccionCometida);
        if (avisoDistanciaMuebles != null) filaLogroMuebles.SetActive(!avisoDistanciaMuebles.yaSeMostro);
        if (avisoDistanciaCuerpo != null) filaLogroCuerpo.SetActive(!avisoDistanciaCuerpo.infraccionCometida);
        if (avisoGiroMano != null) filaLogroGiro.SetActive(!avisoGiroMano.yaCompletado);

        // AQUÍ ESTÁ EL CAMBIO MAGICO PARA LA VELOCIDAD:
        if (avisoVelocidadPaso != null) filaLogroPaso.SetActive(!avisoVelocidadPaso.infraccionCometida);
    }

    public void ApagarTodosLosTextosDerecha()
    {
        foreach (GameObject p in panelesInfoDerecha) if (p != null) p.SetActive(false);
    }
}