using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour,IPoolControl  //给物体挂上的，接口可加可不加
{

    void Update()
    {
        
    }
    //取出物体时的操作
    public void Spawn(){
        Debug.Log("Spawn!");
    }

    //获得物体时的操作
    public void UnSpawn(){
        Debug.Log("UnSpawn!");
    }

    //删除所有的gameObject时用的
    public void DestroyByPool(){
        Destroy(this.gameObject);
    }

}
