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


    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoxCollider2D>();
        player = PlayerManager.instance.player;
    }


    public virtual void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collison)
    {
        if(sr.sprite == SpearMenager.instance.lightBlueSpear)
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
}
