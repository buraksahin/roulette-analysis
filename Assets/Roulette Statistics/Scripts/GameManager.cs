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
		HandleUI();
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
		for (int i = 0; i < 37; i++){
			float tempProb = myRoulette.numbers[i].getProbability();
			numberButtons.GetChild(i).GetChild(1).GetComponent<Text>().text = tempProb + "";

			if (tempProb >= 0.2){
				numberButtons.GetChild (i).GetChild (3).GetComponent<Image>().enabled = true;
			}
			else {
				numberButtons.GetChild (i).GetChild (3).GetComponent<Image>().enabled = false;
			}
		}
	}
}