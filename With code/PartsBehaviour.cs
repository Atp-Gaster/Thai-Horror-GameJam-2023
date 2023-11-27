using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class PartsBehaviour : MonoBehaviour
{
    public static void playsound(string name, float volume=1)
    {
        var sn = "withsoundplayer";
        var a = GameObject.Find(sn);
        if (a != null)
        {
            var b = a.GetComponent<withMusicPlayer>();
            if (b != null)
            {
                var c = b.PlayVoice(name, volume);
                if (!c) Debug.Log(name + "[ no such sound !!! ]" + name);
            }
            else
            {
                Debug.Log(sn + "[Has no withMusicPlayer ]" );
            }
        }
        else 
        {
            Debug.Log(sn+"[No bobject!!! named ]"+sn);
        }
    }

        public static void setAllLayer(GameObject o,LayerMask l,int pool=1) 
    {
        o.layer = l;
        for(int i=0;i<o.transform.childCount;i++)
        {
            var n = o.transform.GetChild(i);
            setAllLayer(n.gameObject, l,pool+1);
        }
    }
    public static Rigidbody2D getTopRigidbody(GameObject o)
    {
        if (o.transform.parent != null) { return getTopRigidbody(o.transform.parent.gameObject); }

        return o.GetComponent<Rigidbody2D>();
    }
    const float cooltimecover = 0.05f;
    [SerializeField]
    public Status status;
    public bool is_dead { get { return status.is_dead || (parent != null && parent.is_dead); } }
    /// <summary>
    /// attack Interval
    /// </summary>
    [SerializeField] public Cooltime attackCD = new Cooltime(2);
    /// <summary>
    /// skill Interval
    /// </summary>
    [SerializeField] public Cooltime skillCD = new Cooltime(10);
    /// <summary>
    /// Back Default Speed
    /// </summary>
    [SerializeField] protected float BDSpeed = 0.1f;

    //list that head or arm parts floating point idk how to implement this
    [SerializeField]
    public List<GameObject> partsPoint = new List<GameObject>();

    /// <summary>
    /// just ready for inspecter
    /// </summary>
    [SerializeField]
    protected List<GameObject> ComponentChild = new List<GameObject>();

    /// <summary>
    /// just ready for inspecter
    /// </summary>
    [SerializeField] protected GameObject ComponentParent = null;


    [HideInInspector]
    public List<PartsBehaviour> child = new List<PartsBehaviour>();
    [HideInInspector]
    public PartsBehaviour parent = null;

    /// <summary>
    /// how bigs of this parts's hitbox
    /// </summary>
    public float bigs { get {

            if (gameObject.GetComponent<Collider2D>() != null) 
            {
                return (Mathf.Abs(gameObject.transform.localScale.x)
                    + Mathf.Abs(gameObject.transform.localScale.y)) / 2; 
            }
            for (int i = 0; i < transform.childCount; i++) 
            {
                var c=transform.GetChild(i).gameObject.GetComponent<Collider2D>();
                if (c != null) 
                {
                    return (Mathf.Abs(c.gameObject.transform.localScale.x* gameObject.transform.localScale.x)
                     + Mathf.Abs(c.gameObject.transform.localScale.y* gameObject.transform.localScale.y)) / 2;
                }
            }
            return (Mathf.Abs(gameObject.transform.localScale.x)
                    + Mathf.Abs(gameObject.transform.localScale.y)) / 2;
        } }

    /// <summary>
    /// call from children
    /// </summary>
    /// <returns></returns>
    protected Vector2 getDefaultPosition(PartsBehaviour p)
    {
        if (gameObject == null) return new Vector2(0, 0);
        for (int i = 0; i < child.Count; i++)
        {
            if (child[i] == p)
            {
                if (i < partsPoint.Count)
                {
                    return (Vector2)partsPoint[i].transform.position
                        // + (Vector2)transform.position
                        +new Vector2(swaying.cos*p.bigs*swaying2.sin*0
                        ,swaying.sin*p.bigs * swaying2.sin);
                }

                break;

            }
        }

        return gameObject.transform.position;
    }
    public Vector2 defaultPosition { get 
        {
            if (is_Core) return transform.position;
            return Core.getDefaultPosition(this);
        } }
    floatmill swaying = new floatmill(0,1,0.0f,0.2f);
    floatmill swaying2 = new floatmill(-0.1f, 0.1f, 0.0f, 0.2f);
    /// <summary>
    /// idk how to targeting enemy
    /// this object might be cursor of Player?
    /// if AI, just parts of Player? 
    /// </summary>
    [HideInInspector] public GameObject target;

    //bool that this part is CORE parts
    public bool is_Core { get { return parent == null; } }
    /// <summary>
    /// return core of this body;
    /// </summary>
    public PartsBehaviour Core
    {
        get
        {
            if (!is_Core) return parent;
            return this;
        }
    }
    public List<PartsBehaviour> AllParts
    {
        get
        {
            var ret = new List<PartsBehaviour>();
            ret.Add(Core);
            foreach (var a in Core.child) ret.Add(a);
            return ret;
        }
    }
    public List<PartsBehaviour> AllAliveParts
    {
        get
        {
            var ret = AllParts;
            for (int i = ret.Count - 1; i >= 0; i--)
            {
                if (ret[i].is_dead)
                {
                    ret.RemoveAt(i);
                }
            }
            return ret;
        }
    }
    public float sumOfMaxHp 
    { get 
        {
            float sum = 0;
            foreach (var a in AllParts) 
            {
                sum += a.status.maxHp;
            }
            return sum;
        }
    }
    public float sumOfHp
    {
        get
        {
            float sum = 0;
            foreach (var a in AllAliveParts)
            {
                sum += a.status.hp;
            }
            return sum;
        }
    }


    // Start is called before the first frame update
    virtual protected void Start()
    {
        status.reset();

        setChildAndParent();


        attackCD.Start();
        skillCD.Start();
        swaying.Start();
        swaying2.Start();
    }
    /// <summary>
    /// set chield and parent from inspector
    /// </summary>
    public void setChildAndParent()
    {
        if (ComponentChild != null)
        {
            foreach (var a in ComponentChild)
            {
                if (a != null)
                {
                    child.Add(a.GetComponent<PartsBehaviour>());
                }
            }
        }
        if (ComponentParent != null)
        {

            parent = ComponentParent.GetComponent<PartsBehaviour>();
        }
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        if (!is_Core)
        {
            
            var velo=parent.gameObject.GetComponent<Rigidbody2D>().velocity;

             parent.gameObject.GetComponent<Rigidbody2D>().velocity=velo + gameObject.GetComponent<Rigidbody2D>().velocity;
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        }
        var deltatime = Time.deltaTime;
        if (is_dead)
        {
            dead();
        }
        attackCD.Update(deltatime);
        skillCD.Update(deltatime);
        if (!is_dead)//if dead stop attack skill only
        {
            doAttack();
        }

        swaying.Update(deltatime);
        swaying2.Update(deltatime);
    }
    virtual public void dead()
    {
        if (is_Core)
        {
            //IDK Dead Effects here 
            foreach (var a in child)
            {
                if (a != null)
                {
                    a.dead();
                    Destroy(a.gameObject);

                }
            }
            if (gameObject != null)
            {
                Destroy(gameObject);
            }
        }
        //when dead, dont destroy and transparenten sprite
        else if(deadmode==false) 
        {
            deadmode = true;
            var list = new List<GameObject>();
            list.Add(gameObject);
            for (int i=0;i<transform.childCount;i++) 
            {
                list.Add(transform.GetChild(i).gameObject);
            }
            foreach (var a in list) 
            {
                var sp=a.GetComponent<SpriteRenderer>();
                if (sp != null) 
                {
                    var col = sp.color;
                    col.a /= 2;
                    sp.color = col;
                }
            }
        }
    }
    bool deadmode = false;
    /// <summary>
    /// call from inherited Update()
    /// </summary>
    /// <param name="time"></param>
    virtual protected void backToDefault(float time)
    {
        // Debug.Log(is_Core+" sda ");
        if (!is_Core)
        {
            // move sprite towards the target location
            var tag = parent.getDefaultPosition(this);
            transform.position = Vector2.MoveTowards(transform.position, tag, BDSpeed);
            SetAllSpriteDirection();
        }
    }
    /// <summary>
    /// do Attack Method
    /// </summary>
    /// <returns></returns>
    virtual public bool doAttack()
    {
        if (attackCD.activate())
        {
            attack();
            return true;
        }
        return false;
    }
    /// <summary>
    /// do Skill Method
    /// </summary>
    /// <returns></returns>
    virtual public bool doSkill(GameObject target)
    {
        if (skillCD.activate())
        {
            skill(target);
            return true;
        }
        return false;
    }
    /// <summary>
    /// Attack Action
    /// </summary>
    abstract protected void attack();
    /// <summary>
    /// Skill action
    /// </summary>
    /// <param name="target">if is this Player, target maybe cursour</param>
    abstract protected void skill(GameObject target);

    /// <summary>
    /// Flip All Texture
    /// </summary>
    /// <param name="Right">True means texture would be right</param>
    protected void SetAllSpriteDirection(bool Right)
    {
        var lsc = transform.localScale;
        if (Right && lsc.x > 0) 
        {
            transform.localScale = new Vector3(lsc.x*-1,lsc.y,lsc.z);
        }
        if (!Right && lsc.x < 0)
        {
            transform.localScale = new Vector3(lsc.x * -1, lsc.y, lsc.z);
        }
        /* it no need to flip this(Parent) objects splite
         * Because i think Parent(this) object is not sprite just hit box
        {
            var sp = GetComponent<SpriteRenderer>();
            if (sp != null)
            {
                sp.flipX = Right;
            }
        }*/
    }
    /// <summary>
    /// Flip All Texture By Core and that target
    /// </summary>
    protected void SetAllSpriteDirection()
    {
        var CX = Core.transform.position.x;
        if (target != null&&Core.target!=null)
        {
            var tx = Core.target.transform.position.x;
            SetAllSpriteDirection(CX < tx);
        }
        else 
        {//Do nothing
        
        }
    }
    /// <summary>
    /// Flip All Texture By this object and targetX
    /// </summary>
    protected void SetAllSpriteDirection(float tagx)
    {
        var CX = Core.transform.position.x;
        SetAllSpriteDirection(CX < tagx);
    }
    /// <summary>
    /// returns this part direction is right
    /// </summary>
    public bool Right { get { return transform.localScale.x<0; } }

    /// <summary>
    /// if(this part direction is right)return-1; else 1
    /// </summary>
    public float ri { get { if (Right) return -1; return 1; } }
}


[System.Serializable]
public class Status
{
    [SerializeField]
    protected float _maxHp = 100;

    public float _hp = 0;
    public float hpRatio { get { return _hp / _maxHp; } }
    public float hp { get { return _hp; } }
    public float maxHp { get { return _maxHp; } }
    //true means this status is dead
    public bool is_dead { get { return hp <= 0; } }
    public Status(){ }
        public Status(Status s)
    {
        _maxHp = s.maxHp;
    }
    /// <summary>
    /// call this on start of statused object
    /// </summary>
    public void reset()
    {
        _hp = _maxHp;
    }
    /// <summary>
    /// function of damage to status
    /// </summary>
    /// <param name="dHp">changehp - means damage + means heal</param>
    /// <returns>changed hp</returns>
    public float creaseHp(float dHp)
    {
        var prehp = hp;
        var calcedHp = hp + dHp;
        if (calcedHp < 0)
        {
            _hp = 0;
            Debug.Log(hp +"+" +dHp+" = "+_hp+" creasedDAM");
            return dHp - calcedHp;
        }
        else if (calcedHp > _maxHp)
        {
            _hp = _maxHp;
            Debug.Log(prehp + "+" + dHp + " = " + _hp + " creasedHEAL");
            return dHp - (calcedHp - _maxHp);
        }
        else _hp = calcedHp;
        Debug.Log(prehp + "+" + dHp + " = " + _hp + " creasedDEF");
        return dHp;
    }
}
[System.Serializable]
public class DamageField
{
    /// <summary>
    /// attack starts time
    /// </summary>
    [SerializeField] float starts = 0;
    public float STARTS { get { return starts; } }
    /// <summary>
    /// return times on start
    /// </summary>
    public bool is_start { get { if (_time < 0) return false; return  starts<=_time && !is_end; } }
    /// <summary>
    /// attack ends time
    /// </summary>
    [SerializeField] float ends = 10;
 
    public float ENDS { get { return ends; } }
    /// <summary>
    /// return time on end
    /// </summary>
    public bool is_end { get { if (_time < 0) return true; return ends <=_time; } }
    /// <summary>
    /// return time starts between ends
    /// </summary>
    public float between { get { return ENDS - STARTS; } }


    [SerializeField] float damage = 1;
    public float DAMAGE { get { return damage; } }
    /// <summary>
    /// damage interval to same object -1 means hit only once
    /// </summary>
    [SerializeField] float DMInterval = -1;
    /// <summary>
    /// -1 means infinity;
    /// </summary>
    [SerializeField] int hitTimes = -1;
    /// <summary>
    /// damage attenuation by time 1 means no change
    /// 2 means on end time damage will be twice
    /// 0 means go to zero damage 
    /// </summary>
    [SerializeField]protected float attenuation=1;


    public DamageField() { }
    public DamageField(DamageField d) 
    {
        starts = d.starts;
        ends = d.ends;
        damage = d.damage;
        DMInterval = d.DMInterval;
        hitTimes = d.hitTimes;
        attenuation = d.attenuation;
    }

    float _time = -1;
    public float time { get { return _time; } }
    int remainTimes = 0;
    List<Status> hitteds = new List<Status>();
    List<float> hittedTime = new List<float>();
    /// <summary>
    /// return this field' remainTimes not remain;
    /// </summary>
    public bool NoMoreTimes { get { return remainTimes == 0 || _time > ends; } }

    /// <summary>
    /// Call this when start hitBox
    /// </summary>
    public void Start()
    {
        _time = 0;
        hitteds = new List<Status>();
        hittedTime = new List<float>();

        remainTimes = hitTimes;
    }

    /// <summary>
    /// Call this when Updating hitbox
    /// </summary>
    /// <param name="deltaTime"></param>
    public void Update(float deltaTime)
    {
        if (!is_end)
        {
            _time += deltaTime;

            for (int i = hittedTime.Count - 1; i >= 0; i--)
            {
                if (hittedTime[i] >= 0)
                {
                    hittedTime[i] -= deltaTime;
                    if (hittedTime[i] <= 0)
                    {
                        hittedTime.RemoveAt(i);
                        hitteds.RemoveAt(i);
                    }
                }
            }
        }
    }
    /// <summary>
    /// move that 
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public float creaseHp(Status target)
    {
        //times remain and can do damage time and not hitted before
        if (!(NoMoreTimes)
            &&  is_start && !is_end
            && !hitteds.Contains(target)
            &&!target.is_dead)
        {
            hitteds.Add(target);
            hittedTime.Add(DMInterval);

            if (remainTimes > 0)
            {
                remainTimes -= 1;
            }

            var dam = damage*(1-(_time-starts)/between)+(damage*attenuation)*((_time - starts) / between);
            
            var ret = target.creaseHp(-dam);
            //Debug.Log(ret + " DAM attack!! remains" + target.hp);
            return ret;
        }
        //Debug.Log("cant attack some how" +starts+" < "+ends+" = "+_time+" remain "+ remainTimes+" " );
        return 0;
    }

    public float creaseHp(PartsBehaviour p)
    {
        return creaseHp(p.status);
    }
    public float creaseHp(GameObject o)
    {
        var p = o.GetComponent<PartsBehaviour>();
        if (p != null)
        {
            return creaseHp(p);
        }
        if (o.transform.parent != null) 
        {
            return creaseHp(o.transform.parent.gameObject);
        }
        return 0;
    }

}
/// <summary>
/// float that can change time easry
/// </summary>
[System.Serializable]
public class floatmill
{
    [SerializeField] float min = 0,max=0;
    [SerializeField] float minspeed = 0.0f,maxspeed = 0.5f;
   /// <summary>
   /// 0, and 0,360 means circle, 0,90 , 180, 270 means rigidbodyShape
   /// </summary>
    [SerializeField] List<float> degreetuple = new List<float> { 0 };
    float _rad;
    public float rad { get { return _rad; }set { _rad = value; } }
    public float Trad { 
        get 
        {
            List<float> st = new List<float>();
            List<float> ed = new List<float>();
            List<float> dd = new List<float>();
            int i = 0;
            float sum = 0;
            for (; i < degreetuple.Count; i++) 
            {
                if (i % 2 == 0) st.Add(degreetuple[i] / 180 * Mathf.PI);
                else
                {
                    ed.Add(degreetuple[i] / 180 * Mathf.PI);
                    var d = ed[st.Count - 1] - st[st.Count - 1];
                    dd.Add(d);
                    sum += d;
                    
                }
            }
            if (i % 2 == 1) 
            {
                ed.Add((st[st.Count - 1]+360) / 180 * Mathf.PI);
                var d = ed[st.Count - 1] - st[st.Count - 1];
                dd.Add(d);
                sum += d;
            }
            var Prad = Mathf.Atan2(Mathf.Sin(_rad), Mathf.Cos(_rad))+Mathf.PI;
            Prad = sum * Prad / Mathf.PI/2;
            var tikurad = 0.0f;
            for (i = 0; i < st.Count; i++) 
            {
                if (Prad < tikurad + dd[i])
                {
                    return Prad + st[i];
                }
                else 
                {
                    tikurad += dd[i];
                }
            }
            return ed[st.Count - 1];
        }
    }
    public float sin{ get { return (max-min)*Mathf.Sin(Trad)+min; } }
    public float cos { get { return (max - min) * Mathf.Cos(Trad) + min; } }
    public floatmill(float min, float max,float minspeed=0,float maxspeed=0,List<float>dgreetuple=null) 
    {
        this.min = min;
        this.max = max;
        this.minspeed = minspeed;
        this.maxspeed = maxspeed;
        if (degreetuple == null)
        {
            this.degreetuple = new List<float> { 0 };
        }
        else 
        {
            this.degreetuple = new List<float>(degreetuple);
        }
    }
    public floatmill() { }

    /// <summary>
    /// randomaize float mill
    /// </summary>
    public void Start() 
    {
        _rad = UnityEngine.Random.Range(0, Mathf.PI * 2);
    }
    public void Update(float time) 
    {
        _rad += Random.Range(maxspeed,minspeed)*time;
        _rad = Mathf.Atan2(Mathf.Sin(_rad), Mathf.Cos(_rad));
    }
    public static explicit operator float(floatmill val)
    {
        return val.sin;
    }
}
[System.Serializable]
public class Cooltime
{
    public Cooltime() { }
    public Cooltime(float max) { this.max = max; }

    [SerializeField] protected float max;
    protected float _time;
    public float time{ get { return _time; } }
    public float remainTime { get { return max-_time; } }
    public float maxCoolTime { get { return max - _time; } }
    public float ratio { get { return _time/max; } }
    public void Update(float deltatime) 
    {
        _time += deltatime;
        _time = Mathf.Min(_time, max);
    }
    const float rand = 0.1f;
    public void Start() 
    {
        _time = max * Random.Range(0, rand);
    }
    /// <summary>
    /// if max,return true and reset timer
    /// </summary>
    /// <returns></returns>
    public bool activate() 
    {
        if (canActivare) {
            Start();
            return true; 
                }
        return false;
    } 
        /// <summary>
        /// just look this cooltime available
        /// </summary>
    public bool canActivare { get { return _time >= max; } }
}
