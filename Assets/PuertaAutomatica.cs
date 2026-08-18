using UnityEngine;
using System.Collections;

public class PuertaAutomatica : MonoBehaviour
{
    [Header("Conexión")]
    [Tooltip("Arrastra aquí el objeto vacío (la bisagra) que contiene tu puerta")]
    public Transform bisagraPuerta;

    [Header("Configuración")]
    [Tooltip("¿Cuántos grados se abre la puerta? (Pon -90 si se abre hacia el lado contrario)")]
    public float anguloDeApertura = 90f;
    [Tooltip("Velocidad a la que se mueve la puerta")]
    public float velocidadApertura = 3f;

    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;
    private Coroutine animacionActual;

    void Start()
    {
        if (bisagraPuerta != null)
        {
            // Guardamos cómo está la puerta al empezar (cerrada)
            rotacionCerrada = bisagraPuerta.localRotation;
            // Calculamos cómo estará cuando se abra
            rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloDeApertura, 0);
        }
    }

    // Cuando el jugador ENTRA en el cubo invisible
    void OnTriggerEnter(Collider other)
    {
        // IMPORTANTE: Asegúrate de que tus gafas/mandos tienen uno de estos Tags
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            MoverPuerta(rotacionAbierta);
        }
    }

    // Cuando el jugador SALE del cubo invisible
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            MoverPuerta(rotacionCerrada);
        }
    }

    // Función que decide hacia dónde se mueve la puerta
    private void MoverPuerta(Quaternion rotacionDestino)
    {
        if (bisagraPuerta == null) return;

        // Si la puerta ya se estaba moviendo, paramos esa animación para empezar la nueva
        if (animacionActual != null)
        {
            StopCoroutine(animacionActual);
        }
        animacionActual = StartCoroutine(AnimarRotacion(rotacionDestino));
    }

    // Animación suave de la puerta
    IEnumerator AnimarRotacion(Quaternion destino)
    {
        // Mientras la puerta no haya llegado a su destino, seguimos rotándola
        while (Quaternion.Angle(bisagraPuerta.localRotation, destino) > 0.01f)
        {
            bisagraPuerta.localRotation = Quaternion.Slerp(bisagraPuerta.localRotation, destino, Time.deltaTime * velocidadApertura);
            yield return null; // Esperamos al siguiente fotograma
        }

        // Nos aseguramos de dejarla exactamente en su sitio al terminar
        bisagraPuerta.localRotation = destino;
    }
}