using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    public GameObject ObjetoaMover;
    public Transform startPoint;
    public Transform endPoint;

    public float velocidad;
    private Vector3 moverHacia;
    void Start()
    {
        moverHacia = endPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        ObjetoaMover.transform.position = Vector3.MoveTowards(ObjetoaMover.transform.position, moverHacia, velocidad * Time.deltaTime);
        if (ObjetoaMover.transform.position == endPoint.position)
        {
            moverHacia = startPoint.position;
        }

        if (ObjetoaMover.transform.position == startPoint.position)
        {
            moverHacia = endPoint.position;
        }
    }
}
