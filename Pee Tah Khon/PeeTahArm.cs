using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeTahArm : PartsBehaviour
{
    [SerializeField] protected float moveSpeed = 0.2f;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] protected DamageField skilldamage;




    protected DamageField attackdamage2;
    protected DamageField skilldamage2;
    
    [SerializeField] protected float shakeratio = 0.75f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        attackdamage2 = new DamageField(attackdamage);
        skilldamage2 = new DamageField(skilldamage);
    }

    float dgree = 0;
    Vector2 moveTo = new Vector2(0, 0);
    bool attacked = false;
    bool skilled = false;
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var deltatime = Time.deltaTime;
        if (!attackdamage.is_end)
        {
            //IDK nice effects here
            this.GetComponent<Animation>().Play();
            transform.position = Vector2.MoveTowards(transform.position,
                 moveTo
                 , moveSpeed);
            //and rotate object TO Direction of travel
        }
        if (!skilldamage.is_end)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                moveTo
                , moveSpeed);
        }
        if (attackdamage.is_end && skilldamage.is_end)
        {
            if (attacked) { attacked = false;attackdamage2.Start(); }
            if (skilled) { skilled = false; skilldamage2.Start(); }
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
        skilldamage2.Update(deltatime);
        attackdamage2.Update(deltatime);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //this toggles Damage
        var damaged = attackdamage.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("noise");

            // IDK nice effect of hit here
        }
        damaged = skilldamage.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("noise");
        }
        //this toggles Damage
        damaged=attackdamage2.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("noise");
            // IDK nice effect of hit here
        }
        damaged = skilldamage2.creaseHp(collision.gameObject);
        if (damaged < 0)
        {
            playsound("noise");
        }
    }

    protected override void attack()
    {
        if (target != null)
        {
            //attack happens
            float range = bigs * shakeratio;
            Vector2 rna = target.gameObject.transform.position - gameObject.transform.position;
            moveTo = (Vector2)target.gameObject.transform.position + rna.normalized*range;
            dgree = Vector2.SignedAngle((Vector2)gameObject.transform.position, moveTo);
            SetAllSpriteDirection(moveTo.x);
       
            attackdamage.Start();
            attacked = true;
        }

    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            //skill happens

            float range = bigs * shakeratio;
            Vector2 rna = target.gameObject.transform.position - gameObject.transform.position;
            moveTo = (Vector2)target.gameObject.transform.position+rna;
            SetAllSpriteDirection(moveTo.x);

            dgree = Vector2.SignedAngle((Vector2)gameObject.transform.position, moveTo);
            skilldamage.Start();
            skilled = true;
        }
    }
}
