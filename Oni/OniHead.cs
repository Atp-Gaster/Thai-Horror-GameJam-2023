using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OniHead : PartsBehaviour
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
        makefire(attackdamage,target);
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            makefire(skilldamage,target);
        }
    }
    GameObject makefire(DamageField d,GameObject target)
    {
        playsound("beam");
        var f = Instantiate(bark);
        var FB = f.GetComponent<BeamBehavior>();
        FB.parent = this.gameObject;
        f.transform.position = this.gameObject.transform.position;
        PartsBehaviour.setAllLayer(f,this.gameObject.layer);
        FB.damage = d;
        FB.setdirection(this.gameObject,target);
        var rot=this.gameObject.transform.rotation;
        this.gameObject.transform.rotation =Quaternion.Euler( rot.eulerAngles+ new Vector3(0,0,90+ri*90));
        return f;
    }
}
