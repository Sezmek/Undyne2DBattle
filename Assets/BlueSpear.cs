using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueSpear : SpearBase
{
    public void SetUpSpear(float _lifeTime, Sprite _sprite, Player _player, float _launchForce)
    {
        player = _player;
        sr.sprite = _sprite;
        lifeTime = _lifeTime;
        launchForce = _launchForce;
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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rb.velocity.magnitude > Mathf.Epsilon)
            {
                collision.GetComponent<Player>()?.Damage();
            }
        }
        else
            Stuck();        
    }

    protected override void Stuck()
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
