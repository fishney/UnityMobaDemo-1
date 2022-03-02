using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace Editor.xNode_Editor
{
    [CreateNodeMenu("new SkillNode"),NodeWidth(NodeHelper.SkillWidth)]
    public class SkillNode : Node
    {
        
        [Input(ShowBackingValue.Always)]public int skillId;
        /// 技能图标
        public string iconName;
        /// 施法动画
        public string aniName;

        /// 施法开始音效
        public string audio_start;
        /// 施法成功音效
        public string audio_work;
        /// 施法命中音效
        public string audio_hit;
    
        /// CD时间 ms
        public int cdTime;
        /// 施法时间(前摇) ms
        public int spellTime;
        /// 技能全长时间(前摇+后摇) ms
        /// 后摇动作均可被移动中断，但技能总时间不能变短
        public int skillTime;
    
        /// 基础伤害数值
        public int damage;
        /// 附加Buff
        [Node.OutputAttribute(dynamicPortList = true)]
        public int[] buffIdArr;
    
        /// 是否为普通攻击
        public bool isNormalAttack;
        /// 释放方式
        public cfg.ReleaseModeEnum releaseMode;
        /// 目标选择配置,null为非锁定弹道技能
        public TargetCfg targetCfg;
        /// 弹道配置，无弹道就为null
        public BulletCfg bulletCfg;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName.Contains("buffIdArr "))
            {
                var index = port.fieldName.GetIndex();
                if (index >= 0 && index < buffIdArr.Length)
                {
                    return buffIdArr[index];
                }
            }
            return null; // Replace this
        }
        
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            if (from.fieldName.Contains("buffIdArr "))
            {
                var index = from.fieldName.GetIndex();
                if (index >= 0)
                {
                    buffIdArr[index] = (to.node as BuffNode).BuffId;
                }
            }
        }
        
        private void OnValidate()
        {
            var linkedNodes = GetInputPort("skillId")?.GetConnections();
            foreach (var linkedNp in linkedNodes)
            {
                if (linkedNp?.node is UnitNode preNode)
                {
                    var index = linkedNp.fieldName.GetIndex();
                    if (index >= 0)
                    {
                        if (linkedNp.fieldName.Contains("pasvBuff "))
                        {
                            preNode.pasvBuff[index] = this.skillId;
                        }
                        else if(linkedNp.fieldName.Contains("skillArr "))
                        {
                            preNode.skillArr[index] = this.skillId; 
                        }
                        
                    }
                }
            }
        }
    }
}