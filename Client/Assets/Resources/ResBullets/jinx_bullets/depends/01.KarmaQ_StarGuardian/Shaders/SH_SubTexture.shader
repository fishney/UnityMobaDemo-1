// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:45,x:34332,y:32545,varname:node_45,prsc:2|emission-795-OUT,alpha-4060-OUT;n:type:ShaderForge.SFN_Tex2d,id:8369,x:31975,y:32543,varname:node_8369,prsc:2,tex:3ef1fbbc90b062c41bca10ede1df9e5e,ntxv:0,isnm:False|TEX-6285-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6285,x:31764,y:32567,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_6285,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3ef1fbbc90b062c41bca10ede1df9e5e,ntxv:0,isnm:False;n:type:ShaderForge.SFN_VertexColor,id:6410,x:31975,y:32732,varname:node_6410,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7379,x:32369,y:32478,varname:node_7379,prsc:2|A-8369-R,B-6410-A;n:type:ShaderForge.SFN_Multiply,id:8084,x:32874,y:32524,varname:node_8084,prsc:2|A-7379-OUT,B-1906-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1906,x:32553,y:32680,ptovrint:False,ptlb:Intensity,ptin:_Intensity,varname:node_1906,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:3167,x:33265,y:32715,varname:node_3167,prsc:2|A-8084-OUT,B-2694-OUT;n:type:ShaderForge.SFN_DepthBlend,id:2694,x:33030,y:32852,varname:node_2694,prsc:2|DIST-1277-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1277,x:32758,y:32807,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_1277,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Multiply,id:769,x:33646,y:32633,varname:node_769,prsc:2|A-3167-OUT,B-9297-OUT;n:type:ShaderForge.SFN_Fresnel,id:6897,x:33327,y:32957,varname:node_6897,prsc:2|EXP-7739-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7739,x:33080,y:33061,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:node_7739,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_SwitchProperty,id:9347,x:33832,y:32838,ptovrint:False,ptlb:HasFresnel?,ptin:_HasFresnel,varname:node_9347,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-3167-OUT,B-769-OUT;n:type:ShaderForge.SFN_OneMinus,id:9297,x:33517,y:32957,varname:node_9297,prsc:2|IN-6897-OUT;n:type:ShaderForge.SFN_Multiply,id:795,x:33947,y:32533,varname:node_795,prsc:2|A-769-OUT,B-6410-RGB,C-6410-A;n:type:ShaderForge.SFN_Multiply,id:4060,x:34054,y:32800,varname:node_4060,prsc:2|A-9347-OUT,B-6410-A;proporder:6285-1906-1277-7739-9347;pass:END;sub:END;*/

Shader "Unlit/Sub" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Intensity ("Intensity", Float ) = 2
        _Depth ("Depth", Float ) = 0
        _FresnelExp ("FresnelExp", Float ) = 0.5
        [MaterialToggle] _HasFresnel ("HasFresnel?", Float ) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 100
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _CameraDepthTexture;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float _Intensity;
            uniform float _Depth;
            uniform float _FresnelExp;
            uniform fixed _HasFresnel;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
////// Lighting:
////// Emissive:
                float4 node_8369 = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float node_3167 = (((node_8369.r*i.vertexColor.a)*_Intensity)*saturate((sceneZ-partZ)/_Depth));
                float node_769 = (node_3167*(1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)));
                float3 emissive = (node_769*i.vertexColor.rgb*i.vertexColor.a);
                float3 finalColor = emissive;
                return fixed4(finalColor,(lerp( node_3167, node_769, _HasFresnel )*i.vertexColor.a));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
