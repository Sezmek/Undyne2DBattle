using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class WallSpearController : MonoBehaviour
{
    private SpearType spearType;
    private float lifeTime;
    private bool isStraight;
    private Rigidbody2D rb;
    private Player player;
    private Animator anim;


    public void SetUpWallSpear(SpearType spearType, float lifeTime,  bool isStraight, Player player)
    {
        this.player = player;
        this.spearType = spearType;
        this.lifeTime = lifeTime;
        this.isStraight = isStraight;

        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }


    void Start()
    {
        float distanceToPlayer = Vector2.Distance(player.transform.position, transform.position);
        if (isStraight && distanceToPlayer <= 10f)
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;

            rb.AddForce(direction * 50, ForceMode2D.Impulse);
            transform.right = rb.velocity.normalized;
        }
        else
        {
            rb.AddForce(Vector2.up * 50, ForceMode2D.Impulse);
            transform.right = rb.velocity.normalized;
        }
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            anim.Play("RegularSpearAnim");
        }

    }
    public virtual void OnTriggerEnter2D(Collider2D collison)
    {
        if (collison.GetComponent<Player>() != null || collison.GetComponentInParent<Player>() != null)
            collison.GetComponent<Player>().Damage();
    }

    public void ZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
}

