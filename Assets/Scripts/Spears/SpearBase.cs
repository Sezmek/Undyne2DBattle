using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearBase : MonoBehaviour
{
    protected float launchForce;
    public float lifeTime;
    public Sprite TasmaKlejacaMisieKonczy;
    protected Animator anim;
    protected SpriteRenderer sr;
    protected Player player;
    protected Rigidbody2D rb;
    protected BoxCollider2D bd;
    public bool Isoff;
    public bool isRed;
    public bool walkable;

    protected virtual Sprite CurrentSprite => sr.sprite;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
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
                if (!walkable)
                    SpearDeahtAnim();
                else
                {
                    lifeTime = 1.5f;
                    walkable = false;
                    sr.sprite = TasmaKlejacaMisieKonczy;
                }
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
        if(walkable)
        {
            bd.isTrigger = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            bd.enabled = true;
            gameObject.layer = LayerMask.NameToLayer("Ground");
        }
        else
        {
            bd.enabled = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    public virtual void SpearDeahtAnim()
    {
        rb.linearVelocity = Vector2.zero;
        Isoff = true;
        anim.enabled = true;
    }

    protected virtual void InitializeMovement()
    {
        if (walkable)
        {
            if (player.transform.position.x < transform.position.x)
            {
                transform.right = Vector2.left;
                rb.AddForce(Vector2.left * launchForce, ForceMode2D.Impulse);
            }
            else
            {
                transform.right = Vector2.right;
                rb.AddForce(Vector2.right * launchForce, ForceMode2D.Impulse);
            }
        }
        else
        { 
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(direction * launchForce, ForceMode2D.Impulse);
            transform.right = rb.linearVelocity.normalized;
        }
    }
}
