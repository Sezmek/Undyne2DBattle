using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    public void SlidePS()
    {
        player.slidePS.Play();
    }
    public void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }
    public void ComboWindowTrigger()
    {
        player.Trigger();
    }
}
