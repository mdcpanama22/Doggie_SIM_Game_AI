using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InternalClock : MonoBehaviour
{

    float lastChange = 0;
    public static int hour;
    public static int minutes;
    string ampm; //if true am false pm
    Text internalClock;
    private bool ManualTime = false;

    private GameObject doggie;

    float InitialW = 582.6f;
    float InitialH = 569.42f;
    int ChangeHeight = 45;

    GameObject Logs;
    Text PlayerOut;


    public bool atWork = false;
    // Start is called before the first frame update

    void Start()
    {
        Logs = GameObject.Find("Logs");
        PlayerOut = Logs.GetComponent<Text>();

        doggie = GameObject.Find("Fido");
        internalClock = GameObject.Find("InternalClock").GetComponent<Text>();

        newDay();
    }
    string ZeroFormatting(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
    void CheckTime()
    {
        OveranHour();
        DayOver();
        ampm = AMPM(hour);
        printTime();
    }
    void printTime()
    {
       //For the sake of this doggie simulator, you are a stay home-owner, and work at home
       if (hour == 8 && minutes == 0)
        {
            Action("Starting working at your stay at home job");
            atWork = true;
        }
        else if (hour == 15 && minutes == 0)
        {
            Action("You are officially of the clock, although you set the time");
            atWork = false;
        }
        internalClock.text = ZeroFormatting(InternalClock.hour).ToString() + ":" + ZeroFormatting(InternalClock.minutes).ToString() + ampm;
    }
    bool OveranHour()
    {
        if(minutes == 60)
        {
            minutes = 0;
            hour++;
        } else if(minutes > 60)
        {
            int hourTemp = 0;
            while(minutes > 60)
            {
                minutes -= 60;
                hourTemp++;
            }
            hour += hourTemp;
        }
        else
        {
            return false;
        }
        return true;
    }

    bool DayOver()
    {
        if(hour == 24)
        {
            hour = 0;
        } else if(hour > 24)
        {
            int hourTemp = 0;
            while (hour > 24)
            {
                hour -= 24;
            }
        }
        else
        {
            return false;
        }
        return true;
    }
    string AMPM(int h) => h >= 0 && h < 12 ? "am" : "pm";

    void UpdateDoggie()
    {
        doggie.GetComponent<Fido>().UpdateFido(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (!ManualTime)
        {
            if (Time.time - lastChange > 1.5)
            {
                minutes += 1;
                lastChange = Time.time;
                CheckTime();
                UpdateDoggie(); //This is so that updates on the dog, are based on the in-game time

            }
        }
        else //Skips a frame in update
        {
            ManualTime = false;
        }


    }
    public void newDay()
    {
        hour = 7;
        minutes = 0;
        ampm = "am";
        printTime();
    }

    //Able to add to the internal time on other scripts
    public void AddTime(int n)
    {
        minutes += n;
        CheckTime();
    }

    public void Action(string b)
    {
        int MaxC = PlayerOut.text.Length;
        if (MaxC >= 457 || (MaxC + b.Length >= 457))
        {
            Logs.GetComponent<RectTransform>().sizeDelta = new Vector2(InitialW, InitialH += ChangeHeight);
        }
        
        PlayerOut.text = PlayerOut.text + "\n<color=red>" +internalClock.text + ": " + b+"</color>";

        Debug.Log(b);
    }

}
