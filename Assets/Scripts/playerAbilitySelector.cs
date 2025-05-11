using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAbilitySelector : MonoBehaviour
{

    public enum abilities { Default, Dash, TimeSlow};
    public abilities selectedAbility;
    

    private void Start()
    {

        selectedAbility = abilities.Default;
        
    }


    public void CycleAbility()
    {
        switch(selectedAbility)
        {
            case abilities.Default:
                selectedAbility = abilities.Dash; 
                break;

            case abilities.Dash:
                selectedAbility = abilities.TimeSlow;
                break;

            case abilities.TimeSlow:
                selectedAbility = abilities.Default;
                break;

        }
    }



}

