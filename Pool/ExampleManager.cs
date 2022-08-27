using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleManager : MonoBehaviour   //给管理prefab的挂上的
{
    //在inspector里面赋值也行，Resources也行
    public GameObject obj;
    public GameObject exampleObj;
    // Start is called before the first frame update
    void Start()
    {
        obj = Resources.Load("example") as GameObject;
        PoolManager.instance.PerSpawn(obj,10,transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            exampleObj=PoolManager.instance.Spawn(obj);
        }
        if(Input.GetKeyDown(KeyCode.W)){
            PoolManager.instance.UnSpawn(exampleObj);
        }
        if(Input.GetKeyDown(KeyCode.E)){
            PoolManager.instance.UnSpawn(exampleObj);
        }
        if(Input.GetKeyDown(KeyCode.R)){
            PoolManager.instance.UnSpawnAll(obj.name);
        }
        if(Input.GetKeyDown(KeyCode.T)){
            PoolManager.instance.ClearPool(obj.name);
        }
    }
}
