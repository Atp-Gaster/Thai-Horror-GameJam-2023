using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinomechBody : BodyBehavior
{
    [SerializeField] protected Status zonehealth;
    [SerializeField] protected GameObject healzone;

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
        makefire(target,zonehealth);

    }
    GameObject makefire(GameObject tag,Status d)
    {
        playsound("shield");
        var f = Instantiate(healzone);
        var FB = f.GetComponent<shieldbehavior>();
        f.transform.position=tag.transform.position;
        PartsBehaviour.setAllLayer(f, this.gameObject.layer);
        FB.status = new Status(d);
        FB.status.reset();
        return f;
    }
}
