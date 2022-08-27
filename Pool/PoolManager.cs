using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager{
    //单列模式
    static PoolManager _instance;
    public static PoolManager instance{
        get{
            if(_instance == null)
                _instance = new PoolManager(); //没法用this，因为this是对应每个类的对象的; 
            return _instance;
        }
    }
    //储存各种类型的对象池的集合
    Dictionary<string,SubPool> poolDictionary = new Dictionary<string, SubPool>();

    //预先加载好若干对象
    public void PerSpawn(GameObject obj,int number){
        if(!poolDictionary.ContainsKey(obj.name))
            Register(obj);
        SubPool subPool = poolDictionary[obj.name];
        subPool.PerSpawn(number);
    }

    //预先加载好若干对象带参数的重载
    public void PerSpawn(GameObject obj,int number,Transform parent){
        if(!poolDictionary.ContainsKey(obj.name))
            Register(obj);
        SubPool subPool = poolDictionary[obj.name];
        subPool.PerSpawn(number,parent);
    }

    //添加SubPool进入对象池
    void Register(GameObject obj){
        SubPool subPool = new SubPool(obj);  //这里给subPool对应的perfab名称
        poolDictionary.Add(obj.name,subPool);
    }

    //获得对象池中游戏对象
    //到时候直接在新代码中调用的函数
    //代码中需要先Resources.Load(string)一个对象，传到这里的代码
    public GameObject Spawn(GameObject obj,Transform parent){     
        if(!poolDictionary.ContainsKey(obj.name))
            Register(obj);
        SubPool subPool = poolDictionary[obj.name];
        return subPool.SubPoolSpawn(parent);
    }
    
    //无参数的重载
    public GameObject Spawn(GameObject obj){     
        if(!poolDictionary.ContainsKey(obj.name))
            Register(obj);
        SubPool subPool = poolDictionary[obj.name];
        return subPool.SubPoolSpawn();
    }

    //回收游戏对象,第二个判断是否调用UnSpawn的接口
    //如果返回的是false，那就在gameObject里面Destory就行
    public bool UnSpawn(GameObject obj){
        //判断是否是pool的对象
        if(poolDictionary.ContainsKey(obj.name)){
            SubPool subPool = poolDictionary[obj.name];
                if(subPool.subPoolContains(obj)){
                    subPool.SubPoolUnSpawn(obj);
                    return true;
                }   
                else
                    Debug.Log("Did not exist in pool");
                    return false;
        }
        else{
            Debug.Log("Did not exist in pool");
            return false;
        }

    }
    //回收所有游戏对象,第二个判断是否调用UnSpawn的接口
    public void UnSpawnAll(string name){
        if(poolDictionary.ContainsKey(name))
            poolDictionary[name].SubPoolUnSpawnAll();
    }
    //清除游戏对象
    public void ClearPool(string name){
        if(poolDictionary.ContainsKey(name)){
            poolDictionary[name].ClearPool();
        }
    }
}
