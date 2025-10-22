using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionaleEffectsManager : MonoBehaviour
{
    [SerializeField] private WindEffect windEffect;
    [SerializeField] private FireZoneManager fireZoneManager;
    [SerializeField] private RandomShooter randomShooter;
    [SerializeField] private Animator additionalEffectsAnimator;

    public void RageAnimation()
    { 
        if(windEffect.isWindActive && !additionalEffectsAnimator.GetBool("Mops Wind"))
        {
            additionalEffectsAnimator.SetBool("Mops Wind", true);
        }
        if(fireZoneManager.isFireStageActive && !additionalEffectsAnimator.GetBool("Chih Fire"))
        {
            additionalEffectsAnimator.SetBool("Chih Fire", true);
        }
        if(randomShooter.isShooting && !additionalEffectsAnimator.GetBool("Bul Attack"))
        {
            additionalEffectsAnimator.SetBool("Bul Attack", true);
        }
    }
}
