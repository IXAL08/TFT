using UnityEngine;

public class Scenes : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject CambiodeNivel;
 

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            _camera.transform.position = new Vector3(0, (_camera.transform.position.y + 10),-10);
            CambiodeNivel.SetActive(false);

        }
        
    }
}
