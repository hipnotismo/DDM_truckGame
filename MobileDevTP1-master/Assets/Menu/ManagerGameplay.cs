using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerGameplay : MonoBehaviour {

    static ManagerGameplay instancia { get; set; }
    public enum Dificultad {
        Easy,
        Normal,
        Dificil
    }
    Dificultad dificultad;
    private void Awake() {
        if (instancia != null) {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
        instancia = this;
    }
    public void SetDificultad(Dificultad d) {
        dificultad = d;
    }
    public Dificultad GetDificultad() {
        return dificultad;
    }

}
