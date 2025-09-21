using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickManager : MonoBehaviour
{
    // 1. Un espacio para arrastrar nuestro botón
    public Button clickButton;

    void Start()
    {
        // 2. Añade un "listener" al botón para que llame a nuestro método
        clickButton.onClick.AddListener(HandleClick);
    }

    private void HandleClick()
    {
        // 3. Llama al Singleton para que haga su trabajo
        ScoreManager.Instance.AddPoints(1);
        
        // ¡Extra! Usemos el Logger que creamos
        // Logger.Instance.Log("Botón presionado. +1 punto.");
    }
}
