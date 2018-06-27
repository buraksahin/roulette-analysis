using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roulette{
    #region Variables
    /* 
     * Variables
     * We can access all statistics easily
     */
    // Numbers and Roulette Variables
    int[] redNumbers = { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 }; // Red Numbers
    int[] blackNumbers = { 2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35 }; // Black Numbers
    int[] numberPositions = { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27,
                              13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1,
                              20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 }; // Clockwise Numbers on the wheel

    // Number zones on the wheel
    int[] zone1V = { 22, 18, 29, 7, 28, 12, 35, 3, 26, 0, 32, 15, 19, 4, 21, 2, 25 };    // Zone 1 VoisinsduZero
    int[] zone2O = { 17, 35, 6, 1, 20, 14, 31, 9 };                                      // Zone 2 Orphelins
    int[] zone3T = { 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33 };                     // Zone 3 TiersduCylindre

    // Stats
    private int spinCount = 0;                                          // Spin Count
    private int totalOfRed = 0, totalOfBlack = 0, totalOfGreen = 0;     // Total of Colors
    private int totalOfOdd = 0, totalOfEven = 0;                        // Total of Odd and Even
    private int tot1stDozen = 0, tot2ndDozen = 0, tot3rdDozen;          // Total of Dozens
    private int tot1stRow = 0, tot2ndRow = 0, tot3rdRow = 0;            // Total of Rows
    private int totalOf1to18 = 0, totalOf19to36 = 0;                    // Total of Range
    private int totalZoneVoi = 0, totalZoneOrp = 0, totalZoneTier = 0;  // Total of Zones

    // Repeats
    private int repeatRed = 0, repeatBlack = 0, repeatGreen = 0;            // Total of Color Repeats
    private int repeatOdd = 0, repeatEven = 0;                              // Total of Number Repeats
    private int repeat1stDozen = 0, repeat2ndDozen = 0, repeat3rdDozen = 0; // Repeat of Blocks
    private int repeat1stRow = 0, repeat2ndRow = 0, repeat3rdRow = 0;       // Repeat of Rows
    private int repeat1to18 = 0, repeat19to36 = 0;                          // Repeat of Range
    private int repeatZoneVoi = 0, repeatZoneOrp = 0, repeatZoneTier = 0;   // Repeat of Zones

    // Line and Number Lists
    public List<Number> numbers = new List<Number>(); // Keep the numbers
    public List<int> numberLine = new List<int>();    // Number Line

    // Statistics and Analysis
    float predictionCorrectness = 0.0f; // Prediction Correctness in range of [0, 1]
    int predictedNumber;                // Predicted Number
    int randomCorrectnessCount;         // Random Number Generate
    int[] randomCorrectnessGraph = new int[1000]; // Keep Correctness Map for Random Generating

    #endregion

    #region Main Functions
    // Use this for initialization
    public void Start()
    {
        // Set Numbers
        for (int i = 0; i < 37; i++){
            numbers.Add( new Number( i, getNumberColor(i), getNumberPosition(i), getNumberRowPosition(i), getNumberDozen(i), getNumberZone(i)) );
            numbers[i].CalculatePropertiesValue();
        }
    }
    #endregion

    #region Helper Functions
    /*
     * Return Number Color
     * 0:Green - 1:Red - 2:Black
     */
    int getNumberColor(int _number){
        int tempColor = 0;
        if (_number == 0){
            return tempColor;
        }
        else{
            tempColor = 2; // Set ball as Black and check reds
            for(int i=0; i<redNumbers.Length; i++){
                if(_number == redNumbers[i]){
                    tempColor = 1;
                    break;
                }
            }
            return tempColor;
        }
    }

    /*
     * Return number position
     */
    int getNumberPosition(int _number){
        int position = 0;
        for(int i=0; i< numberPositions.Length; i++)
        {
            if(_number == numberPositions[i])
            {
                position = i;
                break;
            }
        }
        return position;
    }

    /*
     * Check Number Row Position
     */
    int getNumberRowPosition(int _number){
        int rowPosition = 0;
        if (_number % 3 == 0) { rowPosition = 1; }
        if (_number % 3 == 2) { rowPosition = 2; }
        if (_number % 3 == 1) { rowPosition = 3; }
        return rowPosition;
    }

    /*
     * Get Number Dozen
     */
    int getNumberDozen(int _number){
        int dozenPos = 0;
        if (_number > 0  && _number < 13) { dozenPos = 1; }
        else if (_number > 12 && _number < 25) { dozenPos = 2; }
        else if (_number > 24) { dozenPos = 3; }
        return dozenPos;
    }

    /*
    * Get Number Zone on the Roulette Wheel
    * 0:VoisinsduZero 1:Orphelins 2:Tiers
    */
    int getNumberZone(int _number){
        int tempZone = 0;
        for (int i = 0; i < zone1V.Length; i++)
        {
            if (_number == zone1V[i])
            {
                tempZone = 1;
                break;
            }
        }
        for (int i = 0; i < zone2O.Length; i++)
        {
            if (_number == zone2O[i])
            {
                tempZone = 2;
                break;
            }
        }
        for (int i = 0; i < zone3T.Length; i++)
        {
            if (_number == zone3T[i])
            {
                tempZone = 3;
                break;
            }
        }
        return tempZone;
    }


    // Set Numbers
    public void SetNumber(int _number){
        spinCount++; // Increase spin count
        numbers[_number].IncreaseRank(); // Increase number count
        numbers[_number].setSpinLine(spinCount);
        numberLine.Add(_number);
        if (spinCount > 1)
        {
            numbers[_number].increasePrev(numberLine[spinCount - 2]); // Increase current number's prev
            numbers[numberLine[spinCount - 2]].increaseNext(_number);             // Increase next of previous number
            if(numberLine[spinCount - 1] == _number) // if previous number and current number is same
            {
                numbers[_number].IncreaserepeatCount(); // Increase repeat count of the number
            }
        }
    }

    // Calculate Probabilities
    public void CalculateProbabilities(int num){
    
    }

    /*
     * Generate Random Numbers
     */
     public void GenerateRandomNumber(){    

    }

    /*
     * Calculate Average Prediction Count
     */
     int CalculatePredictionCount(){
        return 0;
    }
    #endregion

    #region Getters and Setters
    /*
     * Get Statistics 
     */
     // Total Spin Count and Colors
    public int getSpinCount() { return spinCount; }
    public int getTotalOfRed() { return totalOfRed; }
    public int getTotalOfBlack() { return totalOfBlack; }
    public int getTotalOfGreen() { return totalOfGreen; }

    // Total of Odd and Even
    public int getTotalOdd() { return totalOfOdd; }
    public int getTotalEven() { return totalOfEven; }

    // Total of Dozen
    public int getTot1stDozen() { return tot1stDozen; }
    public int getTo2ndDozen() { return tot2ndDozen; }
    public int getTot3rdDozen() { return tot3rdDozen; }

    // Total of Rows
    public int getTot1stRow() { return tot1stRow; }
    public int getTot2ndRow() { return tot2ndRow; }
    public int getTot3rdRow() { return tot3rdRow; }

    // Total of Range
    public int getTotOf1to18() { return totalOf1to18; }
    public int getTotOf19to36() { return totalOf19to36; }

    // Zone
    public int getZoneVois() { return totalZoneVoi; }
    public int getZoneOrphelins() { return totalZoneOrp; }
    public int getZoneTiers() { return totalZoneTier; }

    /*
     * Get Repeats
     */
    // Colors
    public  int getRepeatRed() { return repeatRed; }
    public int getRepeatBlack() { return repeatBlack; }
    public int getRepeatGreen() { return repeatGreen; }

    // Odd and Even
    public int getRepeatOdd() { return repeatOdd; }
    public int getRepeatEven() { return repeatEven; }

    // Dozen
    public int getRepeat1stDozen() { return repeat1stDozen; }
    public int getRepeat2ndDozen() { return repeat2ndDozen; }
    public int getRepeat3rdDozen() { return repeat3rdDozen; }

    // Rows
    public int getRepeat1stRow() { return repeat1stRow; }
    public int getRepeat2ndRow() { return repeat2ndRow; }
    public int getRepeat3rdRow() { return repeat3rdRow; }

    // 1 to 18 and 19 to 36
    public int getRepeat1to18() { return repeat1to18; }
    public int getRepeat19to36() { return repeat19to36; }

    // Zone
    public int getRepeatZoneVois() { return repeatZoneVoi; }
    public int getRepeatZoneOrphelins() { return repeatZoneOrp; }
    public int getRepeatZoneTiers() { return repeatZoneTier; }
    #endregion
}
