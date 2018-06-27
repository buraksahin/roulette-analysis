using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    Roulette myRoulette;
	public Transform numberButtons;

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
		HandleUI();
    }

    public void GenerateRandomBet(){
        myRoulette.GenerateRandomNumber();
    }

	public void HandleUI(){
		for (int i = 0; i < 37; i++){
			numberButtons.GetChild(i).GetChild(2).GetComponent<Text>().text = myRoulette.numbers[i].getRank() + "";
		}
	}
}
