using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommands : MonoBehaviour
{
    private GameObject IC;
    private GameObject FIDO;
    // Start is called before the first frame update
    void Start()
    {
        IC = GameObject.Find("InternalClock");
        FIDO = GameObject.Find("Fido");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            IC.GetComponent<InternalClock>().AddTime(15);
            IC.GetComponent<InternalClock>().Action("Moved up 15 minutes in time");
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            IC.GetComponent<InternalClock>().AddTime(60);
            IC.GetComponent<InternalClock>().Action("Moved up an hour in time");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            IC.GetComponent<InternalClock>().newDay();
            IC.GetComponent<InternalClock>().Action("Moved up to 8 AM of the following day");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            FIDO.GetComponent<Fido>().FoodInBowl += 5;
            FIDO.GetComponent<Fido>().UpdateGUI();
            IC.GetComponent<InternalClock>().Action("You added food to Fido's bowl");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            IC.GetComponent<InternalClock>().Action("You Gave Fido a Treat");
            FIDO.GetComponent<Fido>().UpdateHappiness(10, "Fido enjoys the treat");

        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            IC.GetComponent<InternalClock>().Action("Play fetch with Fido");
            FIDO.GetComponent<Fido>().UpdateHappiness(25, "Fido had fun playing with you");
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            IC.GetComponent<InternalClock>().Action("Pet Fido");
            FIDO.GetComponent<Fido>().UpdateHappiness(1, "Fido loves being petted");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            IC.GetComponent<InternalClock>().Action("Take for a walk");
            FIDO.GetComponent<Fido>().UpdateHappiness(12, "Fido enjoyed a nice walk out");
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            IC.GetComponent<InternalClock>().Action("Leave Fido alone");
            FIDO.GetComponent<Fido>().UpdateHappiness(0, "Is grumpy, and needs his space");
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            IC.GetComponent<InternalClock>().Action("Putting in extra hours at stay home job");


        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            IC.GetComponent<InternalClock>().Action("Done working for the day");
        }
    }
}
