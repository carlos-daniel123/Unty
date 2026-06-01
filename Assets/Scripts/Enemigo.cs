using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Estadísticas Base")]
    [SerializeField] private float vidaMaxima = 100f;
    [SerializeField] private float velocidad = 5f;
    [SerializeField] private int recompensaOro = 10;

    public enum TipoElemento { Ninguno, Fuego, Veneno, Hielo }

    [Header("Atributos Elementales")]
    [SerializeField] private TipoElemento resistenciaA = TipoElemento.Ninguno;
    [SerializeField] [Range(0f, 1f)] private float porcentajeResistencia = 0.5f;

    private float vidaActual;
    private Vector3 puntoObjetivo;
    private int indiceWaypoint = 0;
    private bool rutaAsignada = false;

    void Start()
    {
        Debug.Log("1. El script Enemigo ha iniciado correctamente en el objeto: " + gameObject.name);
        vidaActual = vidaMaxima;
        IntentarAsignarRuta();
    }

    void Update()
    {
        if (!rutaAsignada)
        {
            IntentarAsignarRuta();
            return; 
        }

        MoverEnemigo();
    }

public void RecibirDaño(float daño)
{
    vidaActual -= daño;

    Debug.Log(gameObject.name + " recibió daño: " + daño + " | Vida restante: " + vidaActual);

    if (vidaActual <= 0)
    {
        Morir();
    }
}

private void Morir()
{
    Debug.Log("Enemigo eliminado. Recompensa: " + recompensaOro);
    Destroy(gameObject);
}

    private void IntentarAsignarRuta()
    {
        if (MovimientoEnemigos.puntos == null)
        {
            Debug.LogWarning("ALERTA: MovimientoEnemigos.puntos es NULL. El script de la ruta no ha guardado nada.");
            return;
        }

        if (MovimientoEnemigos.puntos.Length == 0)
        {
            Debug.LogWarning("ALERTA: El arreglo de puntos está vacío. ¿El objeto Ruta no tiene hijos?");
            return;
        }

        if (!rutaAsignada)
        {
            puntoObjetivo = MovimientoEnemigos.puntos[0];
            rutaAsignada = true;
            Debug.Log("2. ¡Ruta asignada con éxito! El primer punto está en la posición: " + puntoObjetivo);
        }
    }

    private void MoverEnemigo()
    {
        Vector3 direccion = puntoObjetivo - transform.position;
        
        transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);

        if (direccion != Vector3.zero)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 10f);
        }

        if (Vector3.Distance(transform.position, puntoObjetivo) <= 0.2f)
        {
            ObtenerSiguienteWaypoint();
        }
    }

    private void ObtenerSiguienteWaypoint()
    {
        if (indiceWaypoint >= MovimientoEnemigos.puntos.Length - 1)
        {
            Debug.Log("4. ¡El enemigo llegó al último punto y se destruye!");
            Destroy(gameObject);
            return;
        }

        indiceWaypoint++;
        puntoObjetivo = MovimientoEnemigos.puntos[indiceWaypoint];
        Debug.Log("3. Enemigo llegó a un punto. Avanzando al punto índice: " + indiceWaypoint);
    }
}
