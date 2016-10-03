Shader "Custom/CubeAlpha"
{
Properties
{
  _MainTex ("Textures", 2D) = "white"{} 
  _BumpMap ("Normal Map", 2D) = "bump" {}
  
  _SpecMap ("Specularmap", 2D) = "black" {}
  _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5,1)
  _SpecPower ("Specular Power", Range(0,1)) = 0.5  
  
  _Cutoff ("Alpha Cutoff", Range(0,1)) = 0.0
  
}
  SubShader
  { 
    Tags{"RenderType" = "Opaque" "Opaque" = "AlphaTest" }
    LOD 200    
    CGPROGRAM 
    #pragma surface surf BlinnPhong alphatest:_Cutoff 
    sampler2D _MainTex;
    sampler2D _BumpMap;
    sampler2D _SpecMap;
    float _SpecPower;
    
    struct Input 
    {
      float2 uv_BumpMap;
      float2 uv_MainTex; //(1.0, 1.0) U, V
      float2 uv_SpecMap;
    };
    
    void surf(Input IN, inout SurfaceOutput o)
    {
      //o.Albedo = 1;
      fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
      fixed4 specTex = tex2D(_SpecMap, IN.uv_SpecMap);
      
      o.Albedo = tex.rgb;
      o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));
      o.Specular = _SpecPower;
      o.Gloss = specTex.rgb;
      
      o.Alpha = tex.a;
      
    }
    ENDCG
  }
  Fallback "Diffuse"
}
