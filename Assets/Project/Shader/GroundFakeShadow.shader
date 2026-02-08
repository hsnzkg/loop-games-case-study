Shader "ScratchCard/FakeInnerShadow_Foam"
{
    Properties
    {
        _MainTex ("Main", 2D) = "white" {}
        _MaskTex ("Mask", 2D) = "black" {}
        _Offset ("Offset", Vector) = (0,0,1,1)

        // ===== FAKE LAYER SETTINGS =====
        _FoamColor ("Foam Color", Color) = (0,0,0,0.7)
        _FoamFill ("Foam Fill Amount", Range(0,1)) = 0
        _FoamWidth ("Foam Width", Range(0.001,0.2)) = 0.03
        _FoamSoftness ("Foam Softness", Range(0.1,4)) = 1
        _FoamDirection ("Foam Direction XY", Vector) = (1,-1,0,0)
    }

    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            sampler2D _MaskTex;

            float4 _MainTex_ST;
            float4 _Offset;

            float4 _FoamColor;
            float _FoamFill;
            float _FoamWidth;
            float _FoamSoftness;
            float4 _FoamDirection;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                // ===== MAIN SPRITE =====
                float2 mainUV = float2(
                    _Offset.x + i.uv.x * _Offset.z,
                    _Offset.y + i.uv.y * _Offset.w
                );

                float4 mainCol = tex2D(_MainTex, mainUV);
                float mask = tex2D(_MaskTex, i.uv).r;

                float spriteAlpha = mainCol.a * (1 - mask) * i.color.a;

                // ===== FAKE INNER FOAM / SHADOW =====

                float2 dir = normalize(_FoamDirection.xy);
                float2 foamOffset = dir * _FoamWidth * _FoamFill;

                float maskNear = tex2D(_MaskTex, i.uv - foamOffset).r;

                // Edge detection inside hole
                float edge = saturate(mask - maskNear);

                // smoothing
                float width = max(fwidth(mask) * _FoamSoftness, 0.0001);
                float foam = smoothstep(0, width, edge);

                // Only inside hole
                foam *= mask;

                float3 finalRGB =
                    mainCol.rgb * i.color.rgb * spriteAlpha +
                    _FoamColor.rgb * foam * _FoamColor.a;

                float finalA = spriteAlpha + foam * _FoamColor.a;

                return float4(finalRGB, finalA);
            }

            ENDCG
        }
    }

    FallBack Off
}
