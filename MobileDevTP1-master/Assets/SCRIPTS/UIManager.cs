using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {
    [SerializeField] Sprite[] spritesCargaIzq;
    [SerializeField] Sprite[] spritesCargaDer;
    [SerializeField] Image spriteIzq;
    [SerializeField] Image spriteDer;

    [SerializeField] GameObject[] UIGame;
    [SerializeField] GameObject[] UIDescarga;

    [SerializeField] TextMeshProUGUI plataIzq;
    [SerializeField] TextMeshProUGUI plataDer;

    public enum Lado {
        Izq,
        Der
    }

    private void Start() {
        Player.CambiadaPlata += PlataCambio;
        Player.DescargaSalida += SalidaDescarga;
        Player.DescargaEntrada += EntradaDescarga;
        Player.AgarradaBolsa += CambiarSprite;
    }
    private void OnDestroy() {
        Player.CambiadaPlata -= PlataCambio;
        Player.DescargaSalida -= SalidaDescarga;
        Player.DescargaEntrada -= EntradaDescarga;
        Player.AgarradaBolsa -= CambiarSprite;
    }

    void PlataCambio(int l, float p) {
        if (l == (int)Lado.Izq)
            plataIzq.text = "$: " + (p / 1000f).ToString("F2");
        else if (l == (int)Lado.Der)
            plataDer.text = "$: " + (p / 1000f).ToString("F2");
    }

    void EntradaDescarga(int l) {
        UIGame[l].SetActive(false);
        UIDescarga[l].SetActive(true);
    }

    void SalidaDescarga(int l) {
        UIDescarga[l].SetActive(false);
        UIGame[l].SetActive(true);
    }

    void CambiarSprite(int l, int sprite) {
        if (l == (int)Lado.Izq)
            spriteIzq.sprite = spritesCargaIzq[sprite];
        else if (l == (int)Lado.Der)
            spriteDer.sprite = spritesCargaDer[sprite];
    }

}
