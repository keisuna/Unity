// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/two"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Color", Color) = (1.0,1.0,1.0)
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		LOD 100
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
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				return o;
			}
			
			
			float noise(float3 p,float s) {
	   
				float strength = 10.0 + sin(_Time.x);
				float accum = s/4.0;
				float prev = 0.0;
				float tw = 0.0;
				for (int i = 0; i < 32; ++i) {
					float mag = dot(p, p);
					p = abs(p) / mag + float3(-0.25, -0.3, -1.);
					float w = exp(-i / 8.0);
					accum += w * exp(-strength * pow(abs(mag - prev), 2.0));
					tw += w;
					prev = mag;
				}
				return max(0.0, 5.0 * accum / tw - 0.4);
			}

			uniform float4 _Color;

			fixed4 frag (v2f i) : SV_Target
			{
				
				float2 tex = i.uv + 0.5;
				float3 p = float3(tex, 0.0)+ float3(-1.0, -1.0, 1.0);
				p += 0.5 * float3( sin(_Time.y / 1000.0), cos(_Time.y / 1000.0), cos(_Time.x/2));
				float t = noise(p,5.0);
   
				fixed4 col = float4(_Color.r *t*t* t , _Color.g *t* t , _Color.b * t, _Color.a * t * t);
				return col;
			}

			ENDCG
		}
	}
}
