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
        yield return new WaitForSeconds(2.0f);
        _platform.enabled = !_platform.enabled;
        colorPlataforma.enabled = !colorPlataforma.enabled;
        yield return new WaitForSeconds(5f);
        colorPlataforma.enabled = true;
        _platform.enabled = true;
        iniciador = true;
    }

}
