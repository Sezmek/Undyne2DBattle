using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingSpear : SpearBase
{
    private bool following = true;
    private float followingSpeed;
    public bool isRed;
    public void SetUpSpear(float _lifeTime, Sprite _sprite, Player _player, float _followingSpeed, bool _isRed)
    {
        player = _player;
        sr.sprite = _sprite;
        lifeTime = _lifeTime;
        followingSpeed = _followingSpeed;
        isRed = _isRed;
    }

    private void Update()
    {
        if (!Isoff)
        {
             lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                SpearDeahtAnim();
            }
            if (following)
            {
                if (Vector2.Distance(transform.position, player.transform.position) < 13)
                    following = false;

                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb.velocity = direction * followingSpeed;
                transform.right = rb.velocity;
            }
            else
                rb.velocity = transform.right * followingSpeed * 1.5f;
        }
    }
    public override void SpearDeahtAnim()
    {
        base.SpearDeahtAnim();
        if (isRed)
            anim.Play("RedSpearAnim");
        else
            anim.Play("RegularSpearAnim");
    }
}
