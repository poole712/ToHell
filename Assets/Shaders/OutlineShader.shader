Shader"Custom/SpriteOutline" {
    Properties {
        _MainTex("Texture", 2D) = "white" {}
        _Color("Colour", Color) = (1, 1, 1, 1)
    }

    SubShader {
Cull Off

Blend One OneMinusSrcAlpha
        
        Pass {
            CGPROGRAM
            #pragma vertex vertexFunc
            #pragma fragment fragmentFunc
#include "UnityCG.cginc"
        
sampler2D _MainTex;
float4 _MainTex_TexelSize;
float4 _Color;

struct appdata
{
    float4 vertex : POSITION;
    float2 texcoord : TEXCOORD0;
};
            
struct v2f
{
    float4 pos : SV_POSITION;
    float2 uv : TEXCOORD0;
};
            
v2f vertexFunc(appdata v)
{
    v2f o;
    o.pos = UnityObjectToClipPos(v.vertex);
    o.uv = v.texcoord;
    return o;
}
            
fixed4 fragmentFunc(v2f i) : SV_Target
{
    half4 c = tex2D(_MainTex, i.uv);
    c.rgb *= c.a;
    half4 outlineC = _Color;
    outlineC.a *= ceil(c.a);
    outlineC.rgb *= outlineC.a;
                
    half upAlpha = tex2D(_MainTex, i.uv + fixed2(0, _MainTex_TexelSize.y)).a;
    half downAlpha = tex2D(_MainTex, i.uv - fixed2(0, _MainTex_TexelSize.y)).a;
    half rightAlpha = tex2D(_MainTex, i.uv + fixed2(_MainTex_TexelSize.x, 0)).a;
    half leftAlpha = tex2D(_MainTex, i.uv - fixed2(_MainTex_TexelSize.x, 0)).a;
                
    return lerp(outlineC, c, ceil(upAlpha * downAlpha * rightAlpha * leftAlpha));
}
            ENDCG
        }
    }
}
