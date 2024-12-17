using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed; //Velocidad de la plataforma
    [SerializeField] int startingPoint; //Numero para determinar el index del punto de inicio del movimiento 
    [SerializeField] Transform[] points; //Array de puntos de posicion a los que la platforma "perseguirá"
    int i; //index que determina que numero de plataforma se persigue actualmente

    void Start()
    {
        //setear la posicion inicial de la plataforma en uno de los puntos
        transform.position = points[startingPoint].position;
    }

    void Update()
    {
        PlatformMove();
    }

    void PlatformMove()
    {
        //Detector de si la plataforma ha llegado al destino, cambiando el destino
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f)
        {
            i++; //Aumenta en 1 el index, cambia de objetivo
            if (i == points.Length) i = 0;
        }

        //Movimiento: SIEMPRE DESPPUES DE DETENCCION
        //Mueve la plataforma al punto del array que coincide con el valor de i
        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }


}
