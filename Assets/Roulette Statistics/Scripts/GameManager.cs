using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
    Roulette myRoulette;
	public Transform numberButtons;
	public Transform[] textColumns;
	public Transform textLastNumbers;
	public Transform textSpin, textStats1, textStats2, textStats3;
	public Transform textWheelZ1, textWheelZ2, textWheelZ3, textWheelZ4;
	public Transform textWheel2Z1, textWheel2Z2, textWheel2Z3, textWheel2Z4;
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

	public void HandleUI(){
		// Update number ranks
		for (int i=0; i<37; i++){
			numberButtons.GetChild(i).GetChild(2).transform.GetComponent<Text>().text = myRoulette.numbers[i].getRank() + "";
		}
		// Update number probabilities
		for (int i=0; i<37; i++){
			float tempProb = myRoulette.numbers[i].getProbability();
			numberButtons.GetChild(i).GetChild(1).GetComponent<Text>().text = tempProb + "";

			if (tempProb >= 0.2){
				numberButtons.GetChild(i).GetChild(3).GetComponent<Image>().enabled = true;
			}
			else{
				numberButtons.GetChild(i).GetChild(3).GetComponent<Image>().enabled = false;
			}
		}
		// Update informations
		textSpin.GetComponent<Text>().text = "Spin\n" + myRoulette.getSpinCount();
		textColumns[0].GetComponent<Text>().text = myRoulette.getTot1stCol() + "";
		textColumns[1].GetComponent<Text>().text = myRoulette.getTot2ndCol() + "";
		textColumns[2].GetComponent<Text>().text = myRoulette.getTot3rdCol() + "";
		textStats1.GetComponent<Text>().text = "Odd: " + myRoulette.getTotalOdd() + "\nEven: " + myRoulette.getTotalEven() + "\nD1: " + myRoulette.getTot1stDozen();
		textStats2.GetComponent<Text>().text = "Low: " + myRoulette.getTotLow() + "\nHigh: " + myRoulette.getTotHigh() + "\nD2: " + myRoulette.getTo2ndDozen();
		textStats3.GetComponent<Text>().text =  "Red: " + myRoulette.getTotalOfRed() + "\nBlack: " + myRoulette.getTotalOfBlack() + "\nD3: " + myRoulette.getTot3rdDozen();
		textWheelZ1.GetComponent<Text>().text = myRoulette.getZoneVois() + "";
		textWheelZ2.GetComponent<Text>().text = myRoulette.getZoneOrphelins() + "";
		textWheelZ3.GetComponent<Text>().text = myRoulette.getZoneOrphelins2() + "";
		textWheelZ4.GetComponent<Text>().text = myRoulette.getZoneTiers() + "";
		textWheel2Z1.GetComponent<Text>().text = myRoulette.getZoneVois() + "";
		textWheel2Z2.GetComponent<Text>().text = myRoulette.getZoneOrphelins() + "";
		textWheel2Z3.GetComponent<Text>().text = myRoulette.getZoneOrphelins2() + "";
		textWheel2Z4.GetComponent<Text>().text = myRoulette.getZoneTiers() + "";
	}
}
