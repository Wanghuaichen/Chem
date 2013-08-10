// Upgrade NOTE: replaced 'PositionFog()' with multiply of UNITY_MATRIX_MVP by position
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'

Shader "Car/Glass" {

Properties {

    _Color ("Main Color", Color) = (1,1,1,1)

    _SpecularColor ("Specular Color", Color) = (0.5,0.5,0.5,1)

    _Cube ("Reflection Cubemap", Cube) = "_Skybox" {TexGen CubeNormal}

}

 

Category {

    Tags {Queue=Transparent}

    ZWrite Off

    Blend SrcAlpha OneMinusSrcAlpha

    Lighting On

    Colormask RGB

 

    SubShader {

        Pass {

            

CGPROGRAM

#pragma profiles arbfp1

#pragma fragment frag

#pragma fragmentoption ARB_fog_exp2

#pragma fragmentoption ARB_precision_hint_fastest 

#pragma vertex vert

#include "UnityCG.cginc"

 

struct v2f {

    float4 pos : SV_POSITION;

    float3  normal  : TEXCOORD0;

    float3  viewDir : TEXCOORD1;

};

 

v2f vert (appdata_tan v)

{   

    v2f o;

    o.pos = mul (UNITY_MATRIX_MVP, v.vertex);

    o.normal = mul( (float3x3)_Object2World, v.normal );

    o.viewDir = mul( (float3x3)_Object2World, ObjSpaceViewDir(v.vertex) );

    

    return o;

}

 

uniform samplerCUBE _Cube : register(s0);

uniform float4 _Color;

uniform float4 _SpecularColor;

 

float4 frag (v2f i)  : COLOR

{

    float3 normal = i.normal;

    i.viewDir = normalize(i.viewDir);

    half nsv = saturate(dot( normal, i.viewDir ));

    

    // calculate reflection vector in world space

    half3 r = reflect(-i.viewDir, normal);

    

    half4 reflcolor = texCUBE(_Cube, r);

    

    half fresnel = 1 - nsv*0.5;

    half3 c = lerp( _Color.rgb, reflcolor.rgb*_SpecularColor.rgb, fresnel );

    

    return half4( c, fresnel );

}

ENDCG  

            SetTexture [_Cube] {combine texture}

        }

    }

    

    SubShader {

        Pass {

            Color [_Color]

        }

    }

}

 

}