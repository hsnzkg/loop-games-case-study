Shader "Custom/Instanced/RotatingSpriteZ_Pivot"
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
            float4 _Color;

            float _RotationSpeed;
            float4 _Pivot;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);

                float angle = radians(_Time.y * _RotationSpeed);
                float s = sin(angle);
                float c = cos(angle);

                float3 pos = v.vertex.xyz;

                pos.xy -= _Pivot.xy;

                float2 rotated;
                rotated.x = pos.x * c - pos.y * s;
                rotated.y = pos.x * s + pos.y * c;

                pos.x = rotated.x;
                pos.y = rotated.y;

                pos.xy += _Pivot.xy;

                o.vertex = UnityObjectToClipPos(float4(pos, 1));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                UNITY_SETUP_INSTANCE_ID(i);
                return tex2D(_MainTex, i.uv) * _Color;
            }
            ENDCG
        }
    }
}
