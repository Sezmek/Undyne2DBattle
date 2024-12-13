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

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
            return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rb.velocity.magnitude > Mathf.Epsilon && !Isoff)
            {
                collision.GetComponent<Player>()?.Damage();
            }
        }
        else
        {
            Stuck();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        {
            if (collision.CompareTag("Wall"))
                return;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                if (rb.velocity.magnitude > Mathf.Epsilon && !Isoff)
                {
                    collision.GetComponent<Player>()?.Damage();
                }
            }
            else
            {
                Stuck();
            }
        }
    }

    public override void SpearDeahtAnim()
    {
        base.SpearDeahtAnim();
        anim.Play("BlueSpearAnim");
    }

    protected override void Stuck()
    {
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
