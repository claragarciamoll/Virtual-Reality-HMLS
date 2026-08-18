using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GestorVideoAviso : MonoBehaviour
{
    [Header("Componentes del Aviso")]
    public GameObject canvasAvisoCompleto;
    public GameObject botonEmpezarVideo;
    public RawImage pantallaVideo;
    public VideoPlayer reproductorVideo;

    [Header("Conexión con tu Menú Principal")]
    [Tooltip("Arrastra aquí tu Canvas Inicial de siempre")]
    public GameObject canvasInicial;

    void Start()
    {
        // 1. Por seguridad, apagamos tu Canvas Inicial normal para que no se superpongan
        if (canvasInicial != null)
        {
            canvasInicial.SetActive(false);
        }

        // 2. Preparamos la pantalla del vídeo
        botonEmpezarVideo.SetActive(true);
        pantallaVideo.gameObject.SetActive(false);
        canvasAvisoCompleto.SetActive(true);

        // 3. Le decimos a Unity qué debe hacer cuando el vídeo termine
        reproductorVideo.loopPointReached += AlTerminarElVideo;
    }

    // Esta función va en el OnClick del botón de este Canvas
    public void ReproducirVideo()
    {
        botonEmpezarVideo.SetActive(false);
        pantallaVideo.gameObject.SetActive(true);
        reproductorVideo.Play();
    }

    // Esta función se dispara SOLA al acabar el vídeo
    private void AlTerminarElVideo(VideoPlayer vp)
    {
        // 1. Apagamos el aviso de epilepsia por completo
        canvasAvisoCompleto.SetActive(false);

        // 2. ¡Encendemos tu Canvas Inicial normal!
        if (canvasInicial != null)
        {
            canvasInicial.SetActive(true);
        }
    }
}