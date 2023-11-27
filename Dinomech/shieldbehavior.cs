using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldbehavior : PartsBehaviour
{
    [SerializeField] protected float sizescale = 4;
    // Start is called before the first frame update
     protected override void Start()
    {

        base.Start();
    }

    // Update is called once per frame
    override protected void Update()
    {
        var deltatime = Time.deltaTime;
        base.Update();
    }
    public override void dead()
    {
        //nice effect here
        base.dead();
    }
    protected override void attack()
    {
    }
    protected override void skill(GameObject target)
    {
    }
}
