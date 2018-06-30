using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour{
	#region Variables
    Roulette myRoulette;
	public Transform numberButtons;
	public Transform[] textColumns;
	public Transform textLastNumbers, textLastNumbersCanvas;
	public Transform textSpin, textStats1, textStats2, textStats3;
	public Transform textWheelZ1, textWheelZ2, textWheelZ3, textWheelZ4;
	public Transform textWheel2Z1, textWheel2Z2, textWheel2Z3, textWheel2Z4;
	public Transform textAllStats;
	#endregion

    // Use this for initialization
    void Start(){
        myRoulette = new Roulette();
        myRoulette.Start();
		HandleUI();
    }

	#region Handle User Interface
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
		textSpin.GetComponent<Text>().text = "<color=#a7b5d9>Spin\n" + myRoulette.getSpinCount() + "</color>";
		textColumns[0].GetComponent<Text>().text = myRoulette.getTot1stCol() + "";
		textColumns[1].GetComponent<Text>().text = myRoulette.getTot2ndCol() + "";
		textColumns[2].GetComponent<Text>().text = myRoulette.getTot3rdCol() + "";
		textStats1.GetComponent<Text>().text = "<color=#bbc9ec>Odd: " + myRoulette.getTotalOdd() + "\nEven: " + myRoulette.getTotalEven() + "\nD1: " + myRoulette.getTot1stDozen() + "</color>";
		textStats2.GetComponent<Text>().text = "<color=#bbc9ec>Low: " + myRoulette.getTotLow() + "\nHigh: " + myRoulette.getTotHigh() + "\nD2: " + myRoulette.getTo2ndDozen() + "</color>";
		textStats3.GetComponent<Text>().text =  "<color=#e196b0>Red: " + myRoulette.getTotalOfRed() + "</color>\n<color=#dce0ec>Black: " + myRoulette.getTotalOfBlack() + "\n</color><color=#bbc9ec>D3: " + myRoulette.getTot3rdDozen() + "</color>";
		textWheelZ1.GetComponent<Text>().text = myRoulette.getZoneVois() + "";
		textWheelZ2.GetComponent<Text>().text = myRoulette.getZoneOrphelins() + "";
		textWheelZ3.GetComponent<Text>().text = myRoulette.getZoneOrphelins2() + "";
		textWheelZ4.GetComponent<Text>().text = myRoulette.getZoneTiers() + "";
		textWheel2Z1.GetComponent<Text>().text = myRoulette.getZoneVois() + "";
		textWheel2Z2.GetComponent<Text>().text = myRoulette.getZoneOrphelins() + "";
		textWheel2Z3.GetComponent<Text>().text = myRoulette.getZoneOrphelins2() + "";
		textWheel2Z4.GetComponent<Text>().text = myRoulette.getZoneTiers() + "";
		// Update last numbers from main menu
		textLastNumbers.GetComponent<Text>().text = "";
		int x = myRoulette.numberLine.Count;
		if (x > 12){
			x = 12;
		}
		for (int p = x; p>0; p--){
			textLastNumbers.GetComponent<Text>().text = getNumber(myRoulette.numberLine[myRoulette.numberLine.Count-p]) + "      " + textLastNumbers.GetComponent<Text>().text;
		}
		// Update last numbers from new window canvas
		textLastNumbersCanvas.GetComponent<Text>().text = "";
		int z = myRoulette.numberLine.Count;
		if (z > 156){
			z = 156;
		}

		for (int t = z; t>0; t--){
			textLastNumbersCanvas.GetComponent<Text>().text = getNumber(myRoulette.numberLine[myRoulette.numberLine.Count-t]) + "      " + textLastNumbersCanvas.GetComponent<Text>().text;
		}
	}
	#endregion

	#region Helper Functions
	public void addNumberToRoulette(int i){
		myRoulette.setNumber(i);
		HandleUI();
	}

	public void removeLastNumber(){
		myRoulette.undoLastNumber();
		HandleUI();
	}

	public string getNumber(int _number){
		if(myRoulette.getNumberColor(_number) == 0){
			if(_number > 9){
				return "<color=#19a4a5>" + _number + "</color>";
			}
			else{
				return "<color=#19a4a5>" + _number + "   </color>";
			}
		}
		else if(myRoulette.getNumberColor(_number) == 1){
			if (_number > 9){
				return "<color=#e196b0>" + _number + "</color>";
			} else {
				return "<color=#e196b0>" + _number + "  </color>";
			}
		}
		else{
			if(_number > 9){
				return "<color=#dce0ec>" + _number + "</color>";
			}
			else{
				return "<color=#dce0ec>" + _number + "  </color>";
			}
		}
	}// end of the getNumber
	#endregion
}
