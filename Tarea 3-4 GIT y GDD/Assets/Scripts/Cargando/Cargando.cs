using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Cargando : MonoBehaviour
{
    public TMP_Text texto;

    private void Start()
    {
        string nivelACargar = PantallaCarga.siguienteNivel;
        StartCoroutine(IniciarCarga(nivelACargar));
    }

    IEnumerator IniciarCarga(string nivel)
    {
        AsyncOperation operacion = SceneManager.LoadSceneAsync(nivel);
        operacion.allowSceneActivation = false;

        while(!operacion.isDone)
        {
            if(operacion.progress >= 0.9f)
            {
                texto.text = "Presiona una tecla para continuar...";
                if(Input.anyKey)
                {
                    operacion.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
