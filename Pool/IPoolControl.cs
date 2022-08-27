using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolControl
{
    //可以挂在也可以不挂
    //取出物体时的操作
    public void Spawn();

    //获得物体时的操作
    public void UnSpawn();

    //删除所有的gameObject时用的
    public void DestroyByPool();

}
