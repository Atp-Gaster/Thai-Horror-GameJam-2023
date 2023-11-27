using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackHeadBehavier : PartsBehaviour
{
    [SerializeField] protected float moveTime = 3;
    [SerializeField] protected float moveSpeed = 0.2f;
    [SerializeField] protected GameObject fire;
    // Start is called before the first frame update
    Vector2 moveTo = new Vector2(0, 0);
    float moveTimer=0;
    protected override void Start()
    {
        //Worn!! Dont forget base.Start() AND base.Update() 
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var deltatime = Time.deltaTime;
        if (moveTimer > 0)
        {
            //IDK nice effects here
            moveTimer -= deltatime;

            transform.position = Vector2.MoveTowards(transform.position, moveTo, moveSpeed);
            //and rotate object TO Direction of travel
        }
        else
        {
            backToDefault(deltatime);
            //rotate object To Default
        }
    }
    protected override void attack()
    {
        makefire();
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            moveTo = target.gameObject.transform.position;
            moveTimer = moveTime;
            makefire(); 
            SetAllSpriteDirection(moveTo.x);
        }
    }
    GameObject makefire()
    {
        playsound("fire");
        var f = Instantiate(fire);
        var FB=f.GetComponent<FireBehavior>();
        FB.parent = this.gameObject;
        PartsBehaviour.setAllLayer(f, this.gameObject.layer);
        return f;
    }
}
