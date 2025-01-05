using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PantallaCarga
{
    public static string siguienteNivel;

    public static void NivelCargado(string nombre)
    {
        siguienteNivel = nombre;
        SceneManager.LoadScene("PantallaCarga");
    }

}
