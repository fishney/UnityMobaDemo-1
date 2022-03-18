// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.30 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.30;sub:START;pass:START;ps:flbk:Particles/Additive,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:1085,x:36426,y:32360,varname:node_1085,prsc:2|emission-8155-OUT,alpha-5344-OUT,refract-6848-OUT;n:type:ShaderForge.SFN_Tex2d,id:6236,x:31918,y:32741,varname:node_6236,prsc:2,tex:755a3d79299727e499dc066acd9484e4,ntxv:0,isnm:False|UVIN-5302-UVOUT,TEX-6733-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6733,x:31760,y:32741,ptovrint:False,ptlb:MainTexture,ptin:_MainTexture,varname:node_6733,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:755a3d79299727e499dc066acd9484e4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:529,x:32139,y:32741,varname:node_529,prsc:2|A-6236-RGB,B-4974-RGB,C-3623-RGB,D-3623-A;n:type:ShaderForge.SFN_Color,id:4974,x:31918,y:32898,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_4974,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_VertexColor,id:3623,x:31918,y:33050,varname:node_3623,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8336,x:32139,y:32898,varname:node_8336,prsc:2|A-6236-R,B-4974-A,C-3623-A;n:type:ShaderForge.SFN_Multiply,id:7726,x:32487,y:32745,varname:node_7726,prsc:2|A-1637-OUT,B-529-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1637,x:32253,y:32598,ptovrint:False,ptlb:EmissiveIntensity,ptin:_EmissiveIntensity,varname:node_1637,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_Multiply,id:5136,x:32487,y:32928,varname:node_5136,prsc:2|A-8336-OUT,B-1906-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1906,x:32245,y:33141,ptovrint:False,ptlb:OpacityIntensity,ptin:_OpacityIntensity,varname:node_1906,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_SwitchProperty,id:251,x:32787,y:32652,ptovrint:False,ptlb:HasNoiseEmissive?,ptin:_HasNoiseEmissive,varname:node_251,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-7726-OUT,B-5083-OUT;n:type:ShaderForge.SFN_Tex2d,id:5766,x:32568,y:32434,varname:node_5766,prsc:2,tex:dc664e2d56996f844ad09d81f5e27fe2,ntxv:0,isnm:False|UVIN-1800-OUT,TEX-5958-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5958,x:32408,y:32434,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_5958,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:dc664e2d56996f844ad09d81f5e27fe2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5083,x:32568,y:32565,varname:node_5083,prsc:2|A-2541-OUT,B-7726-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7197,x:32021,y:32072,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:node_8820,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:7662,x:32021,y:32167,ptovrint:False,ptlb:V_Speed,ptin:_V_Speed,varname:node_3475,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Append,id:7346,x:32231,y:32108,varname:node_7346,prsc:2|A-7197-OUT,B-7662-OUT;n:type:ShaderForge.SFN_Multiply,id:1706,x:32402,y:32108,varname:node_1706,prsc:2|A-7346-OUT,B-3262-T;n:type:ShaderForge.SFN_Time,id:3262,x:32231,y:32262,varname:node_3262,prsc:2;n:type:ShaderForge.SFN_Add,id:1800,x:32402,y:32262,varname:node_1800,prsc:2|A-1706-OUT,B-6388-OUT;n:type:ShaderForge.SFN_Power,id:2541,x:32745,y:32462,varname:node_2541,prsc:2|VAL-5766-RGB,EXP-2891-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2891,x:32568,y:32364,ptovrint:False,ptlb:NoisePower,ptin:_NoisePower,varname:node_2891,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Multiply,id:7204,x:32688,y:33006,varname:node_7204,prsc:2|A-2189-OUT,B-5136-OUT;n:type:ShaderForge.SFN_ComponentMask,id:2189,x:32987,y:32434,varname:node_2189,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-5593-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:6806,x:32849,y:32874,ptovrint:False,ptlb:HasNoiseOpacity?,ptin:_HasNoiseOpacity,varname:node_6806,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5136-OUT,B-7204-OUT;n:type:ShaderForge.SFN_Multiply,id:5785,x:33052,y:32915,varname:node_5785,prsc:2|A-6806-OUT,B-7176-OUT;n:type:ShaderForge.SFN_DepthBlend,id:7176,x:33067,y:33085,varname:node_7176,prsc:2|DIST-8558-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8558,x:32913,y:33173,ptovrint:False,ptlb:Depth,ptin:_Depth,varname:node_8558,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_SwitchProperty,id:5291,x:33227,y:32825,ptovrint:False,ptlb:HasOpacityDepth?,ptin:_HasOpacityDepth,varname:node_5291,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6806-OUT,B-5785-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1427,x:33288,y:32588,ptovrint:False,ptlb:HasEmissiveDepth?,ptin:_HasEmissiveDepth,varname:node_1427,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-251-OUT,B-5834-OUT;n:type:ShaderForge.SFN_Multiply,id:5834,x:33115,y:32642,varname:node_5834,prsc:2|A-251-OUT,B-7176-OUT;n:type:ShaderForge.SFN_ScreenPos,id:8110,x:31663,y:32237,varname:node_8110,prsc:2,sctp:0;n:type:ShaderForge.SFN_Fresnel,id:6776,x:33296,y:33206,varname:node_6776,prsc:2|EXP-7935-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7935,x:33130,y:33240,ptovrint:False,ptlb:FresnelExp,ptin:_FresnelExp,varname:node_7935,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_SwitchProperty,id:4760,x:33726,y:32797,ptovrint:False,ptlb:HasOpacityFresnel?,ptin:_HasOpacityFresnel,varname:node_4760,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5291-OUT,B-6166-OUT;n:type:ShaderForge.SFN_Multiply,id:6166,x:33522,y:32934,varname:node_6166,prsc:2|A-5291-OUT,B-8690-OUT;n:type:ShaderForge.SFN_OneMinus,id:3651,x:33460,y:33206,varname:node_3651,prsc:2|IN-6776-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:2215,x:33726,y:32581,ptovrint:False,ptlb:HasEmissiveFresnel?,ptin:_HasEmissiveFresnel,varname:node_2215,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-1427-OUT,B-9528-OUT;n:type:ShaderForge.SFN_Multiply,id:9528,x:33515,y:32664,varname:node_9528,prsc:2|A-1427-OUT,B-8690-OUT;n:type:ShaderForge.SFN_Power,id:8690,x:33660,y:33206,varname:node_8690,prsc:2|VAL-3651-OUT,EXP-1027-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1027,x:33460,y:33381,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_1027,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.2;n:type:ShaderForge.SFN_Multiply,id:5344,x:34795,y:32707,varname:node_5344,prsc:2|A-2788-OUT,B-4760-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2788,x:34615,y:32648,ptovrint:False,ptlb:FinalOpacity,ptin:_FinalOpacity,varname:node_2788,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_SwitchProperty,id:6388,x:31972,y:32359,ptovrint:False,ptlb:Is_ScreenSpace?,ptin:_Is_ScreenSpace,varname:node_6388,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6475-OUT,B-8110-UVOUT;n:type:ShaderForge.SFN_Multiply,id:5584,x:34795,y:32569,varname:node_5584,prsc:2|A-1249-OUT,B-2788-OUT;n:type:ShaderForge.SFN_Multiply,id:2725,x:34135,y:32354,varname:node_2725,prsc:2|A-6228-OUT,B-2215-OUT;n:type:ShaderForge.SFN_SwitchProperty,id:1249,x:34371,y:32460,ptovrint:False,ptlb:HasColorGradient?,ptin:_HasColorGradient,varname:node_1249,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2215-OUT,B-2725-OUT;n:type:ShaderForge.SFN_Tex2d,id:6774,x:33412,y:32013,varname:node_6774,prsc:2,tex:e81d9a5b1fe0414489324c1a2e05e207,ntxv:0,isnm:False|UVIN-5302-UVOUT,TEX-5322-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5322,x:33248,y:32013,ptovrint:False,ptlb:ColorGradient,ptin:_ColorGradient,varname:node_5322,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:e81d9a5b1fe0414489324c1a2e05e207,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:6228,x:33792,y:32052,ptovrint:False,ptlb:HasColorGradientMask?,ptin:_HasColorGradientMask,varname:node_6228,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-6774-RGB,B-6641-OUT;n:type:ShaderForge.SFN_Tex2d,id:2563,x:33134,y:32211,varname:node_2563,prsc:2,ntxv:0,isnm:False|TEX-965-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:965,x:32971,y:32211,ptovrint:False,ptlb:ColorGradientMask,ptin:_ColorGradientMask,varname:node_965,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Lerp,id:6641,x:33607,y:32052,varname:node_6641,prsc:2|A-6774-RGB,B-875-OUT,T-2385-OUT;n:type:ShaderForge.SFN_Vector1,id:875,x:33412,y:31943,varname:node_875,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:2385,x:33412,y:32243,varname:node_2385,prsc:2|A-2563-RGB,B-6673-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6673,x:33182,y:32376,ptovrint:False,ptlb:ColorMaskIntensity,ptin:_ColorMaskIntensity,varname:node_6673,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_TexCoord,id:5302,x:31329,y:32610,varname:node_5302,prsc:2,uv:0;n:type:ShaderForge.SFN_SwitchProperty,id:6475,x:31663,y:32415,ptovrint:False,ptlb:IsFirstPanUV1,ptin:_IsFirstPanUV1,varname:node_6475,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5302-UVOUT,B-1980-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:1980,x:31313,y:31988,varname:node_1980,prsc:2,uv:1;n:type:ShaderForge.SFN_ValueProperty,id:3750,x:32043,y:31716,ptovrint:False,ptlb:U_Speed_2,ptin:_U_Speed_2,varname:_U_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;n:type:ShaderForge.SFN_ValueProperty,id:4798,x:32043,y:31811,ptovrint:False,ptlb:V_Speed_2,ptin:_V_Speed_2,varname:_V_Speed_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.15;n:type:ShaderForge.SFN_Append,id:3209,x:32253,y:31752,varname:node_3209,prsc:2|A-3750-OUT,B-4798-OUT;n:type:ShaderForge.SFN_Multiply,id:6047,x:32424,y:31752,varname:node_6047,prsc:2|A-3209-OUT,B-8329-T;n:type:ShaderForge.SFN_Time,id:8329,x:32253,y:31906,varname:node_8329,prsc:2;n:type:ShaderForge.SFN_Add,id:6639,x:32424,y:31906,varname:node_6639,prsc:2|A-6047-OUT,B-5302-UVOUT;n:type:ShaderForge.SFN_Multiply,id:9581,x:32759,y:32178,varname:node_9581,prsc:2|A-6452-OUT,B-2541-OUT;n:type:ShaderForge.SFN_Tex2d,id:2432,x:32759,y:31947,varname:node_2432,prsc:2,ntxv:0,isnm:False|UVIN-6639-OUT,TEX-4181-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:4181,x:32590,y:31947,ptovrint:False,ptlb:SecondaryNoise,ptin:_SecondaryNoise,varname:node_4181,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:5593,x:32803,y:32330,ptovrint:False,ptlb:HasSecondaryNoise,ptin:_HasSecondaryNoise,varname:node_5593,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-2541-OUT,B-9581-OUT;n:type:ShaderForge.SFN_Multiply,id:6452,x:32907,y:31804,varname:node_6452,prsc:2|A-3769-OUT,B-2432-RGB;n:type:ShaderForge.SFN_ValueProperty,id:3769,x:32696,y:31678,ptovrint:False,ptlb:SecondaryNoisePower,ptin:_SecondaryNoisePower,varname:node_3769,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:8467,x:35477,y:32194,varname:node_8467,prsc:2|A-3766-OUT,B-5584-OUT;n:type:ShaderForge.SFN_DepthBlend,id:3814,x:34532,y:32093,varname:node_3814,prsc:2|DIST-9887-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9887,x:34336,y:32013,ptovrint:False,ptlb:IntersectionDepth,ptin:_IntersectionDepth,varname:node_9887,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.25;n:type:ShaderForge.SFN_SwitchProperty,id:8155,x:35764,y:32341,ptovrint:False,ptlb:HasIntersectionDepth,ptin:_HasIntersectionDepth,varname:node_8155,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-5584-OUT,B-8467-OUT;n:type:ShaderForge.SFN_OneMinus,id:7125,x:34702,y:32093,varname:node_7125,prsc:2|IN-3814-OUT;n:type:ShaderForge.SFN_Multiply,id:6264,x:34884,y:32093,varname:node_6264,prsc:2|A-7125-OUT,B-8922-RGB;n:type:ShaderForge.SFN_Color,id:8922,x:34655,y:32282,ptovrint:False,ptlb:IntersectionColor,ptin:_IntersectionColor,varname:node_8922,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:613,x:35101,y:32044,varname:node_613,prsc:2|A-5462-OUT,B-6264-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5462,x:34896,y:31970,ptovrint:False,ptlb:IntersectionStrength,ptin:_IntersectionStrength,varname:node_5462,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:3766,x:35337,y:31974,varname:node_3766,prsc:2|A-613-OUT,B-9192-RGB;n:type:ShaderForge.SFN_Tex2d,id:9192,x:35063,y:32290,varname:node_9192,prsc:2,tex:23180e75018665c48957d0377e0abffb,ntxv:0,isnm:False|TEX-5348-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5348,x:34884,y:32290,ptovrint:False,ptlb:IntersectionTexture,ptin:_IntersectionTexture,varname:node_5348,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:23180e75018665c48957d0377e0abffb,ntxv:0,isnm:False;n:type:ShaderForge.SFN_SwitchProperty,id:5529,x:36033,y:32678,ptovrint:False,ptlb:HasDistortion?,ptin:_HasDistortion,varname:node_5529,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-127-OUT,B-6834-OUT;n:type:ShaderForge.SFN_Vector2,id:127,x:35806,y:32571,varname:node_127,prsc:2,v1:0,v2:0;n:type:ShaderForge.SFN_SwitchProperty,id:6834,x:35757,y:32767,ptovrint:False,ptlb:SameDistortionAsOpacity?,ptin:_SameDistortionAsOpacity,varname:node_6834,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False|A-322-OUT,B-309-OUT;n:type:ShaderForge.SFN_ComponentMask,id:309,x:35531,y:32538,varname:node_309,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-8155-OUT;n:type:ShaderForge.SFN_Tex2d,id:7638,x:35311,y:32777,varname:node_7638,prsc:2,ntxv:0,isnm:False|TEX-5437-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5437,x:35136,y:32777,ptovrint:False,ptlb:DistortionTexture,ptin:_DistortionTexture,varname:node_5437,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ComponentMask,id:322,x:35482,y:32777,varname:node_322,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-7638-RGB;n:type:ShaderForge.SFN_Multiply,id:6848,x:36241,y:32680,varname:node_6848,prsc:2|A-5529-OUT,B-2764-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2764,x:35980,y:32893,ptovrint:False,ptlb:DistortionStrength,ptin:_DistortionStrength,varname:node_2764,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.1;proporder:6733-4974-1637-1906-251-5958-7197-7662-2891-6806-8558-5291-1427-7935-4760-2215-1027-2788-6388-1249-5322-6228-965-6673-6475-3750-4798-4181-5593-3769-9887-8155-8922-5462-5348-5529-6834-5437-2764;pass:END;sub:END;*/

Shader "Custom/AddMul_SS" {
    Properties {
        _MainTexture ("MainTexture", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _EmissiveIntensity ("EmissiveIntensity", Float ) = 4
        _OpacityIntensity ("OpacityIntensity", Float ) = 4
        [MaterialToggle] _HasNoiseEmissive ("HasNoiseEmissive?", Float ) = 0
        _Noise ("Noise", 2D) = "white" {}
        _U_Speed ("U_Speed", Float ) = 0.1
        _V_Speed ("V_Speed", Float ) = 0.15
        _NoisePower ("NoisePower", Float ) = 3
        [MaterialToggle] _HasNoiseOpacity ("HasNoiseOpacity?", Float ) = 0
        _Depth ("Depth", Float ) = 0.2
        [MaterialToggle] _HasOpacityDepth ("HasOpacityDepth?", Float ) = 0
        [MaterialToggle] _HasEmissiveDepth ("HasEmissiveDepth?", Float ) = 0
        _FresnelExp ("FresnelExp", Float ) = 0.5
        [MaterialToggle] _HasOpacityFresnel ("HasOpacityFresnel?", Float ) = 0
        [MaterialToggle] _HasEmissiveFresnel ("HasEmissiveFresnel?", Float ) = 0
        _FresnelPower ("FresnelPower", Float ) = 1.2
        _FinalOpacity ("FinalOpacity", Float ) = 1
        [MaterialToggle] _Is_ScreenSpace ("Is_ScreenSpace?", Float ) = 0
        [MaterialToggle] _HasColorGradient ("HasColorGradient?", Float ) = 0
        _ColorGradient ("ColorGradient", 2D) = "white" {}
        [MaterialToggle] _HasColorGradientMask ("HasColorGradientMask?", Float ) = 0
        _ColorGradientMask ("ColorGradientMask", 2D) = "white" {}
        _ColorMaskIntensity ("ColorMaskIntensity", Float ) = 1
        [MaterialToggle] _IsFirstPanUV1 ("IsFirstPanUV1", Float ) = 0
        _U_Speed_2 ("U_Speed_2", Float ) = 0.1
        _V_Speed_2 ("V_Speed_2", Float ) = 0.15
        _SecondaryNoise ("SecondaryNoise", 2D) = "white" {}
        [MaterialToggle] _HasSecondaryNoise ("HasSecondaryNoise", Float ) = 0
        _SecondaryNoisePower ("SecondaryNoisePower", Float ) = 1
        _IntersectionDepth ("IntersectionDepth", Float ) = 0.25
        [MaterialToggle] _HasIntersectionDepth ("HasIntersectionDepth", Float ) = 0
        _IntersectionColor ("IntersectionColor", Color) = (1,1,1,1)
        _IntersectionStrength ("IntersectionStrength", Float ) = 1
        _IntersectionTexture ("IntersectionTexture", 2D) = "white" {}
        [MaterialToggle] _HasDistortion ("HasDistortion?", Float ) = 0
        [MaterialToggle] _SameDistortionAsOpacity ("SameDistortionAsOpacity?", Float ) = 0
        _DistortionTexture ("DistortionTexture", 2D) = "white" {}
        _DistortionStrength ("DistortionStrength", Float ) = 0.1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One OneMinusSrcAlpha
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
            uniform sampler2D _GrabTexture;
            uniform sampler2D _CameraDepthTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTexture; uniform float4 _MainTexture_ST;
            uniform float4 _Color;
            uniform float _EmissiveIntensity;
            uniform float _OpacityIntensity;
            uniform fixed _HasNoiseEmissive;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _U_Speed;
            uniform float _V_Speed;
            uniform float _NoisePower;
            uniform fixed _HasNoiseOpacity;
            uniform float _Depth;
            uniform fixed _HasOpacityDepth;
            uniform fixed _HasEmissiveDepth;
            uniform float _FresnelExp;
            uniform fixed _HasOpacityFresnel;
            uniform fixed _HasEmissiveFresnel;
            uniform float _FresnelPower;
            uniform float _FinalOpacity;
            uniform fixed _Is_ScreenSpace;
            uniform fixed _HasColorGradient;
            uniform sampler2D _ColorGradient; uniform float4 _ColorGradient_ST;
            uniform fixed _HasColorGradientMask;
            uniform sampler2D _ColorGradientMask; uniform float4 _ColorGradientMask_ST;
            uniform float _ColorMaskIntensity;
            uniform fixed _IsFirstPanUV1;
            uniform float _U_Speed_2;
            uniform float _V_Speed_2;
            uniform sampler2D _SecondaryNoise; uniform float4 _SecondaryNoise_ST;
            uniform fixed _HasSecondaryNoise;
            uniform float _SecondaryNoisePower;
            uniform float _IntersectionDepth;
            uniform fixed _HasIntersectionDepth;
            uniform float4 _IntersectionColor;
            uniform float _IntersectionStrength;
            uniform sampler2D _IntersectionTexture; uniform float4 _IntersectionTexture_ST;
            uniform fixed _HasDistortion;
            uniform fixed _SameDistortionAsOpacity;
            uniform sampler2D _DistortionTexture; uniform float4 _DistortionTexture_ST;
            uniform float _DistortionStrength;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 screenPos : TEXCOORD4;
                float4 vertexColor : COLOR;
                float4 projPos : TEXCOORD5;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                o.projPos = ComputeScreenPos (o.pos);
                COMPUTE_EYEDEPTH(o.projPos.z);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float sceneZ = max(0,LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)))) - _ProjectionParams.g);
                float partZ = max(0,i.projPos.z - _ProjectionParams.g);
                float4 node_7638 = tex2D(_DistortionTexture,TRANSFORM_TEX(i.uv0, _DistortionTexture));
                float4 node_6236 = tex2D(_MainTexture,TRANSFORM_TEX(i.uv0, _MainTexture));
                float3 node_7726 = (_EmissiveIntensity*(node_6236.rgb*_Color.rgb*i.vertexColor.rgb*i.vertexColor.a));
                float4 node_3262 = _Time + _TimeEditor;
                float2 node_1800 = ((float2(_U_Speed,_V_Speed)*node_3262.g)+lerp( lerp( i.uv0, i.uv1, _IsFirstPanUV1 ), i.screenPos.rg, _Is_ScreenSpace ));
                float4 node_5766 = tex2D(_Noise,TRANSFORM_TEX(node_1800, _Noise));
                float3 node_2541 = pow(node_5766.rgb,_NoisePower);
                float3 _HasNoiseEmissive_var = lerp( node_7726, (node_2541*node_7726), _HasNoiseEmissive );
                float node_7176 = saturate((sceneZ-partZ)/_Depth);
                float3 _HasEmissiveDepth_var = lerp( _HasNoiseEmissive_var, (_HasNoiseEmissive_var*node_7176), _HasEmissiveDepth );
                float node_8690 = pow((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_FresnelExp)),_FresnelPower);
                float3 _HasEmissiveFresnel_var = lerp( _HasEmissiveDepth_var, (_HasEmissiveDepth_var*node_8690), _HasEmissiveFresnel );
                float4 node_6774 = tex2D(_ColorGradient,TRANSFORM_TEX(i.uv0, _ColorGradient));
                float node_875 = 1.0;
                float4 node_2563 = tex2D(_ColorGradientMask,TRANSFORM_TEX(i.uv0, _ColorGradientMask));
                float3 node_5584 = (lerp( _HasEmissiveFresnel_var, (lerp( node_6774.rgb, lerp(node_6774.rgb,float3(node_875,node_875,node_875),(node_2563.rgb*_ColorMaskIntensity)), _HasColorGradientMask )*_HasEmissiveFresnel_var), _HasColorGradient )*_FinalOpacity);
                float4 node_9192 = tex2D(_IntersectionTexture,TRANSFORM_TEX(i.uv0, _IntersectionTexture));
                float3 _HasIntersectionDepth_var = lerp( node_5584, (((_IntersectionStrength*((1.0 - saturate((sceneZ-partZ)/_IntersectionDepth))*_IntersectionColor.rgb))*node_9192.rgb)+node_5584), _HasIntersectionDepth );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + (lerp( float2(0,0), lerp( node_7638.rgb.rg, _HasIntersectionDepth_var.rg, _SameDistortionAsOpacity ), _HasDistortion )*_DistortionStrength);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float3 emissive = _HasIntersectionDepth_var;
                float3 finalColor = emissive;
                float node_5136 = ((node_6236.r*_Color.a*i.vertexColor.a)*_OpacityIntensity);
                float4 node_8329 = _Time + _TimeEditor;
                float2 node_6639 = ((float2(_U_Speed_2,_V_Speed_2)*node_8329.g)+i.uv0);
                float4 node_2432 = tex2D(_SecondaryNoise,TRANSFORM_TEX(node_6639, _SecondaryNoise));
                float _HasNoiseOpacity_var = lerp( node_5136, (lerp( node_2541, ((_SecondaryNoisePower*node_2432.rgb)*node_2541), _HasSecondaryNoise ).r*node_5136), _HasNoiseOpacity );
                float _HasOpacityDepth_var = lerp( _HasNoiseOpacity_var, (_HasNoiseOpacity_var*node_7176), _HasOpacityDepth );
                return fixed4(lerp(sceneColor.rgb, finalColor,(_FinalOpacity*lerp( _HasOpacityDepth_var, (_HasOpacityDepth_var*node_8690), _HasOpacityFresnel ))),1);
            }
            ENDCG
        }
    }
    FallBack "Particles/Additive"
    CustomEditor "ShaderForgeMaterialInspector"
}
