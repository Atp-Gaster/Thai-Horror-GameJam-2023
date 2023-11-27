using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoArm : PartsBehaviour
{
    [SerializeField] protected float moveSpeed = 0.2f;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] protected DamageField skilldamage;

    [SerializeField] protected float shakeratio = 0.75f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    float dgree = 0;
    Vector2 moveTo = new Vector2(0, 0);
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var deltatime = Time.deltaTime;
        if (!(attackdamage.is_end || attackdamage.NoMoreTimes))
        {
            //IDK nice effects here

            transform.position = Vector2.MoveTowards(transform.position,
                 moveTo
                 , moveSpeed);
            //and rotate object TO Direction of travel
        }
        if (!(skilldamage.is_end || skilldamage.NoMoreTimes))
        {
            transform.position = Vector2.MoveTowards(transform.position,
                moveTo
                , moveSpeed);
        }
        if (attackdamage.is_end && skilldamage.is_end)
        {
            backToDefault(deltatime);
            //rotate object To Default
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0,  dgree+90);
        }
        //must be need to redresh damage
        skilldamage.Update(deltatime);
        attackdamage.Update(deltatime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //this toggles Damage
        var damaged = attackdamage.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("light");
            // IDK nice effect of hit here
        }
        damaged = skilldamage.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("light");
        }
    }

    protected override void attack()
    {
        if (target != null)
        {
            //attack happens
            float range = bigs * shakeratio;
            var f = new floatmill(0, range, 0, 0); f.Start();
            var shake = new Vector2(f.cos, f.sin);
            moveTo = (Vector2)target.gameObject.transform.position + shake;
            dgree = Vector2.SignedAngle((Vector2)gameObject.transform.position, moveTo);
            SetAllSpriteDirection(moveTo.x);
       
            attackdamage.Start();
        }

    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            //skill happens
            this.GetComponent<Animation>().Play();

            moveTo = (Vector2)target.gameObject.transform.position;
            SetAllSpriteDirection(moveTo.x);

            dgree = Vector2.SignedAngle((Vector2)gameObject.transform.position, moveTo);
            skilldamage.Start();
        }
    }
}
