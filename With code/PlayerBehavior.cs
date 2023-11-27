using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [HideInInspector]
    public PlayerBehavior target;

    [SerializeField]
    protected GameObject coregameObject=null;
    public bool controlable=true;

    [HideInInspector]
    public PartsBehaviour core;
    // Start is called before the first frame update
    void Start()
    {
        
        setCore();
    }

    /// <summary>
    /// SetCorefrominspector
    /// </summary>
    public void setCore()
    {
        if (coregameObject == null)
        {
            //Look at me! construct monstar like this!
            List<GameObject> bodies = new List<GameObject>
        {
            (GameObject)Resources.Load("Jack O_ Lantern\\body"),
            (GameObject)Resources.Load("Wendigo\\body"),
            (GameObject)Resources.Load("Frankenstein_s Monster\\body"),
            (GameObject)Resources.Load("Dinomech\\body"),
            (GameObject)Resources.Load("Pee Tah Khon\\body"),
            (GameObject)Resources.Load("Oni\\body"),
        };

            List<GameObject> parts = new List<GameObject>
        {
            (GameObject)Resources.Load("Jack O_ Lantern\\head")
            ,(GameObject)Resources.Load("Jack O_ Lantern\\larm")
            ,(GameObject)Resources.Load("Jack O_ Lantern\\rarm")
            ,(GameObject)Resources.Load("Wendigo\\head")
            ,(GameObject)Resources.Load("Wendigo\\larm")
            ,(GameObject)Resources.Load("Wendigo\\rarm")
            ,(GameObject)Resources.Load("Frankenstein_s Monster\\head")
            ,(GameObject)Resources.Load("Frankenstein_s Monster\\larm")
            ,(GameObject)Resources.Load("Frankenstein_s Monster\\rarm"),
             (GameObject)Resources.Load("Dinomech\\head"),
            (GameObject)Resources.Load("Dinomech\\larm"),
            (GameObject)Resources.Load("Dinomech\\rarm"),
            (GameObject)Resources.Load("Pee Tah Khon\\head"),
            (GameObject)Resources.Load("Pee Tah Khon\\larm"),
            (GameObject)Resources.Load("Pee Tah Khon\\rarm"),
            (GameObject)Resources.Load("Oni\\head"),
            (GameObject)Resources.Load("Oni\\larm"),
            (GameObject)Resources.Load("Oni\\rarm"),
        };

            int[] PartID = { 0, 0, 0, 0 };

            PartID[0] = PlayerPrefs.GetInt("PlayerA_PartID_Head");
            PartID[1] = PlayerPrefs.GetInt("PlayerA_PartID_Body");
            PartID[2] = PlayerPrefs.GetInt("PlayerA_PartID_Lh");
            PartID[3] = PlayerPrefs.GetInt("PlayerA_PartID_Rh");

            Debug.Log("========Loading Part========");
            Debug.Log(PartID[0]);
            Debug.Log(PartID[1]);
            Debug.Log(PartID[2]);
            Debug.Log(PartID[3]);
            Debug.Log("=============================");

            if (controlable)
            {
                if (coregameObject == null)
                {
                    switch (PartID[1])
                    {
                        case 0:
                            coregameObject = Instantiate(bodies[0], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case 1:
                            coregameObject = Instantiate(bodies[1], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case 2:
                            coregameObject = Instantiate(bodies[2], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case 3:
                            coregameObject = Instantiate(bodies[3], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case 4:
                            coregameObject = Instantiate(bodies[4], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                        case 5:
                            coregameObject = Instantiate(bodies[5], transform.position, Quaternion.Euler(0, 0, 0));
                            break;
                    }
                    var p = coregameObject.GetComponent<PartsBehaviour>();

                    for (int i = 0; i < p.partsPoint.Count; i++)
                    {
                        var idx = PartID[i + 1];
                        if (i == 0) idx = PartID[i];
                        idx *= 3;
                        idx += i;
                        var cp = Instantiate(parts[idx], transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<PartsBehaviour>();
                        p.child.Add(cp);
                        cp.parent = p;
                        Debug.Log("Part has made");
                    }
                }
                if (coregameObject != null)
                {
                    core = coregameObject.GetComponent<PartsBehaviour>();
                }
                foreach (var a in core.AllParts)
                {
                    PartsBehaviour.setAllLayer(a.gameObject,this.gameObject.layer);
                }
            }

            if (!controlable)
            {
                if (coregameObject == null)
                {
                    //Look at me! construct monstar like this!

                    coregameObject = Instantiate(bodies[Random.Range(0, bodies.Count)], transform.position, Quaternion.Euler(0, 0, 0));

                    var p = coregameObject.GetComponent<PartsBehaviour>();
                    for (int i = 0; i < p.partsPoint.Count; i++)
                    {
                        var idx = Random.Range(0, 6);
                        var cp = Instantiate(parts[idx * 3 + i], transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<PartsBehaviour>();
                        p.child.Add(cp);
                        cp.parent = p;
                        Debug.Log("Part has made");
                    }
                }
                if (coregameObject != null)
                {
                    core = coregameObject.GetComponent<PartsBehaviour>();
                }
                foreach (var a in core.AllParts)
                {
                    PartsBehaviour.setAllLayer(a.gameObject, this.gameObject.layer);
                }
            }
        }
    }
    public bool is_dead { get { return core.is_dead; } }

    // Update is called once per frame
    void Update()
    {

        var mousePos = Input.mousePosition;
        var worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        transform.position = worldPos;
        //this target maybe means cursor
        GameObject target=gameObject;
        if (core == null) return;
        if (controlable)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                core.child[1].doSkill(target);
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                core.child[0].doSkill(target);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                core.child[2].doSkill(target);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                core.doSkill(target);
            }
        }
        else
        {
            
            core.child[1].doSkill(core.child[1].target);

            core.child[0].doSkill(core.child[0].target);

            core.child[2].doSkill(core.child[2].target);

            core.doSkill(core.target);

        }

        core.target = getTarget(0);
        //set target to every parts
        foreach (var a in core.child)
        {
            if (a != null)
            {
                a.target = getTarget();
            }
        }
        
    }
    /// <summary>
    /// function that provide body parts
    /// </summary>
    /// <param name="idx">-1 means random</param>
    /// <returns></returns>
    public PartsBehaviour getParts(int idx=-1) 
    {
        if (idx < 0) 
        {
            var AAP = core.AllAliveParts;
            if (AAP.Count < 0) return null;
            idx =Random.Range(0,AAP.Count);
            return AAP[idx];
        }
        if (idx == 0) 
        {
            if (!core.is_dead)
            {
                return core;
            }
            else
            {
                return null;
            }
        }
        idx -= 1;
        if (idx < core.child.Count) 
        {
            if (!core.child[idx].is_dead)
            {
                return core.child[idx];
            }
            else 
            {
                return null;
            }
        }
        return null;
    }
    /// <summary>
    /// function that provide random parts . dont select backwords
    /// </summary>
    /// <returns></returns>
    public PartsBehaviour getParts2(PartsBehaviour from)
    {
        if (core == null) return null;
        var r=0<(core.gameObject.transform.position.x- from.gameObject.transform.position.x);
        var AAP = core.AllAliveParts;
        for (int i = AAP.Count - 1; i >= 0; i--)
        {
            if (r != (0 < (AAP[i].gameObject.transform.position.x - from.gameObject.transform.position.x))) 
            {
                AAP.RemoveAt(i);
            }

        }
        if (AAP.Count < 0) return null;
        var idx = Random.Range(0, AAP.Count);
        if (idx < 0 || AAP.Count <= idx) return null;
        return AAP[idx];


    }
    /// <summary>
    /// function that provide Target body parts
    /// </summary>
    /// <param name="idx">-1 means random</param>
    /// <returns></returns>
    GameObject getTarget(int idx = -1)
    {
        if (target == null) return null;
        if (idx == -1)
        {
            if (core == null) return null;
            var tagg = target.getParts2(core);
            if (tagg == null) return null;
            return tagg.gameObject;
        }
        else 
        {
            var tagg = target.getParts(idx);
            if (tagg == null) return null;
            return tagg.gameObject;
        }
    }
}
