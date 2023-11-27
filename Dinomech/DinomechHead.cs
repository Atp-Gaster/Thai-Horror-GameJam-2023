using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinomechHead : PartsBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected DamageField attackdamage;
    [SerializeField] int attacktimes=3;
    [SerializeField] int skilltimes = 5;
    [SerializeField] float interbal = 0.25f;


    int remain = 0;
    Cooltime shot;
    // Start is called before the first frame update
    protected override void Start()
    {
        //Worn!! Dont forget base.Start() AND base.Update() 
        base.Start();
        shot = new Cooltime(interbal);
    }
    GameObject nowtag;

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        var deltatime = Time.deltaTime;
        shot.Update(deltatime);
        if (remain > 0 && shot.activate()) 
        {
            remain -= 1;
            makefire(attackdamage,nowtag);

        }
        if (remain <= 0) 
        {
            backToDefault(deltatime);
        }
    }
    protected override void attack()
    {
        remain += attacktimes;
        nowtag = target;
    }
    protected override void skill(GameObject target)
    {
        if (target != null)
        {
            nowtag = target;
            remain += skilltimes;
        }
    }
    GameObject makefire(DamageField d,GameObject target)
    {
        playsound("Sbomb",0.5f);
        var f = Instantiate(bullet);
        var FB = f.GetComponent<bulletbehavior1>();
        PartsBehaviour.setAllLayer(f,this.gameObject.layer);
        f.transform.position = gameObject.transform.position;
        FB.damage = new DamageField(d);
        FB.setspeed(gameObject,target);
        var rot = this.gameObject.transform.rotation;
        this.gameObject.transform.rotation = Quaternion.Euler(rot.eulerAngles + new Vector3(0, 0, 90 + ri * 90));
        return f;
    }
}
