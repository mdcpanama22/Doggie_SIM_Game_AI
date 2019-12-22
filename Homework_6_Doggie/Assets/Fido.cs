using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Panda;
public class Fido : MonoBehaviour
{
    //UI SECTION
    Text SleepMeter;
    Text HungerMeter;
    Text HappinessMeter;
    Text FoodMeter;


    GameObject Logs;
    Text FidoOut;
    Text internalClock;

    bool A = true;
    bool B = true;
    bool C = true;

    bool D = true;
    bool E = true;
    bool F = true;
    bool G = true;

    //BEHAVIOR True-false statements
    bool Sleepy = false;
    bool Irritable = false;
    bool Asleep = false;
    bool Awake = true;

    bool Hungry = false;
    bool NoFood = false;
    public float FoodInBowl = 10;


    float InitialW = 582.6f;
    float InitialH = 569.42f;
    int ChangeHeight = 100;

    float hunger = 1f; //Hunger 1 means they are full
    float chanceToEat = 0.01f;
    float tired = 1; //Tired 1 means they are fully awake
                     // Start is called before the first frame update
    float happy = .34f;

    #region Sleeping
    [Task]
    bool _Asleep
    {
        get
        {
            return Asleep;
        }
    }
    [Task]
    void GettingSleepy()
    {
        if (Sleepy)
        {
            if (A)
            {
                Bark("Fido is getting sleepy...");
                A = false;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    void GettingIrritated()
    {
        if (Irritable)
        {
            if (B)
            {
                Bark("Fido is starting to get irritated");
                B = false;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    void FallingAsleep()
    {
        if (Asleep)
        {
            if (C)
            {
                Bark("Fido goes to sleep");
                C = false;
            }
            Task.current.Succeed();
        }
        else
        {
            Task.current.Fail();
        }
    }
    [Task]
    bool Wakingup()
    {
        tired += 0.1f;
        if(tired >= 1)
        {
            tired = 1;
            Bark("Fido is awake");
            Asleep = false;
            Sleepy = false;
            Irritable = false;
            A = true;
            B = true;
            C = true;

            return false;
        }
        return true;
    }
    #endregion

    [Task]
    void SearchingforFood()
    {

    }

    void Start()
    {
        internalClock = GameObject.Find("InternalClock").GetComponent<Text>();
        SleepMeter = GameObject.Find("SleepMeter").GetComponent<Text>();
        HungerMeter = GameObject.Find("Hungry").GetComponent<Text>();
        FoodMeter = GameObject.Find("Food").GetComponent<Text>();
        HappinessMeter = GameObject.Find("Happy").GetComponent<Text>();

        Logs = GameObject.Find("Logs");
        FidoOut = Logs.GetComponent<Text>();
        FidoOut.text = "";
        FidoOut.text = "\n \n";

    }

    [Task]
    //Log for the Dog
    void Bark(string b)
    {
        int MaxC = FidoOut.text.Length;
        if (MaxC >= 437 || (MaxC + b.Length >= 437))
        {
            Logs.GetComponent<RectTransform>().sizeDelta = new Vector2(InitialW, InitialH += ChangeHeight);
        }
        FidoOut.text = FidoOut.text + "\n<color=blue>" + internalClock.text + ": " + b + "</color>";

        Debug.Log(b);
        Task.current.Succeed();
    }

    public void UpdateGUI()
    {
        SleepMeter.text = "Sleep " + ((int)(tired * 100)).ToString() + "%";
        HungerMeter.text = "Hunger " + ((int)(hunger * 100)).ToString() + "%";
        FoodMeter.text = "Food " + FoodInBowl.ToString();
        if (FoodInBowl > 0)
        {
            NoFood = false;
        }
        HappinessMeter.text = "Happiness " + ((int)(happy * 100)).ToString() + "%";
    }
    [Task]
    void UpdateFidoState()
    {
        #region Dealing with Sleepiness
        if (tired <= 0.54 && tired > 0.35)
        {
            Sleepy = true;
        }
        else if (tired <= 0.35 && tired > .15 && Sleepy)
        {
            Irritable = true;
        }
        else if (tired <= 0.15)
        {
            Asleep = true;

            if (tired < 0) //In cases where the user travels forth in time often
            {
                tired = 0;
            }
        }
        #endregion
        if (FoodInBowl <= 0)
        {
            FoodInBowl = 0;
            NoFood = true;
        }
        if (hunger <= 1 && hunger > .12) {
            float chance = Random.Range(0.0f, 1.0f);
            if (hunger < chance)
            {
                Hungry = true;
            }

        } else if (hunger <= .12)
        {
            Hungry = true;
        }

    }
    [Task]
    bool _Hungry
    {
        get
        {
            return Hungry;
        }
    }
    [Task]
    bool GettingHungry()
    {
        if (Hungry)
        {
            if (D)
            {
                Bark("Fido finds his bowl of food");
                D = false;
            }
            return true;
        }
        return false;
    }
    float timetemp = 0;
    [Task]
    void IsFood()
    {
        //Such as that there is no food left in the bowl
        if (NoFood)
        {
            if (F)
            {
                Bark("Fido barks frantically to get food in his bowl");
                timetemp++;
                if (timetemp == 5){
                    F = false;
                }
            }
            Task.current.Fail();
        }
        else {

            if (E)
            {
                Bark("Fido eats a delicious meal");
                happy += 0.1f;
                Hungry = false;
                hunger = 1;
                FoodInBowl -= 5; 
                E = false;
                Task.current.Succeed();
                D = true;
                E = true;
                F = true;
                G = true;
            }
        }
    }
    [Task]
    public void UpdateFido(int n)
    {
        //Decay only occurs, if Fido is awake
        if (!Asleep)
        {
            tired -= 0.002f * n; //Remember to make numbers smaller
            if (hunger <= 0)
                hunger = 0;
            else
                hunger -= 0.001f * n;

            happy -= 0.05f;
        }
        UpdateFidoState();
        UpdateGUI();
    }
    void BarkTaskless(string b)
    {
        int MaxC = FidoOut.text.Length;
        if (MaxC >= 437 || (MaxC + b.Length >= 437))
        {
            Logs.GetComponent<RectTransform>().sizeDelta = new Vector2(InitialW, InitialH += ChangeHeight);
        }
        FidoOut.text = FidoOut.text + "\n<color=blue>" + internalClock.text + ": " + b + "</color>";

        Debug.Log(b);
    }

    public void UpdateHappiness(float reward, string FidoResponse)
    {
        happy += (reward / 100);
        BarkTaskless(FidoResponse);
        UpdateGUI();
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
