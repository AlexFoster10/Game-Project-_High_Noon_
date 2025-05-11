using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static playerAbilitySelector;

public class playerAbilities : MonoBehaviour
{


    public playerAbilitySelector abilitySelector;
    public GameObject player;
    public playerMovement2 playerMovement;
    public double dashCooldown = 0;
    public double timeSlowCooldown = 0;
    [SerializeField] AudioClip[] dashSFX;
    [SerializeField] AudioClip slowSFX;

    // Start is called before the first frame update

    private void Start()
    {
        playerMovement = player.GetComponent<playerMovement2>();
        abilitySelector = player.GetComponent<playerAbilitySelector>();
    }

    private void Update()
    {
        if(dashCooldown > 0 ) {
            dashCooldown -= Time.deltaTime;
        }
        if (dashCooldown < 0)
        {
            dashCooldown = 0;
        }

        if (timeSlowCooldown > 0)
        {
            timeSlowCooldown -= Time.deltaTime;
        }
        if (timeSlowCooldown < 0)
        {
            timeSlowCooldown = 0;
        }

        if (Input.GetKey(KeyCode.E))
        {
            UseAbility();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            abilitySelector.CycleAbility();
        }



    }

    public void UseAbility()
    {

            switch (abilitySelector.selectedAbility)
            {
                case abilities.Default:
                    break;

                case abilities.Dash:
                    DashAbility();
                    break;

                case abilities.TimeSlow:
                    TimeAbility();
                    break;

            }

        
    }

    public void DashAbility()
    {
        if (dashCooldown == 0)
        {
            sfxManager.instance.playRandSFX(dashSFX, transform, 1f);
            playerMovement.Dash();
            dashCooldown = 3;
        }

    }

    public void TimeAbility()
    {
        if (timeSlowCooldown == 0)
        {
            sfxManager.instance.playSFX(slowSFX, transform, 1f);
            StartCoroutine(slow());
            timeSlowCooldown = 10;
        }
    }

    IEnumerator slow()
    {

        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(2);

        Time.timeScale = 1f;

    }


}
