using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeeTahBody : BodyBehavior
{
    [SerializeField] protected float moveSpeed = 2f;



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
     
        SetAllSpriteDirection();
        if (moveTimer > 0)
        {
            moveTimer -= deltatime;
            var prex = transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, moveTo, moveSpeed);
            var nowx = transform.position.x;
            if (Mathf.Abs(prex - nowx) < 0.1) { moveTimer = 0; }
        }
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            playsound("dash");
            moveTo = target.gameObject.transform.position;
            moveTimer = 4;
        }

    }
}
