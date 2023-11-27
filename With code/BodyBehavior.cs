using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BodyBehavior : PartsBehaviour
{
    [SerializeField] protected floatmill range = new floatmill(3, 7,0.1f,0.5f);

    [SerializeField] protected floatmill jump=new floatmill(0,300,0,0,new List<float> { 60, 120 });



  
    protected new Rigidbody2D rigidbody;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        rigidbody= GetComponent<Rigidbody2D>();
        range.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        base.Update();
        if (target != null)
        {
            //Move only X axis by range;
            //Range will change like Simple harmonic motion
            var tagx = target.transform.position.x;
            if (tagx < transform.position.x)
            {
                tagx += (float)range;
            }
            else
            {
                tagx -= (float)range;
            }
            var tag = new Vector2(tagx, getDefaultPosition(this).y);
            transform.position = Vector2.MoveTowards(transform.position, tag, BDSpeed);
        }
        range.Update(Time.deltaTime);

       
    }
    protected override void attack()
    {
        // This is jump!
        jump.Start();
        var force = new Vector2(jump.cos , jump.sin);
        rigidbody.AddForce(force);
        playsound("jump");
    }

}
