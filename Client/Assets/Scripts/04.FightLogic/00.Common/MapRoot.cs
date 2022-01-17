/****************************************************
    文件：MapRoot.cs
    作者：CodingCodingK
    博客：CodingCodingK.github.io
    邮箱：2470939431@qq.com
    日期：#DATE#
    功能：Unknown
*****************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRoot : MonoBehaviour
{
    public Transform transCameraRoot;
    public Transform transEnvCollider;

    public Transform blueTower;
    public Transform redTower;
    public Transform blueCrystal;
    public Transform redCrystal;
    
    // 毁灭
    public Transform desBlueTower;
    public Transform desRedTower;
    public Transform desBlueCrystal;
    public Transform desRedCrystal;
    
    public void DestroyBlueTower() {
        blueTower.gameObject.SetActive(false);
        desBlueTower.gameObject.SetActive(true);
    }

    public void DestroyRedTower() {
        redTower.gameObject.SetActive(false);
        desRedTower.gameObject.SetActive(true);
    }
    public void DestroyBlueCrystal() {
        //blueCrystal.gameObject.SetActive(false);
        desBlueCrystal.gameObject.SetActive(true);
    }
    public void DestroyRedCrystal() {
        //redCrystal.gameObject.SetActive(false);
        desRedCrystal.gameObject.SetActive(true);
    }
}
