using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 专门为GameRoot下的继承MonoBehaviour的单例脚本做的 简易单例，必须是GameRoot挂载的脚本！
/// </summary>
/// <typeparam name="T"></typeparam>
public class GameRootMonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
	protected GameRootMonoSingleton()
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
        
		// // 获取非public类的构造方法
		// ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);
		// // 从上述构造方法集中获取无参的构造方法
		// ConstructorInfo nullConstructor = Array.Find(constructors, c => c.GetParameters().Length == 0);
		// // 不存在无参的构造方法就报错
		// if (nullConstructor == null)
		// 	throw new Exception("Non-public ConstructorInfo new() not found!");
		// // 存在无参的构造方法就调用该方法
		// _instance = nullConstructor.Invoke(null) as T;
		// return _instance;

		_instance = GameObject.Find("GameRoot").GetComponent<T>();
		if (_instance == null)
		{
			throw new Exception(typeof(T).Name + " not found in GameRoot!");
		}
		return _instance;
	}
	
	//
	// private static T _instance;
	// public static T Instance
	// {
	// 	get
	// 	{
	// 		if(MonoSingletonObject.go == null)
	// 		{
	// 			MonoSingletonObject.go = new GameObject("MonoSingletonObject");
	// 			Object.DontDestroyOnLoad(MonoSingletonObject.go);
	// 		}
	//
	// 		if(_instance == null)
	// 		{
	// 			_instance = MonoSingletonObject.go.AddComponent<T>();
	// 		}
	//
	// 		return _instance;
	// 	}
	// }

	// Destory when Scene changed
	// public static bool IsDestoryOnLoad;
	//
	// public void AddSceneChangeEvent()
	// {
	// 	SceneManager.activeSceneChanged += OnSceneChanged;
	// }
	//
	// private void OnSceneChanged(Scene s1,Scene s2)
	// {
	// 	if (IsDestoryOnLoad)
	// 	{
	// 		if (_instance != null)
	// 		{
	// 			GameObject.DestroyImmediate(_instance);
	// 		}
	// 	}
	// }

}

// Cache GameObject
public class MonoSingletonObject
{
	public static GameObject go;
}