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
    public void AttackAnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);
        foreach (var item in colliders)
        {
            var regularSpear = item.GetComponent<RegularSpear>();
            if (regularSpear?.isRed == true)
            {
                regularSpear.SpearDeahtAnim();
            }

            var followingSpear = item.GetComponent<FollowingSpear>();
            if (followingSpear?.isRed == true)
            {
                followingSpear.SpearDeahtAnim();
            }
        }
    }
}
