/****************************************************
    文件：PlayWindow.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using PEMath;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayWindow : WindowBase
{
    public Image imgCancelSkill;
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    public Transform ArrowRoot;
    float pointDis = 135;
    Vector2 startPos = Vector2.zero;
    Vector2 defaultPos = Vector2.zero;
    
    protected override void InitWindow()
    {
        base.InitWindow();
        SetActive(ArrowRoot, false);
        pointDis = Screen.height * 1.0f / ClientConfig.ScreenStandardHeight * ClientConfig.ScreenOPDis;
        defaultPos = imgDirBg.transform.position;

        RegisterMoveEvts();
    }
    
   
    
    protected override void ClearWindow()
    {
        base.ClearWindow();
    }
    

    private Vector2 lastKeyDir = Vector2.zero;
    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector2 keyDir = new Vector2(h, v);
        if (keyDir != lastKeyDir)
        {
            if (h != 0 || v != 0)
            {
                keyDir = keyDir.normalized;
            }
            InputMoveKey(keyDir);
            lastKeyDir = keyDir;
        }
    }

  
    
    void RegisterMoveEvts() {
        SetActive(ArrowRoot, false);

        OnClickDown(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            startPos = evt.position;
            Debug.Log($"evt.pos:{evt.position}");
            imgDirPoint.color = new Color(1, 1, 1, 1f);
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            imgDirBg.transform.position = defaultPos;
            imgDirPoint.color = new Color(1, 1, 1, 0.5f);
            imgDirPoint.transform.localPosition = Vector2.zero;
            SetActive(ArrowRoot, false);

            InputMoveKey(Vector2.zero);
        });
        OnDrag(imgTouch.gameObject, (PointerEventData evt, object[] args) => {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;
            if(len > pointDis) {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);
                imgDirPoint.transform.position = startPos + clampDir;
            }
            else {
                imgDirPoint.transform.position = evt.position;
            }

            if(dir != Vector2.zero) {
                SetActive(ArrowRoot);
                float angle = Vector2.SignedAngle(new Vector2(1, 0), dir);
                ArrowRoot.localEulerAngles = new Vector3(0, 0, angle);
            }

            InputMoveKey(dir.normalized);
        });
    }
    
    private Vector2 lastStickDir = Vector2.zero;
    private void InputMoveKey(Vector2 dir)
    {
        if (!dir.Equals(lastStickDir))
        {
            // 适配相机，旋转角度45，使角色移动方向与地图的视觉统一
            Vector3 dirVector3 = new Vector3(dir.x, 0, dir.y);
            dirVector3 = Quaternion.Euler(0, 45, 0) * dirVector3;
            
            PEVector3 logicDir = PEVector3.zero;
            if (dir != Vector2.zero)
            {
                logicDir.x = (PEInt) dirVector3.x;
                logicDir.y = (PEInt) dirVector3.y;
                logicDir.z = (PEInt) dirVector3.z;
            }

            bool isSend = BattleSys.Instance.SendMoveKey(logicDir);
            if (isSend)
            {
                lastStickDir = dir;
            }
        }
    }
        
}
