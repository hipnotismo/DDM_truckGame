using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiMenu : MonoBehaviour{
    
   public void Start(){
        Debug.Log("SinglePlayer");

   }

    public void MultiPlayer(){

        Debug.Log("MultiPlayer");

    }

    public void Credits(){

        Debug.Log("Credits" );
    }

    public void Quit(){

        Debug.Log("Quit");
        Application.Quit();
    }
}