using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackBodyBehavior : BodyBehavior
{
    [SerializeField] protected float moveTime = 4;
    [SerializeField] protected float moveSpeed = 0.2f;



    Vector2 moveTo = new Vector2(0, 0);
    float moveTimer;
    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        var deltatime = Time.deltaTime;
        base.Update();
        if (moveTimer > 0)
        {
            moveTimer -= deltatime;

            transform.position = Vector2.MoveTowards(transform.position, moveTo, moveSpeed);
            
           
        }
        SetAllSpriteDirection();
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            playsound("dash");
            moveTo = target.gameObject.transform.position;
            moveTimer = moveTime;
        }
       
    }
}
