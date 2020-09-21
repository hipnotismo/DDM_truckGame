using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSelecciones : MonoBehaviour {

    ManagerGameplay managerGameplay;
    [SerializeField] GameObject buttonsMode;
    [SerializeField] GameObject buttonsDificultad;
    [SerializeField] PantallaDeCarga pantallaCarga;
    void Start() {
        managerGameplay = FindObjectOfType<ManagerGameplay>();
    }

    public void TocarBotonPlay() {
        buttonsDificultad.SetActive(true);
        buttonsMode.SetActive(false);
    }

    public void TocadoBotonDificultad(string btn) {
        switch (btn) {
            case "easy":
                managerGameplay.SetDificultad(ManagerGameplay.Dificultad.Easy);
                break;
            case "normal":
                managerGameplay.SetDificultad(ManagerGameplay.Dificultad.Normal);
                break;
            case "dificil":
                managerGameplay.SetDificultad(ManagerGameplay.Dificultad.Dificil);
                break;
            case "back":
                buttonsDificultad.SetActive(false);
                buttonsMode.SetActive(true);
                break;
            default:
                Debug.LogError("boton invalido");
                break;
        }
        if (btn != "back")
            pantallaCarga.CargarEscena("tutorial");
    }
}
