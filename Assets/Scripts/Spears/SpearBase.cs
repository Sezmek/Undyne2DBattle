using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBase : MonoBehaviour
{
    protected float launchForce;
    public float lifeTime;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Player player;
    protected Rigidbody2D rb;
    protected BoxCollider2D bd;
    public bool Isoff;
    public bool isRed;

    protected virtual Sprite CurrentSprite => sr.sprite;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        anim.enabled = false;
        rb = GetComponent<Rigidbody2D>();
        bd = GetComponent<BoxCollider2D>();
        player = PlayerManager.instance.player;
    }

    protected virtual void Start()
    {
        InitializeMovement();
    }

    protected virtual void Update()
    {
        if (!Isoff)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
                SpearDeahtAnim();
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (CurrentSprite == SpearManager.Instance.lightBlueSpear && !Isoff || collision.CompareTag("Wall"))
            return;

        if (collision.GetComponent<Player>() != null || collision.GetComponentInParent<Player>() != null)
            collision.GetComponent<Player>().Damage();
        else if (!collision.CompareTag("Spear"))
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

    protected virtual void InitializeMovement()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
        transform.right = rb.velocity.normalized;
    }
}
