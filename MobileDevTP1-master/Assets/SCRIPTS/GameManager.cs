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

    //mueve los esqueletos para usar siempre los mismos
    public Transform Esqueleto1;
    public Transform Esqueleto2;
    //public Vector3[] PosEsqsCalib;
    public Vector3[] PosEsqsCarrera;

    bool PosSeteada = false;

    bool ConteoRedresivo = true;
    public Rect ConteoPosEsc;
    public float ConteoParaInicion = 3;
    public GUISkin GS_ConteoInicio;

    public Rect TiempoGUI = new Rect();
    public GUISkin GS_TiempoGUI;
    Rect R = new Rect();

    public float TiempEspMuestraPts = 3;

    //posiciones de los camiones dependientes del lado que les toco en la pantalla
    //la pos 0 es para la izquierda y la 1 para la derecha
    public Vector3[] PosCamionesCarrera = new Vector3[2];
    //posiciones de los camiones para el tutorial
    public Vector3 PosCamion1Tuto = Vector3.zero;
    public Vector3 PosCamion2Tuto = Vector3.zero;

    //listas de GO que activa y desactiva por sub-escena
    //escena de calibracion
    public GameObject[] ObjsCalibracion1;
    public GameObject[] ObjsCalibracion2;
    //escena de tutorial
    public GameObject[] ObjsTuto1;
    public GameObject[] ObjsTuto2;
    //la pista de carreras
    public GameObject[] ObjsCarrera;
    //de las descargas se encarga el controlador de descargas

    //para saber que el los ultimos 5 o 10 segs se cambie de tamaño la font del tiempo
    //bool SeteadoNuevaFontSize = false;
    //int TamOrigFont = 75;
    //int TamNuevoFont = 75;

    /*
	//para el testing
	public float DistanciaRecorrida = 0;
	public float TiempoTranscurrido = 0;
	*/

    IList<int> users;

    //--------------------------------------------------------//
    [SerializeField] PantallaDeCarga pantallaCarga;
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
    }

    IEnumerator Play() {
        yield return new WaitForSeconds(TiempoDeJuego);
        FinalizarCarrera();
        if (pantallaCarga != null)
            pantallaCarga.CargarEscena("PtsFinal");
        StopCoroutine(Play());
        yield return null;
    }

    void OnGUI() {
        switch (EstAct) {
            case EstadoJuego.Jugando:
                if (ConteoRedresivo) {
                    GUI.skin = GS_ConteoInicio;

                    R.x = ConteoPosEsc.x * Screen.width / 100;
                    R.y = ConteoPosEsc.y * Screen.height / 100;
                    R.width = ConteoPosEsc.width * Screen.width / 100;
                    R.height = ConteoPosEsc.height * Screen.height / 100;

                    if (ConteoParaInicion > 1) {
                        GUI.Box(R, ConteoParaInicion.ToString("0"));
                    }
                    else {
                        GUI.Box(R, "GO");
                    }
                }

                GUI.skin = GS_TiempoGUI;
                R.x = TiempoGUI.x * Screen.width / 100;
                R.y = TiempoGUI.y * Screen.height / 100;
                R.width = TiempoGUI.width * Screen.width / 100;
                R.height = TiempoGUI.height * Screen.height / 100;
                GUI.Box(R, TiempoDeJuego.ToString("00"));
                break;
        }

        GUI.skin = null;
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

