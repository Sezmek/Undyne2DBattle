using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBase : MonoBehaviour
{
    protected float launchForce;
    protected float lifeTime;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Player player;
    protected Rigidbody2D rb;
    protected BoxCollider2D bd;
    public bool Isoff;


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        anim.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoxCollider2D>();
        player = PlayerManager.instance.player;
    }



    public virtual void OnTriggerEnter2D(Collider2D collison)
    {
        if(sr.sprite == SpearMenager.instance.lightBlueSpear && !Isoff)
            return;
        if (collison.GetComponent<Player>() != null || collison.GetComponentInParent<Player>() != null)
            collison.GetComponent<Player>().Damage();
        else
            Stuck();
    }

    protected virtual void Stuck()
    {
        bd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public virtual void SpearDeahtAnim()
    {
        rb.velocity = Vector2.zero;
        Isoff = true;
        anim.enabled = true;    
    }
}
