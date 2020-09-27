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

    [SerializeField] GameObject[] botones;
    [SerializeField] Camera camP1;
    [SerializeField] GameObject Escena2;
    void Start() {
        principalCamera.SetActive(false);
        pc = FindObjectOfType<PantallaDeCarga>();
        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);
#if UNITY_EDITOR 
#elif UNITY_ANDROID || UNITY_IOS
        botones[0].SetActive(true);
        camP1.rect = new Rect(0, 0, 1, 1);
        Escena2.SetActive(false);
#endif

    }

    // Update is called once per frame
    void Update() {

#if UNITY_EDITOR
        if (p1.GetTutorialTerminado() && p2.GetTutorialTerminado() && !cambiandoEscena) {
            cambiandoEscena = true;
            principalCamera.SetActive(true);
            cameras[0].SetActive(false);
            cameras[1].SetActive(false);
            pc.CargarEscena("conduccion9");
        }
#elif UNITY_ANDROID || UNITY_IOS
       if (p1.GetTutorialTerminado() && !cambiandoEscena) {
            cambiandoEscena = true;
            principalCamera.SetActive(true);
            cameras[0].SetActive(false);
            pc.CargarEscena("conduccion9");
        }
#endif


    }
}
