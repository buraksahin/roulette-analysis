using UnityEngine;
using System.Collections;

public class ShowPanel : MonoBehaviour
{
    public GameObject RecentNumbersPanel;   //Store a reference to the Game Object Recent Numbers Panel
    public GameObject NumberAnalysisPanel;  //Store a reference to the Game Object Number Analysis Panel
    public GameObject StatisticsPanel;      //Store a reference to the Game Object Statistics Panel
    public GameObject OptionsPanel;         //Store a reference to the Game Object Option Panel
    public GameObject AboutPanel;           //Store a reference to the Game Object About Panel
    public GameObject SpinnerPanel;		    //Store a reference to the Game Object Spinner Panel

    //Call this function to activate and display the Recent Numbers panel during the main menu
    public void ShowRecentNumbersPanel(){
        RecentNumbersPanel.SetActive(true);
    }

    public void HideRecentNumbersPanel(){
        RecentNumbersPanel.SetActive(false);
    }

	//Call this function to deactivate and hide the About panel during the main menu
    public void ShowAboutPanel(){
        AboutPanel.SetActive(true);
    }

    public void HideAboutPanel(){
        AboutPanel.SetActive(false);
    }

	//Call this function to deactivate and hide the Options panel during the main menu
    public void ShowOptionsPanel(){
        OptionsPanel.SetActive(true);
    }

    public void HideOptionsPanel(){
        OptionsPanel.SetActive(false);
    }

	//Call this function to deactivate and hide the Number Analysis panel during the main menu
    public void ShowNumberAnalysisPanel(){
        NumberAnalysisPanel.SetActive(true);
    }

    public void HideNumberAnalysisPanel(){
        NumberAnalysisPanel.SetActive(false);
    }

	//Call this function to deactivate and hide the Statistics panel during the main menu
    public void ShowStatisticsPanel(){
        StatisticsPanel.SetActive(true);
    }

    public void HideStatisticsPanel(){
        StatisticsPanel.SetActive(false);
    }

	//Call this function to deactivate and hide the Spinner panel during the main menu
    public void ShowSpinnerPanel(){
        SpinnerPanel.SetActive(true);
    }

    public void HideSpinnerPanel(){
        SpinnerPanel.SetActive(false);
    }
}
