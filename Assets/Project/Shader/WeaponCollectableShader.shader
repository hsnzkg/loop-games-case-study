Shader "Custom/Sprite/WeaponCollectableShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)
        _RotationSpeed ("Rotation Speed (deg/sec)", Float) = 180
        _Pivot ("Pivot Offset (local)", Vector) = (0,0,0,0)
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

        // Premultiplied alpha blend
        Blend One OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_instancing

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _Color;

            float _RotationSpeed;
            float4 _Pivot;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                // Rotation
                float angle = radians(_Time.y * _RotationSpeed);
                float s = sin(angle);
                float c = cos(angle);

                float3 pos = v.vertex.xyz;

                // Pivot offset
                pos.xy -= _Pivot.xy;

                float2 rotated;
                rotated.x = pos.x * c - pos.y * s;
                rotated.y = pos.x * s + pos.y * c;

                pos.xy = rotated;

                pos.xy += _Pivot.xy;

                o.vertex = UnityObjectToClipPos(float4(pos, 1));

                // UV clamp to avoid atlas bleeding
                float2 uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.uv = clamp(uv, float2(0.001, 0.001), float2(0.999, 0.999));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);

                fixed4 col = tex2D(_MainTex, i.uv) * _Color;

                // Premultiply alpha to kill halo artifacts
                col.rgb *= col.a;

                return col;
            }
            ENDCG
        }
    }
}
