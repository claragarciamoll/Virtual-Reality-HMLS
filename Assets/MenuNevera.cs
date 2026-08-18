using UnityEngine;

public class MenuNevera : MonoBehaviour
{
    public GameObject panelPrincipal; // Aquí arrastraremos tu "Panel Principal"
    public GameObject panelInfo;      // Aquí arrastraremos tu "Panel Info"
    public GameObject canvasEntero;   // Aquí arrastraremos el Canvas completo

    // Esta función se activará al pulsar "Accept"
    public void PulsarAccept()
    {
        canvasEntero.SetActive(false); // Apaga todo el cartel
    }

    // Esta función se activará al pulsar "More Info"
    public void PulsarMoreInfo()
    {
        panelPrincipal.SetActive(false); // Oculta los botones
        panelInfo.SetActive(true);       // Muestra el texto largo
    }

    // Esta función se activará al pulsar "Back"
    public void PulsarBack()
    {
        panelInfo.SetActive(false);      // Oculta el texto largo
        panelPrincipal.SetActive(true);  // Vuelve a mostrar los botones
    }
}