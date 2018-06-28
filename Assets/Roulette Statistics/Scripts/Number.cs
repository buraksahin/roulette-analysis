using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number
{
    /*
     * Variables 
     */
    // Number Properties
    private int number;					// Number Value
    private int color;					// 0: green 1:red 2: black
    private int numberPosition;			// Number position on the wheel
    private int numberColumn;			// Number Row Value
    private int numberDozen;			// Number Dozen Value
    private int numberZone;				// Number Zone
    private int numberPropertiesValue;	// Keep number properties as a calculated value

    // Number Statistics and Analysis
    private int rank;                             // Total number of counts
    private int repeatCount;                      // Repeat Count
    private float probability = 0.0f;             // Probability of number
    private int[] next = new int[37];             // Previous number list
    private int[] prev = new int[37];             // Next number list
    private List<int> spinLine = new List<int>(); // Spin Line


    #region Main Functions
    // Constructor
    public Number(int _number, int _color, int _numberPosition, int _numberColumn, int _nubmerDozen, int _numberZone){
        number = _number;
        color = _color;
        numberPosition = _numberPosition;
        numberColumn = _numberColumn;
        numberDozen = _nubmerDozen;
        numberZone = _numberZone;
    }
    #endregion

    #region Getters and Setters
    // Getters
    public int getColor() { return color; }
    public int getPosition() { return numberPosition; }
    public int getNumberColumn() { return numberColumn; }
    public int getNumberDozen() { return numberDozen; }
    public int getNumberZone() { return numberZone; }
    public int getNumberPropertiesValue() { return numberPropertiesValue; }
    public int getRank() { return rank; }
    public int getRepeatCount() { return repeatCount; }
    public float getProbability() { return probability; }
    public int getNextNumber(int i) { return next[i]; }
    public int getPrevNumber(int i) { return prev[i]; }
    
    // Return Next Number
    public int getMaxNextNumber(){
        if (rank > 1){
            int tempMax = 0;
            for(int i=0; i<next.Length; i++){
                if(tempMax <= next[i]){
                    tempMax = i;
                }
            }
            return tempMax;
        }
        else { return 37; } // if doesn't exist any next number return 37 as error
    }

    // Return Next Number
    public int getMaxPrevNumber(){
        if(rank == 0) { return 37; } // if doesn't exist any next number return 37 as error
        else{
            int tempMax = 0;
            for (int i = 0; i < prev.Length; i++)
            {
                if (tempMax <= prev[i])
                {
                    tempMax = i;
                }
            }
            return tempMax;
        } 
    }

    // Setters
    public void IncreaseRank() { rank++; }
    public void IncreaserepeatCount() { repeatCount++; }
    public void setProbability(float prob) { probability = prob; }
    public void increaseNext(int i) { next[i] = next[i] + 1; }
    public void increasePrev(int i) { prev[i] = prev[i] + 1; }
    public void setSpinLine(int line) { spinLine.Add(line); }

    /*
    * Calculate Number Properties Value
    * Calculate a number in 4 base [Zone, Color, Odd:1 or Even:0, Row, Dozen] -> Ex: 31101(base 4) Max Value
    * Calculate as decimal and add wheel position of the number
    */
    public void CalculatePropertiesValue(){
        int tempNumber = 0;
        tempNumber = numberDozen + tempNumber;
        tempNumber = 4 * numberColumn + tempNumber;
        if(number % 2 != 0){
            tempNumber = 16 + tempNumber;
        }
        tempNumber = 64 * color + tempNumber;
        tempNumber = 256 * numberZone + tempNumber;
        tempNumber = numberPosition + tempNumber;
        numberPropertiesValue = tempNumber;
        // Debug.Log(number + " ->  " + numberZone + " " + color +  " " + number % 2  + " " + numberRow + " "+numberDozen);
    }
    #endregion
}
