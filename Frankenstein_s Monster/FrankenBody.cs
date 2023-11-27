using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankenBody : BodyBehavior
{
    [SerializeField] protected float moveSpeed = 0.02f;
    [SerializeField] protected float knockback = 200;
    [SerializeField] protected DamageField skilldamage;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }
    Vector2 moveto;
    // Update is called once per frame
    override protected void Update()
    {
        var deltatime = Time.deltaTime;
        base.Update();
     
        SetAllSpriteDirection();
        if (!(skilldamage.is_end || skilldamage.NoMoreTimes))
        {
            var tagx = moveto.x;
           
            var tag = new Vector2(tagx, getDefaultPosition(this).y);
            transform.position = Vector2.MoveTowards(transform.position, tag, moveSpeed);
        }
        skilldamage.Update(deltatime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //this toggles Damage
     
        var damaged = skilldamage.creaseHp(collision.gameObject);
        if (damaged < 0)
        {

            playsound("bomb");
            var a = PartsBehaviour.getTopRigidbody(collision.gameObject);
            if (a != null)
            {
                a.AddForce(new Vector2(knockback*-ri,knockback));
            }
        }
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            moveto = target.transform.position;
            skilldamage.Start();
            
        }

    }
}
