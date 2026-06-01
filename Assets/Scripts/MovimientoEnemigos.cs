using UnityEngine;

public class MovimientoEnemigos : MonoBehaviour
{
    public static Vector3[] puntos;

    void Awake()
    {
        puntos = new Vector3[transform.childCount];

        for (int i = 0; i < puntos.Length; i++)
        {
            // Usamos la posición exacta del objeto ignorando su tamaño o escala
            puntos[i] = transform.GetChild(i).position;
        }
    }
}
