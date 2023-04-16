using System;
using UnityEngine;

public class Scenes : MonoBehaviour
{
    [SerializeField] private Transform player;

    private float cameraSize, alturaPantalla;
    
    private void Start()
    {
        cameraSize = Camera.main.orthographicSize;
        alturaPantalla = cameraSize * 2;
    }

    private void Update()
    {
        CalcularPositionCamera();
    }

    void CalcularPositionCamera()
    {
        int pantallaPersonaje = (int)(player.position.y / alturaPantalla);
        float alturacamara = (pantallaPersonaje * alturaPantalla) + cameraSize;

        transform.position = new Vector3(transform.position.x, alturacamara, transform.position.z);
    }
}
