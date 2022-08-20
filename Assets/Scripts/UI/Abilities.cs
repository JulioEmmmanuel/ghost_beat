using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [SerializeField]
    private Image abilityImage;
    [SerializeField]
    private float coolDownTime;
    [SerializeField]
    private KeyCode abilityKey;


    private bool isCoolDown = false;


    void Update()
    {
        if(Input.GetKey(abilityKey) && isCoolDown == false)
        {
            isCoolDown = true;
            abilityImage.fillAmount = 0;
        }

        if (isCoolDown)
        {
            abilityImage.fillAmount += 1 / coolDownTime * Time.deltaTime;
            if(abilityImage.fillAmount >= 1)
            {
                abilityImage.fillAmount = 1;
                isCoolDown = false;
            }
        }

    }
}
