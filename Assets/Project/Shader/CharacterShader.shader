Shader "Project/Sprite/PlayerShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _HurtColor ("Hurt Color", Color) = (1,0,0,1)
        _Hurt ("Hurt", Range(0,1)) = 0
        
        _WobbleDir ("Wobble Dir", Vector) = (0,-1,0,0)
        _WobbleAmount ("Wobble Amount", Float) = 0.05
        _WobbleSpeed ("Wobble Speed", Float) = 4
        _SquashAmount ("SquashAmount", Range(0,1)) = 0.25
        _Facing ("Facing (1 = Right, -1 = Left)", Float) = 1
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv     : TEXCOORD0;
                float4 color  : COLOR;
            };

            struct v2f
            {
                float2 uv     : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color  : COLOR;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _Color;
            float4 _HurtColor;
            float  _Hurt;

            float3 _WobbleDir;
            float  _WobbleAmount;
            float  _WobbleSpeed;
            float  _SquashAmount;
            float  _Facing;

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.x *= _Facing;
                float3 dir = normalize(_WobbleDir);
                dir.x *= _Facing;
                float t = abs(frac(_Time.y * _WobbleSpeed) * 2.0 - 1.0);
                v.vertex.xyz += dir * t * _WobbleAmount;
                
                float squash = t * _SquashAmount;

                float2 d = normalize(dir.xy);
                float2 p = float2(-d.y, d.x);

                float2 pos = v.vertex.xy;

                float dirScale  = 1.0 - squash;
                float perpScale = 1.0 + squash * 0.5;

                pos =
                    d * dot(pos, d) * dirScale +
                    p * dot(pos, p) * perpScale;

                v.vertex.xy = pos;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv) * i.color;
                col.rgb = lerp(col.rgb, _HurtColor.rgb, saturate(_Hurt));
                return col;
            }
            ENDCG
        }
    }
}