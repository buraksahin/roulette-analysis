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
    int[] zone1V = { 22, 18, 29, 7, 28, 12, 35, 3, 26, 0, 32, 15, 19, 4, 21, 2, 25 };	// Zone 1 VoisinsduZero
    int[] zone2O = { 1, 20, 14, 31, 9 };                                      			// Zone 2 Orphelins
	int[] zone22O = { 17, 34, 6 };														// Zone 4 Orphelins
    int[] zone3T = { 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33 };                    // Zone 3 TiersduCylindre
    // Stats
    private int spinCount = 0;					// Total spin count
	private int[] totalColor = new int[3];		// Total colors 0:Green 1:Red 2:Black
	private int[] totalEvenOdd = new int[2];	// Total odd or even 0:Even 1:Odd
	private int[] totalDozen = new int[3];		// Total dozens
	private int[] totalColumn = new int[3];		// Total columns
	private int[] totalLowHigh = new int[2];	// Total low high numbers
	private int[] totalZone = new int[4];		// Total wheel zones 0:VoisindsduZero 1:Orphelins 2:TierduCylindre 3:Orphelins(2nd part)

    // Repeats
	private int[] repeatColor = new int[3];		// Total repeat of colors 0:Green 1:Red 2:Black
	private int[] repeatEvenOdd = new int[2];	// Total repeat of odd or even 0:Even 1:Odd
	private int[] repeatDozen = new int[3];		// Total repeat of dozens
    private int[] repeatColumn = new int[3];	// Total repeat of columns
	private int[] repeatLowHigh = new int[2];	// Total repeat of low and high numbers
	private int[] repeatZone = new int[4];		// Total repeat of zones 0:VoisindsduZero 1:Orphelins 2:TierduCylindre

    // Line and Number Lists
    public List<Number> numbers = new List<Number>(); // Keep the numbers
    public List<int> numberLine = new List<int>();    // Number Line

    // Statistics and Analysis
    float predictionCorrectness = 0.0f; // Prediction Correctness in range of [0, 1]
    int predictedNumber;                // Predicted Number

    #endregion

    #region Main Functions
    // Use this for initialization
    public void Start()
    {
        // Set Numbers
        for (int i = 0; i < 37; i++){
			numbers.Add( new Number( i, getNumberColor(i), getNumberPosition(i), getNumberColumnPosition(i), getNumberDozen(i), getNumberZone(i)) );
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
    int getNumberColumnPosition(int _number){
		int columnPosition = 0;
		if (_number % 3 == 0) { columnPosition = 0; }
		if (_number % 3 == 2) { columnPosition = 1; }
		if (_number % 3 == 1) { columnPosition = 2; }
        return columnPosition;
    }

    /*
     * Get Number Dozen
     */
    int getNumberDozen(int _number){
        int dozenPos = 0;
        if (_number >= 0  && _number < 13) { dozenPos = 0; }
        else if (_number > 12 && _number < 25) { dozenPos = 1; }
        else if (_number > 24) { dozenPos = 2; }
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
                tempZone = 0;
                break;
            }
        }
        for (int i = 0; i < zone2O.Length; i++)
        {
            if (_number == zone2O[i])
            {
                tempZone = 1;
                break;
            }
        }
		for (int i = 0; i < zone22O.Length; i++)
		{
			if (_number == zone22O[i])
			{
				tempZone = 3;
				break;
			}
		}
        for (int i = 0; i < zone3T.Length; i++)
        {
            if (_number == zone3T[i])
            {
                tempZone = 2;
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
        if(spinCount > 1)
        {
            numbers[_number].increasePrev(numberLine[spinCount - 2]);	// Increase current number's prev
            numbers[numberLine[spinCount - 2]].increaseNext(_number);	// Increase next of previous number
			// if previous number and current number is same
			if(numberLine[spinCount - 1] == _number){
                numbers[_number].IncreaserepeatCount(); // Increase repeat count of the number
            }
        }
		// Increase total color
		totalColor[numbers[_number].getColor()] += 1;

		// Increase total Odd or Even
		totalEvenOdd[_number%2] += 1;

		if (_number != 0) {
			// Increase total dozen
			totalDozen[getNumberDozen(_number)] += 1;

			// Increase total column
			totalColumn[getNumberColumnPosition(_number)] += 1;

			// Increase total low high
			if (_number < 19) {
				totalLowHigh [0] += 1;
			} else {
				totalLowHigh [1] += 1;
			}
		}
		// Increase total wheel
		totalZone[getNumberZone(_number)] += 1;
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
	public int getTotalOfGreen() { return totalColor[0]; }
	public int getTotalOfRed() { return totalColor[1]; }
	public int getTotalOfBlack() { return totalColor[2]; }

    // Total of Odd and Even
	public int getTotalEven() { return totalEvenOdd[0]; }
	public int getTotalOdd() { return totalEvenOdd[1]; }

    // Total of Dozen
	public int getTot1stDozen() { return totalDozen[0]; }
	public int getTo2ndDozen() { return totalDozen[1]; }
	public int getTot3rdDozen() { return totalDozen[2]; }

    // Total of Columns
	public int getTot1stCol() { return totalColumn[0]; }
	public int getTot2ndCol() { return totalColumn[1]; }
	public int getTot3rdCol() { return totalColumn[2]; }

    // Total of Range
	public int getTotLow() { return totalLowHigh[0]; }
	public int getTotHigh() { return totalLowHigh[1]; }

    // Zone
	public int getZoneVois() { return totalZone[0]; }
	public int getZoneOrphelins() { return totalZone[1]; }
	public int getZoneTiers() { return totalZone[2]; }
	public int getZoneOrphelins2() { return totalZone[3]; }

    /*
     * Get Repeats
     */
    // Colors
	public int getRepeatGreen() { return repeatColor[0]; }
	public  int getRepeatRed() { return repeatColor[1]; }
	public int getRepeatBlack() { return repeatColor[2]; }

    // Odd and Even
	public int getRepeatOdd() { return repeatEvenOdd[0]; }
    public int getRepeatEven() { return repeatEvenOdd[1]; }

    // Dozen
	public int getRepeat1stDozen() { return repeatDozen[0]; }
	public int getRepeat2ndDozen() { return repeatDozen[1]; }
	public int getRepeat3rdDozen() { return repeatDozen[2]; }

    // Rows
    public int getRepeat1stColumn() { return repeatColumn[0]; }
	public int getRepeat2ndColumn() { return repeatColumn[1]; }
	public int getRepeat3rdColumn() { return repeatColumn[2]; }

    // 1 to 18 and 19 to 36
	public int getRepeatLow() { return repeatLowHigh[0]; }
	public int getRepeatHigh() { return repeatLowHigh[1]; }

    // Zone
	public int getRepeatZoneVois() { return repeatZone[0]; }
	public int getRepeatZoneOrphelins() { return repeatZone[1]; }
	public int getRepeatZoneTiers() { return repeatZone[2]; }
    #endregion
}
