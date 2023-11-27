using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class JackhandBehavior : PartsBehaviour
{
    [SerializeField] protected float moveSpeed=0.2f;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] protected DamageField skilldamage;

    [SerializeField] protected float lifesteal;
    [SerializeField] protected float shakeratio = 0.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    float atdegree = 0;
    float skdegree = 0;
    

    Vector2 moveTo = new Vector2(0, 0);
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var deltatime = Time.deltaTime;
        if (attackdamage.is_start)
        {
            atdegree += 360f / attackdamage.between * deltatime*-ri;
            
        }
        if (skilldamage.is_start)
        {
            skdegree += 360f / skilldamage.between*deltatime * -ri;
        }
        if (!attackdamage.is_end )
        {
            //IDK nice effects here

            transform.position = Vector2.MoveTowards(transform.position,
                 defaultPosition + (Vector2)(Quaternion.Euler(0, 0, skdegree + atdegree-90) * moveTo)
                 , moveSpeed);
            //and rotate object TO Direction of travel
        }
        if (!skilldamage.is_end)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                defaultPosition+ (Vector2)(Quaternion.Euler(0, 0, skdegree+atdegree-90) * moveTo)
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
            transform.rotation = Quaternion.Euler(0,0,  skdegree + atdegree+90);
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
            playsound("heal", 0.75f);
            foreach (var a in Core.AllAliveParts)
            {
                a.status.creaseHp(lifesteal);
            }
        }
    }
    
    protected override void attack()
    {
        if (target != null) 
        {
            //attack happens
            float range = bigs*shakeratio;
            var f=new floatmill(0,range,0,0);f.Start();
            var shake = new Vector2(f.cos, f.sin);
            moveTo = (Vector2)target.gameObject.transform.position+shake;
            SetAllSpriteDirection(moveTo.x);
            moveTo -= this.defaultPosition;
            moveTo.x = moveTo.sqrMagnitude;
            moveTo.y = 0;
            attackdamage.Start();
            atdegree = 0;
        }

    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            //skill happens

            moveTo = (Vector2)target.gameObject.transform.position ;
            SetAllSpriteDirection(moveTo.x);
            moveTo -= -this.defaultPosition;
            moveTo.x = moveTo.sqrMagnitude;
            moveTo.y = 0;

            skilldamage.Start();
            skdegree = 0;
        }
    }
}
