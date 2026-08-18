using UnityEngine;

public class AvisoMovimientoBrusco : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject canvasEntero;
    public GameObject panelAvisoCorto;
    public GameObject panelExplicacionLarga;

    [Header("Configuración del Sensor")]
    [Tooltip("Baja este número a 20 temporalmente para probar si funciona.")]
    public float limiteVelocidadGiro = 200f;

    private Transform scanner;
    private Quaternion rotacionAnterior;
    public bool yaCompletado = false;
    public bool juegoIniciado = false;

    // Temporizador para ignorar el "tirón" de los mandos al darle al Play
    private float tiempoDeGracia = 50f;

    void Start()
    {
        // Apagamos el cartel por precaución
        if (canvasEntero != null) canvasEntero.SetActive(false);
        // Ya no buscamos el mando aquí, lo hacemos en el Update para no fallar
    }

    void Update()
    {
        // Si ya saltó el aviso, apagamos el sensor para ahorrar recursos
        if (yaCompletado) return;
        if (!juegoIniciado) return;
        // 1. BUSCAR EL MANDO: Si no lo tenemos, lo buscamos hasta encontrarlo
        if (scanner == null)
        {
            GameObject objetoScanner = GameObject.FindGameObjectWithTag("Scanner");
            if (objetoScanner != null)
            {
                scanner = objetoScanner.transform;
                // Usamos localRotation para ignorar si giras con el joystick
                rotacionAnterior = scanner.localRotation;
            }
            return; // Esperamos al siguiente fotograma si aún no hay mando
        }

        // 2. PAUSA DE SEGURIDAD: Los primeros 2 segundos no medimos nada
        if (tiempoDeGracia > 0)
        {
            tiempoDeGracia -= Time.deltaTime;
            rotacionAnterior = scanner.localRotation; // Mantenemos la rotación al día
            return;
        }

        // 3. MATEMÁTICAS: Calculamos a qué velocidad gira la muñeca real
        float gradosGirados = Quaternion.Angle(rotacionAnterior, scanner.localRotation);
        float velocidadGiro = gradosGirados / Time.deltaTime;

        // Si la muñeca ha girado más rápido que nuestro límite... ¡Aviso!
        if (velocidadGiro > limiteVelocidadGiro)
        {
            MostrarAviso();
        }

        // Guardamos cómo estaba la muñeca para el siguiente milisegundo
        rotacionAnterior = scanner.localRotation;
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
        yaCompletado = true; // Candado cerrado para siempre
    }
    public void EmpezarJuego()
    {
        juegoIniciado = true; // Ponemos el semáforo en verde
    }
}