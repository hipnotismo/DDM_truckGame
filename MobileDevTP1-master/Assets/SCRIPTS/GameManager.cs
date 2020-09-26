using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameManager : MonoBehaviour {
    //public static Player[] Jugadoers;

    public static GameManager Instancia;

    public float TiempoDeJuego = 60;

    public enum EstadoJuego { Calibrando, Jugando, Finalizado }
    public EstadoJuego EstAct = EstadoJuego.Jugando;

    public PlayerInfo PlayerInfo1 = null;
    public PlayerInfo PlayerInfo2 = null;

    public Player Player1;
    public Player Player2;


    [SerializeField] GameObject[] objetosApagarFacil;
    [SerializeField] GameObject[] objetosApagarNormal;
    [SerializeField] GameObject[] objetosApagarDificil;

    [SerializeField] ManagerGameplay mg;
    [SerializeField] PantallaDeCarga pantallaCarga;

    [SerializeField] GameObject[] p2Objects;

    [SerializeField] Camera camp1;
    [SerializeField] Camera camp1Entrega;

    void Awake() {
        GameManager.Instancia = this;
    }

    void Start() {

        //IniciarCalibracion();
        EmpezarCarrera();
        //para testing
        //PosCamionesCarrera[0].x+=100;
        //PosCamionesCarrera[1].x+=100;
        StartCoroutine(Play());
        pantallaCarga = FindObjectOfType<PantallaDeCarga>();
        mg = FindObjectOfType<ManagerGameplay>();

        if(mg!=null)
            switch (mg.GetDificultad()) {
                case ManagerGameplay.Dificultad.Easy:
                    for (int i = 0; i < objetosApagarFacil.Length; i++)
                        if (objetosApagarFacil[i] != null)
                            objetosApagarFacil[i].SetActive(false);
                    break;
                case ManagerGameplay.Dificultad.Normal:
                    for (int i = 0; i < objetosApagarNormal.Length; i++)
                        if (objetosApagarNormal[i] != null)
                            objetosApagarNormal[i].SetActive(false); 
                    break;
                case ManagerGameplay.Dificultad.Dificil:
                    for (int i = 0; i < objetosApagarDificil.Length; i++)
                        if (objetosApagarDificil[i] != null)
                            objetosApagarDificil[i].SetActive(false); 
                    break;
            }

        for (int i = 0; i < p2Objects.Length; i++)
            if (p2Objects[i] != null)
                p2Objects[i].SetActive(false);


       #if UNITY_EDITOR
               for (int i = 0; i < p2Objects.Length; i++)
                   if (p2Objects[i] != null)
                       p2Objects[i].SetActive(true);
       #elif UNITY_ANDROID || UNITY_IOS
               for (int i = 0; i < p2Objects.Length; i++)
                   if (p2Objects[i] != null)
                       p2Objects[i].SetActive(false);

        camp1.rect = new Rect(0f, 0f, 1f, 1f);
        camp1Entrega.rect = new Rect(0f, 0f, 1f, 1f);
       #endif

        
    }

    IEnumerator Play() {
        yield return new WaitForSeconds(TiempoDeJuego);
        FinalizarCarrera();
        if (pantallaCarga != null)
            pantallaCarga.CargarEscena("PtsFinal");
        StopCoroutine(Play());
        yield return null;
    }

    void EmpezarCarrera() {
        Player1.GetComponent<Frenado>().RestaurarVel();
        Player1.GetComponent<ControlDireccion>().Habilitado = true;

        Player2.GetComponent<Frenado>().RestaurarVel();
        Player2.GetComponent<ControlDireccion>().Habilitado = true;
    }

    void FinalizarCarrera() {
        EstAct = GameManager.EstadoJuego.Finalizado;

        TiempoDeJuego = 0;

        if (Player1.Dinero > Player2.Dinero) {
            //lado que gano
            if (PlayerInfo1.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player1.Dinero;
            DatosPartida.PtsPerdedor = Player2.Dinero;
        }
        else {
            //lado que gano
            if (PlayerInfo2.LadoAct == Visualizacion.Lado.Der)
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Der;
            else
                DatosPartida.LadoGanadaor = DatosPartida.Lados.Izq;

            //puntajes
            DatosPartida.PtsGanador = Player2.Dinero;
            DatosPartida.PtsPerdedor = Player1.Dinero;
        }

        Player1.GetComponent<Frenado>().Frenar();
        Player2.GetComponent<Frenado>().Frenar();

        Player1.ContrDesc.FinDelJuego();
        Player2.ContrDesc.FinDelJuego();
    }


    [System.Serializable]
    public class PlayerInfo {
        public PlayerInfo(int tipoDeInput, Player pj) {
            TipoDeInput = tipoDeInput;
            PJ = pj;
        }

        public bool FinCalibrado = false;
        public bool FinTuto1 = false;
        public bool FinTuto2 = false;

        public Visualizacion.Lado LadoAct;

        public int TipoDeInput = -1;

        public Player PJ;
    }

}

