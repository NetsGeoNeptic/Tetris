Shader "Custom/CubeMaterialShader" {
Properties
{
        _MainTex ("Base (RGB)", 2D) = "white" {}
        _Color ("Color", Color) = (0.5, 0.5, 1, 1)
        _AlphaTex ("AlphaTex (RGB)", 2D) = "white" {}
        _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.0
}
SubShader {
        Tags{"RenderType" = "Opaque" "Opaque" = "AlphaTest" }
        LOD 150

CGPROGRAM
#pragma surface surf BlinnPhong alphatest:_Cutoff

sampler2D _MainTex;
sampler2D _AlphaTex;
fixed4 _Color;
float _Alpha;

struct Input
{
   float2 uv_MainTex;
   float2 uv_AlphaTex;
};

void surf (Input IN, inout SurfaceOutput o)
{
      fixed4 tex = tex2D(_MainTex, IN.uv_MainTex); 
      fixed4 aTex = tex2D(_AlphaTex, IN.uv_AlphaTex); 
      
      o.Albedo = tex.rgb*_Color;
      o.Alpha = aTex.a;
}
ENDCG
}

Fallback "Diffuse"
}

