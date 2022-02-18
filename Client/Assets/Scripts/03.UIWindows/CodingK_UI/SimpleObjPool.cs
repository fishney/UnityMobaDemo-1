using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodingK.UI
{
    public interface IPoolObj<T>
    {
        /// <summary>
        /// 创建时操作
        /// </summary>
        void Init();
        
        /// <summary>
        /// 销毁时操作
        /// </summary>
        void UnInit();
        
        /// <summary>
        /// 复用时的处理，数据层更新显示层，一般需要gameObject.SetActive(true)！
        /// </summary>
        void Load(T t);
        
        /// <summary>
        /// 回池时的处理，一般需要gameObject.SetActive(false)！
        /// </summary>
        void UnLoad();
    }
    
    /// <summary>
    /// 对象池
    /// </summary>
    /// <typeparam name="T">物品 数据层</typeparam>
    /// <typeparam name="K">物品 显示层</typeparam>
    public class SimpleObjPool<T,K> where K : IPoolObj<T>
    {
        private Transform poolRoot;
        private Queue<K> queue;
        private GameObject prefab;
    
        int _index = 0;
        int Index { get { return ++_index; } }
    
        public SimpleObjPool(int capacity, Transform poolRoot, GameObject prefab)
        {
            this.poolRoot = poolRoot;
            this.prefab = prefab;
            queue = new Queue<K>();
            
            for (int i = 0; i < capacity; i++)
            {
                PushOne(CreateOne());
            }
        }

        /// <summary>
        /// 创建、初始化并返回一个预制体
        /// </summary>
        K CreateOne()
        {
            GameObject go = GameObject.Instantiate(prefab);
            go.name = "SimpleObj_" + Index;
            go.transform.SetParent(poolRoot);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(false);
            K jn = go.GetComponent<K>();
            jn.Init();
            return jn;
        }
    
        /// <summary>
        /// 取出对象，且预加载
        /// </summary>
        public K PopOne(T t) {
            if(queue.Count > 0)
            {
                var newObj = queue.Dequeue();
                newObj.Load(t);
                return newObj;
            }
            else {
                Debug.Log(typeof(K) + "动态调整上限");
                PushOne(CreateOne());
                return PopOne(t);
            }
        }
        
        /// <summary>
        /// 取出对象
        /// </summary>
        public K PopOne() {
            if(queue.Count > 0)
            {
                var newObj = queue.Dequeue();
                return newObj;
            }
            else {
                Debug.Log(typeof(K) + "动态调整上限");
                PushOne(CreateOne());
                return PopOne();
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public void PushOne(K jn) {
            jn.UnLoad();
            queue.Enqueue(jn);
        }

        /// <summary>
        /// 回收并销毁所有对象
        /// </summary>
        public void ClearPool()
        {
            var obj = queue.Dequeue();
            while (obj != null)
            {
                obj.UnLoad();
                obj.UnInit();
                obj = queue.Dequeue();
            }

            queue = null;
            prefab = null;
            poolRoot = null;
        }
    }
}