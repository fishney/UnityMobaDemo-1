using System;
using System.Collections.Generic;
using UnityEngine;

namespace CodingK.UI
{
    /// <summary>
    /// 滚动列表，不依赖于Mono，由外部驱动
    /// </summary>
    /// <typeparam name="T">物品 数据层</typeparam>
    /// <typeparam name="K">物品 显示层</typeparam>
    public class CodingK_SV<T,K> where K : MonoBehaviour, IPoolObj<T>
    {
        // 数据源
        private List<T> dataList = new List<T>();
        
        // 履带 Content
        private RectTransform content;
        // 可视范围高 ScrollViewer
        private RectTransform view;
        public int viewPortH;
        public int viewPortW;

        public bool isShowing;

        #region 物品格子配置
    
        // 宽高
        public int childWidth = 100;
        public int childHeight = 100;
        // padding
        public int paddingWidth = 45;
        public int paddingHeight = 25;
        // 一行几个
        public int oneRowColumns = 5;
    
        #endregion
        
        // 当前显示的格子对象
        public Dictionary<int, K> showingItems = new Dictionary<int, K>();
        private SimpleObjPool<T, K> objPool;
        private GameObject itemPrefab;
        private int oldMinIndex;
        private int oldMaxIndex;
        private int index_MinValue;
        private int index_MaxValue;

        public CodingK_SV(List<T> data, RectTransform content, RectTransform scrollViewer, GameObject itemPrefab, int poolCapacity = 100)
        {
            dataList = data;
            this.content = content;
            this.view = scrollViewer;
            this.itemPrefab = itemPrefab;

            this.objPool = new SimpleObjPool<T, K>(poolCapacity, content, itemPrefab);
                
            RefreshPanelSize();
        }

        public void InitItemView(int paddingWidth = -1, int paddingHeight = -1)
        {
            var rect = itemPrefab.GetComponent<RectTransform>().rect;
            this.childWidth = (int)rect.width;
            this.childHeight = (int)rect.height;
            
            this.paddingWidth = paddingWidth >= 0 ? paddingWidth : childWidth / 4;
            this.paddingHeight = paddingHeight >= 0 ? paddingHeight : childHeight / 4;
            this.oneRowColumns = ((int)view.rect.width + paddingWidth) / (this.childWidth + paddingWidth);
        }
        
        /// <summary>
        /// 当数据量改变时调用
        /// </summary>
        public void RefreshPanelSize()
        {
            viewPortH = (int)view.rect.height;
            viewPortW = (int)view.rect.width;
            
            index_MinValue = 0;
            index_MaxValue = dataList.Count - 1;
        }
        
        public void Show()
        {
            content.sizeDelta = new Vector2(0,
                Mathf.CeilToInt((float)dataList.Count / (float)oneRowColumns ) * (childHeight + paddingHeight) - paddingHeight);
            isShowing = true;
        }

        public void Destroy()
        {
            isShowing = false;
            Clear();
        }
        
        private void Clear()
        {
            foreach (var go in showingItems.Values)
            {
                objPool.PushOne(go);
            }
            content.localPosition = Vector3.zero;

            showingItems.Clear();
            dataList = null;
            showingItems = null;
            content = null;
            view = null;
            itemPrefab = null;
        }
        
        /// <summary>
        /// 外部驱动器
        /// </summary>
        public void Tick()
        {
            // 防止越下界
            if (content.anchoredPosition.y < 0)
            {
                return;
            }
        
            CheckShowOrHide();

            foreach (var item in showingItems.Values)
            {
                
            }
        }
        
        private void CheckShowOrHide() 
        {
            // 检测哪些要显示
            // 左上索引
            int minIndex = (int)(content.anchoredPosition.y / (childHeight + paddingHeight)) * oneRowColumns;
            // 右下索引,+向上取整
            int maxIndex = Mathf.CeilToInt((content.anchoredPosition.y + viewPortH ) / (childHeight + paddingHeight)) * oneRowColumns - 1;
            // 防止越上界,不能超出道具总数量
            maxIndex = Math.Min(maxIndex, index_MaxValue);

            // 根据上一次索引和这一次新算出来的索引，来判断哪些该移除
            for (int i = oldMinIndex; i < minIndex; ++i)
            {
                if (showingItems.ContainsKey(i))
                {
                    var item = showingItems[i];
                    if (item != null)
                    {
                        // 对象池回收
                        showingItems.Remove(i);
                        objPool.PushOne(item);
                        
                    }
                }
            }
            
            for (int i = maxIndex + 1; i < oldMaxIndex; ++i)
            {
                if (showingItems.ContainsKey(i))
                {
                    var item = showingItems[i];
                    if (item != null)
                    {
                        // 对象池回收
                        showingItems.Remove(i);
                        objPool.PushOne(item);
                        
                    }
                }
            }
            
            oldMinIndex = minIndex;
            oldMaxIndex = maxIndex;
            
            for (int i = minIndex; i <= maxIndex; ++i)
            {

                if (showingItems.ContainsKey(i))
                {
                    continue;
                }
                int index = i;
                showingItems.Add(index, null);
                
                var go = objPool.PopOne(dataList[index]);

                go.transform.SetParent(content);
                go.transform.localScale = Vector3.one;
                // 算出它在履带上的位置就行，虚拟画面可视化那是ScrollerView实现的：
                go.transform.localPosition = new Vector3((index % oneRowColumns) * (childWidth + paddingWidth), - (index / oneRowColumns) * (childHeight + paddingHeight),0);
                // 更新格子信息
                if (showingItems.ContainsKey(index))
                {
                    showingItems[index] = go;  
                }
                else
                {
                    // 异步加载情况下,要将对象放回返回池
                    objPool.PushOne(go);
                }
            }
        
        }
    }
    
    
}