using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Referencia a la escena de carga
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    void Update()
    {
        // Detectar si cualquier tecla ha sido presionada
        if (Input.anyKeyDown)
        {
            // Cargar la escena del menú principal
            SceneManager.LoadScene(mainMenuSceneName);
        }
    }
}
