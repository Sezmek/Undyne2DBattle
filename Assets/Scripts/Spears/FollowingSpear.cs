using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpear : SpearBase
{
    private bool following = true;
    private float followingSpeed;

    public void SetUpSpear(float _lifeTime, Sprite _sprite, Player _player, float _followingSpeed, bool _isRed)
    {
        player = _player;
        sr.sprite = _sprite;
        lifeTime = _lifeTime;
        followingSpeed = _followingSpeed;
        isRed = _isRed;
        if (isRed)
            anim.Play("defaultRed");
    }

    protected override void Update()
    {
        base.Update();
        if (!Isoff)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.linearVelocity = following ? direction * followingSpeed : transform.right * followingSpeed * 1.5f;
            transform.right = rb.linearVelocity.normalized;
        }

        if (following && Vector2.Distance(transform.position, player.transform.position) < 13)
        {
            following = false;
            InitializeMovement();
        }

    }

    public override void SpearDeahtAnim()
    {
        base.SpearDeahtAnim();
        anim.Play(isRed ? "RedSpearAnim" : "RegularSpearAnim");
    }
}
