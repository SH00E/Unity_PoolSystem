using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPool
{
    //先进先出，比较贴切
    List<GameObject> pool = new List<GameObject>();
    //预设的名字
    GameObject prefab;
    //绑定预制体
    public SubPool(GameObject obj){
        prefab = obj;
    }
    public string name{
        get => prefab.name;
    }
    //预先加载若干对象
    public void PerSpawn(int number){
        while(pool.Count < number){
            GameObject obj = GameObject.Instantiate(prefab);
            //更改名称，否则会导致后面有个(Clone)
            obj.name = name;
            obj.SetActive(false);
            pool.Add(obj);
        }
    }
    //预先加载若干对象(带参数重载)
    public void PerSpawn(int number,Transform parent){
        while(pool.Count < number){
            GameObject obj = GameObject.Instantiate(prefab);
            //更改名称，否则会导致后面有个(Clone)
            obj.name = name;
            obj.transform.SetParent(parent);
            obj.SetActive(false);
            pool.Add(obj);
        }
    }

    //无参数的加载
    public GameObject SubPoolSpawn(){
        GameObject obj = null;
        foreach(GameObject item in pool){  //找到没在用的
            if(item.activeSelf == false){
                obj = item;
                break;   
            }
 
        }
        if(obj == null){
            obj = GameObject.Instantiate(prefab);
            //更改名称，否则会导致后面有个(Clone)
            obj.name = name;
            pool.Add(obj);
        }
        obj.SetActive(true);
        //调用接口
        IPoolControl poolControl = obj.GetComponent<IPoolControl>();//GetComponent方法是可以直接获得接口的
        poolControl?.Spawn();
        return obj;
    }
    //带参数的加载
    public GameObject SubPoolSpawn(Transform parent){
        GameObject obj = null;
        foreach(GameObject item in pool){  //找到没在用的
            if(item.activeSelf == false){
                obj = item;
                break;   
            }
        }
        if(obj == null){
            obj = GameObject.Instantiate(prefab);
            //更改名称，否则会导致后面有个(Clone)
            obj.name = name;
            pool.Add(obj);
        }
        obj.transform.SetParent(parent);
        obj.SetActive(true);
        //调用接口
        IPoolControl poolControl = obj.GetComponent<IPoolControl>();//GetComponent方法是可以直接获得接口的
        poolControl?.Spawn();
        return obj;
    }
    //对象是否在pool中
    public bool subPoolContains(GameObject obj){
        if(pool.Contains(obj))
            return true;
        return false;
    }
    //回收物体
    public void SubPoolUnSpawn(GameObject obj){
        //执行相关的回收接口
        IPoolControl poolControl = obj.GetComponent<IPoolControl>();//GetComponent方法是可以直接获得接口的
        poolControl?.UnSpawn();
        obj.SetActive(false);

    }
    //回收所有物体
    public void SubPoolUnSpawnAll(){
        foreach(GameObject obj in pool){
            SubPoolUnSpawn(obj);
        }
    }

    //清除所有对象
    public void ClearPool(){
        foreach(GameObject obj in pool){
            //如果挂在了会调用Destroy，否则不调用
            IPoolControl poolControl = obj.GetComponent<IPoolControl>();//GetComponent方法是可以直接获得接口的
            poolControl?.DestroyByPool();
        }
        pool.Clear();
    }
}
