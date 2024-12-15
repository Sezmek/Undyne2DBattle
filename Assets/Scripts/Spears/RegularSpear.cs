using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularSpear : SpearBase
{

    public void SetUpSpear(float _lifeTime, Sprite _sprite, Player _player, float _launchForce, bool _isRed, bool _isWalkable)
    {
        player = _player;
        sr.sprite = _sprite;
        lifeTime = _lifeTime;
        launchForce = _launchForce;
        isRed = _isRed;
        walkable = _isWalkable;
    }
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    public override void SpearDeahtAnim()
    {
        base.SpearDeahtAnim();
        anim.Play(isRed ? "RedSpearAnim" : "RegularSpearAnim");
    }
}
