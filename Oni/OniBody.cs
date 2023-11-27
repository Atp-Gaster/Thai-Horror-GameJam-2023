using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniBody : BodyBehavior
{
    [SerializeField] protected GameObject healzone;

    [SerializeField] protected GameObject quakestarts;


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
            makefire(target);
        }

    }
    GameObject makefire(GameObject tag)
    {
        playsound("quake");

        SetAllSpriteDirection(tag);
        var f = Instantiate(healzone);
        var FB = f.GetComponent<QuakeBehavior>();
        f.transform.position = quakestarts.transform.position;// transform.TransformPoint(quakestarts.transform.position);
        FB.setspeed(tag);
        PartsBehaviour.setAllLayer(f, this.gameObject.layer);
        var hit=Physics2D.Raycast(f.transform.position, Vector3.down,10000000000,this.gameObject.layer);
       
        f.transform.position = Vector3.down*hit.distance + f.transform.position;
        return f;
    }
}
