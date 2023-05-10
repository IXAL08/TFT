using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class VanishPlatform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D _platform;
    [SerializeField] private SpriteRenderer colorPlataforma;
    private bool iniciador = true;


    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") && iniciador)
        {
            StartCoroutine(Desaparecer());
        }

        
    }

    private IEnumerator Desaparecer()
    {
        iniciador = false;
        colorPlataforma.color = Color.green;
        yield return new WaitForSeconds(0.8f);
        colorPlataforma.color = Color.yellow;
        yield return new WaitForSeconds(0.8f);
        colorPlataforma.color = Color.red;
        yield return new WaitForSeconds(0.8f);
        _platform.enabled = !_platform.enabled;
        colorPlataforma.enabled = !colorPlataforma.enabled;
        yield return new WaitForSeconds(5f);
        colorPlataforma.color = Color.green;
        colorPlataforma.enabled = true;
        _platform.enabled = true;
        iniciador = true;
    }

}
