//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Bright.Serialization;

namespace proto.HOKProtocol
{

    public  sealed class HeroSelectData :  Bright.Serialization.BeanBase 
    {
        public HeroSelectData()
        {
        }

        public HeroSelectData(Bright.Common.NotNullInitialization _) 
        {
        }

        public static void SerializeHeroSelectData(ByteBuf _buf, HeroSelectData x)
        {
            x.Serialize(_buf);
        }

        public static HeroSelectData DeserializeHeroSelectData(ByteBuf _buf)
        {
            var x = new HOKProtocol.HeroSelectData();
            x.Deserialize(_buf);
            return x;
        }

         public int heroID;


        public const int __ID__ = 0;
        public override int GetTypeId() => __ID__;

        public override void Serialize(ByteBuf _buf)
        {
            _buf.WriteInt(heroID);
        }

        public override void Deserialize(ByteBuf _buf)
        {
            heroID = _buf.ReadInt();
        }

        public override string ToString()
        {
            return "HOKProtocol.HeroSelectData{ "
            + "heroID:" + heroID + ","
            + "}";
        }
    }

}
