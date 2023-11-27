using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WendigoHead : PartsBehaviour
{
    [SerializeField] protected GameObject bark;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] protected DamageField skilldamage;
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
        makefire(attackdamage);
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            makefire(skilldamage);
        }
    }
    GameObject makefire(DamageField d)
    {
        playsound("bark");
        var f = Instantiate(bark);
        var FB = f.GetComponent<BarkBehavior>();
        FB.parent = this.gameObject;
        PartsBehaviour.setAllLayer(f,this.gameObject.layer);
        FB.damage = d;
        return f;
    }
}
