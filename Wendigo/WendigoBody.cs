using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoBody : BodyBehavior
{
    [SerializeField] protected float jumpSpeed = 0.2f;


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
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            playsound("jump");
            var dir=((Vector2)target.transform.position - (Vector2)this.gameObject.transform.position).normalized;
            var force = new Vector2(dir.x*jumpSpeed, dir.y * jumpSpeed);
            rigidbody.AddForce(force);
        }

    }
}
