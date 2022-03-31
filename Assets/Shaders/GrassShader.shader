
Shader "Custom/GrassShader"
{
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color ("Color", Color) = (1,1,1,1) 
		_WindTexture("Wind texture", 2D) = "white" {}
        _WindIntensity ("Wind Intensity", Float) = 1.0
        _WindStrength ("Wind Strength", Float) = 1.0
        _WindDensity ("Wind Density", Float) = 1.0
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
            #include "noiseSimplex.cginc"
			#include "remap.cginc"
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
            #pragma target 3.0

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 pos : SV_POSITION;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Color;
			sampler2D _WindTexture;
            float4 _WindTexture_ST;
            uniform float _WindIntensity;
            uniform float _WindStrength;
            uniform float _WindDensity;
			
			v2f vert (appdata v)
			{
				v2f o;
				_WindTexture_ST.xy += _Time.y * _WindDensity;
				float4 worldCoord = mul(unity_ObjectToWorld, v.vertex);
				fixed4 noise = tex2Dlod(_WindTexture, float4(worldCoord.xz * _WindTexture_ST, 0.0, 0.0)) * _WindIntensity;
				float xNoise = map(snoise(float2(worldCoord.x, _Time.y* _WindDensity)), -1, 1, -_WindStrength, _WindStrength);
				float zNoise = map(snoise(float2(worldCoord.z, _Time.y* _WindDensity)), -1, 1, -_WindStrength, _WindStrength);
				//float xNoise = map(noise.x, -1, 1, -_WindStrength, _WindStrength);
				//float zNoise = map(noise.y, -1, 1, -_WindStrength, _WindStrength);
				v.vertex.xyz += float3(xNoise, 0.0, zNoise) * 0.01 * v.uv.y;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv.xy;

				// billboard mesh towards camera
				/*float3 vpos = mul((float3x3)unity_ObjectToWorld, v.vertex.xyz);
				
                float4 worldCoord = float4(unity_ObjectToWorld._m03, unity_ObjectToWorld._m13, unity_ObjectToWorld._m23, 1);
				float4 viewPos = mul(UNITY_MATRIX_V, worldCoord) + float4(vpos, 0);
				float4 outPos = mul(UNITY_MATRIX_P, viewPos);

				o.pos = outPos;*/

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float4 color = _Color * col.w;
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, color);
				return color;
			}
			ENDCG
		}
	}
}
