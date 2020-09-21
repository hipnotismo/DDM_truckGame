using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour {

    [SerializeField] ControllerTutorial p1;
    [SerializeField] ControllerTutorial p2;
    [SerializeField] PantallaDeCarga pc;
    bool cambiandoEscena;
    [SerializeField] GameObject[] cameras;
    [SerializeField] GameObject principalCamera;
    void Start() {
        principalCamera.SetActive(false);
        pc = FindObjectOfType<PantallaDeCarga>();
    }

    // Update is called once per frame
    void Update() {
        if (p1.GetTutorialTerminado() && p2.GetTutorialTerminado() && !cambiandoEscena) {
            cambiandoEscena = true;
            principalCamera.SetActive(true);
            cameras[0].SetActive(false);
            cameras[1].SetActive(false);
            pc.CargarEscena("conduccion9");
        }
    }
}
