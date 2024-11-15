using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularSpear : SpearBase
{
    public bool isRed;
    public void SetUpSpear(float _lifeTime, Sprite _sprite, Player _player, float _launchForce, bool _isRed)
    {
        player = _player;
        sr.sprite = _sprite;
        lifeTime = _lifeTime;
        launchForce = _launchForce;
        isRed = _isRed;
    }
    private void Start()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;

        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        transform.right = rb.velocity.normalized;
    }
    public override void Update()
    {
        base.Update();
    }
}
