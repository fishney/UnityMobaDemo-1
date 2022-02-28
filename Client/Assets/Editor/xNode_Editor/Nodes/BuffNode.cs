using XNode;

namespace Editor.xNode_Editor
{
    [NodeWidth(NodeHelper.BuffWidth)]
    public class BuffNode : Node
    {
        [Input(ShowBackingValue.Always)]
        public int BuffId;

        #region 属性
        public string buffName;
    
        /// <summary>
        /// buff类型，用来创建不同类型的buff
        /// </summary>
        public BuffTypeEnum buffType;
        
        /// <summary>
        /// buff附着目标
        /// </summary>
        public AttachTypeEnum attacher;
        public StaticPosTypeEnum staticPosType;
    
        /// <summary>
        /// buff作用目标，如果为null默认影响附着对象
        /// </summary>
        public TargetCfg impacter;

        #endregion
    
        #region 效果相关

        public int buffDelay;
        /// <summary>
        /// buff效果触发频率(比如持续1秒1次)，如果为0就只调用Start，LogicTick不会调用到Tick
        /// </summary>
        public int buffInterval;
        /// <summary>
        /// buff持续时间（不包含delay）0：生效1次，-1：永久生效
        /// </summary>
        public int buffDuration;

        #endregion

        #region 配置

        public string buffAudio;
        public string buffEffect;
        public string hitTickAudio;

        #endregion
        	
        protected override void Init() {
            base.Init();
        }
        	
        // Return the correct value of an output port when requested
        public override object GetValue(NodePort port)
        {
            return null; // Replace this
        }
        
        public override void OnCreateConnection(NodePort from, NodePort to)
        {
            base.OnCreateConnection(from,to);
        }

        private void OnValidate()
        {
            
            var linkedNodes = GetInputPort("BuffId")?.GetConnections();
            foreach (var linkedNode in linkedNodes)
            {
                var preNode = linkedNode?.node as SkillNode;
                if (preNode != null)
                {
                    var index = linkedNode.fieldName.GetIndex();
                    if (index >= 0)
                    {
                        preNode.buffIdArr[index] = this.BuffId;
                    }
                }
            }
        }
    }
}