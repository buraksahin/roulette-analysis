using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    Roulette myRoulette;
    // Use this for initialization
    void Start(){
        myRoulette = new Roulette();
        myRoulette.Start();
    }
	
	// Update is called once per frame
	void Update(){
		
	}

    public void addNumberToRoulette(int i){
        myRoulette.SetNumber(i);
    }

    public void GenerateRandomBet(){
        myRoulette.GenerateRandomNumber();
    }
}
