using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAbilitySelector : MonoBehaviour
{

    public enum abilities { Default, Dash, TimeSlow};
    public abilities selectedAbility;
    public GameObject abilityHolder;
    public GameObject timerObj;
    public GameObject dashObj;
    public GameObject slowObj;


    private void Start()
    {

        selectedAbility = abilities.Default;

        timerObj =  abilityHolder.transform.GetChild(0).gameObject;
        dashObj = abilityHolder.transform.GetChild(1).gameObject;
        slowObj = abilityHolder.transform.GetChild(2).gameObject;
        timerObj.SetActive(true);
        dashObj.SetActive(false);
        slowObj.SetActive(false);

    }


    public void CycleAbility()
    {
        switch(selectedAbility)
        {
            case abilities.Default:
                selectedAbility = abilities.Dash;
                timerObj.SetActive(false);
                dashObj.SetActive(true);
                slowObj.SetActive(false);
                break;

            case abilities.Dash:
                selectedAbility = abilities.TimeSlow;
                timerObj.SetActive(false);
                dashObj.SetActive(false);
                slowObj.SetActive(true);
                break;

            case abilities.TimeSlow:
                selectedAbility = abilities.Default;
                timerObj.SetActive(true);
                dashObj.SetActive(false);
                slowObj.SetActive(false);
                break;

        }
    }



}

