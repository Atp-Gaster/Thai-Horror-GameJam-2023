using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrankenHead : PartsBehaviour
{
    [SerializeField] protected GameObject bark;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] protected float attackpull=100;
    [SerializeField] protected DamageField skilldamage;
    [SerializeField] protected float skillpull = 200;
    // Start is called before the first frame update
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
        if(attackdamage.is_end&&skilldamage.is_end)
        {
            backToDefault(deltatime);
            //rotate object To Default
        }
    }
    protected override void attack()
    {
        makefire(attackdamage,attackpull);
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            makefire(skilldamage,skillpull);
        }
    }
    GameObject makefire(DamageField d,float pull)
    {
        playsound("magnet");
        var f = Instantiate(bark);
        var FB = f.GetComponent<Magnetbehavior>();
        FB.parent = this.gameObject;
        PartsBehaviour.setAllLayer(f,this.gameObject.layer);
        FB.damage = new DamageField(d);
        FB.pull = pull;
        return f;
    }
}
