using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

// public class Singleton<T> where T : new()
// {
// 	private static T _instance;
// 	public static T Instance
// 	{
// 		get
// 		{
// 			if(_instance == null)
// 			{
// 				_instance = new T();
// 			}
// 			return _instance;
// 		}
// 	}
// }

/// <summary>
/// 反射原理实现简易单例，需要Non-public new()
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class Singleton<T> where T : Singleton<T>
{
    protected Singleton()
    {
        
    }

    private static T _instance = null;
    
    /// <summary>
    /// 获取实例
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static T Instance()
    {
        if (_instance != null) return _instance;
        
        // 获取非public类的构造方法
        ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
        // 从上述构造方法集中获取无参的构造方法
        ConstructorInfo nullConstructor = Array.Find(constructors, c => c.GetParameters().Length == 0);
        // 不存在无参的构造方法就报错
        if (nullConstructor == null)
            throw new Exception("Non-public ConstructorInfo new() not found!");
        // 存在无参的构造方法就调用该方法
        _instance = nullConstructor.Invoke(null) as T;
        return _instance;
    }
}
