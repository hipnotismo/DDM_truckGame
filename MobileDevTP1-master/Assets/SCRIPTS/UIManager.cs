﻿using System.Collections;
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

    [SerializeField] TextMeshProUGUI plataIzqDesc;
    [SerializeField] TextMeshProUGUI plataDerDesc;

    [SerializeField] GameObject gameDerCosas;
    [SerializeField] GameObject descDerCosas;

    [SerializeField] GameObject[] botones;
    [SerializeField] GameObject[] sticks;
    public enum Lado {
        Izq,
        Der
    }

    private void Start() {
        for (int i = 0; i < UIGame.Length; i++)
            UIGame[i].SetActive(true);
        for (int i = 0; i < UIDescarga.Length; i++)
            UIDescarga[i].SetActive(false);

#if UNITY_EDITOR
        spriteDer.gameObject.SetActive(true);
        plataDerDesc.gameObject.SetActive(true);
        plataDer.gameObject.SetActive(true);

        for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);


        sticks[0].SetActive(false);
        sticks[1].SetActive(false);

#elif UNITY_ANDROID || UNITY_IOS
              spriteDer.gameObject.SetActive(false);
               plataDerDesc.gameObject.SetActive(false);
               plataDer.gameObject.SetActive(false);

     for (int i = 0; i < botones.Length; i++)
            if (botones[i] != null)
                botones[i].SetActive(false);

        botones[0].SetActive(true);

        sticks[0].SetActive(true);
        sticks[1].SetActive(false);

#endif





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
        if (l == (int)Lado.Izq) {
            plataIzq.text = "$: " + (p / 1000f).ToString("F2");
            plataIzqDesc.text = "$: " + (p / 1000f).ToString("F2");
        }
        else if (l == (int)Lado.Der) {
            plataDer.text = "$: " + (p / 1000f).ToString("F2");
            plataDerDesc.text = "$: " + (p / 1000f).ToString("F2");
        }
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
