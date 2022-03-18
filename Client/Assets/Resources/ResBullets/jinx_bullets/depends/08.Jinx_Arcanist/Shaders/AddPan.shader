// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:7457,x:33484,y:32725,varname:node_7457,prsc:2|emission-4620-OUT;n:type:ShaderForge.SFN_Tex2d,id:1221,x:32045,y:32679,varname:node_1221,prsc:2,tex:fb09c85f3d69db849a572d364b4f8fe0,ntxv:0,isnm:False|TEX-2289-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:2289,x:31882,y:32679,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_2289,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:fb09c85f3d69db849a572d364b4f8fe0,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:9162,x:32052,y:32889,varname:node_9162,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-1131-OUT,TEX-7086-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:7086,x:31882,y:32889,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_7086,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2054,x:32396,y:32744,varname:node_2054,prsc:2|A-8558-OUT,B-6082-OUT;n:type:ShaderForge.SFN_Multiply,id:7117,x:32607,y:32833,varname:node_7117,prsc:2|A-2054-OUT,B-8263-RGB;n:type:ShaderForge.SFN_Color,id:8263,x:32396,y:32929,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8263,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:440,x:32810,y:32833,varname:node_440,prsc:2|A-7117-OUT,B-3061-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3061,x:32607,y:33051,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_3061,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:9373,x:31290,y:32980,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:node_8820,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:6605,x:31290,y:33075,ptovrint:False,ptlb:V_Speed,ptin:_V_Speed,varname:node_3475,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Append,id:7699,x:31500,y:33016,varname:node_7699,prsc:2|A-9373-OUT,B-6605-OUT;n:type:ShaderForge.SFN_Multiply,id:8805,x:31671,y:33016,varname:node_8805,prsc:2|A-7699-OUT,B-9569-T;n:type:ShaderForge.SFN_Time,id:9569,x:31500,y:33170,varname:node_9569,prsc:2;n:type:ShaderForge.SFN_Add,id:1131,x:31671,y:33170,varname:node_1131,prsc:2|A-8805-OUT,B-9573-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:9573,x:31290,y:33180,varname:node_9573,prsc:2,uv:0;n:type:ShaderForge.SFN_Power,id:6082,x:32240,y:32889,varname:node_6082,prsc:2|VAL-9162-RGB,EXP-9583-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9583,x:31989,y:33138,ptovrint:False,ptlb:MaskPower,ptin:_MaskPower,varname:node_9583,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Power,id:8558,x:32299,y:32472,varname:node_8558,prsc:2|VAL-1221-RGB,EXP-4238-OUT;n:type:ShaderForge.SFN_ValueProperty,id:4238,x:32038,y:32391,ptovrint:False,ptlb:MainPower,ptin:_MainPower,varname:node_4238,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:4620,x:33185,y:32812,varname:node_4620,prsc:2|A-4334-RGB,B-4334-A,C-440-OUT;n:type:ShaderForge.SFN_VertexColor,id:4334,x:32914,y:32984,varname:node_4334,prsc:2;proporder:2289-7086-8263-3061-9373-6605-9583-4238;pass:END;sub:END;*/

Shader "Custom/AddPan" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _Intensity ("Intensity", Float ) = 1
        _U_Speed ("U_Speed", Float ) = 0.1
        _V_Speed ("V_Speed", Float ) = 0.15
        _MaskPower ("MaskPower", Float ) = 4
        _MainPower ("MainPower", Float ) = 2
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _Color;
            uniform float _Intensity;
            uniform float _U_Speed;
            uniform float _V_Speed;
            uniform float _MaskPower;
            uniform float _MainPower;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_1221 = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float4 node_9569 = _Time + _TimeEditor;
                float2 node_1131 = ((float2(_U_Speed,_V_Speed)*node_9569.g)+i.uv0);
                float4 node_9162 = tex2D(_Mask,TRANSFORM_TEX(node_1131, _Mask));
                float3 emissive = (i.vertexColor.rgb*i.vertexColor.a*(((pow(node_1221.rgb,_MainPower)*pow(node_9162.rgb,_MaskPower))*_Color.rgb)*_Intensity));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
