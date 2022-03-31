Shader "Custom/Leaves" {
    Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
        _WindIntensity ("Wind Intensity", Float) = 1.0
        _Factor ("Leaves Size", Range(0.001, 0.01)) = 1.0
	}
	SubShader
	{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "DisableBatching" = "True" }

		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog

			#include "UnityCG.cginc"
			#include "remap.cginc"
			#include "noiseSimplex.cginc"

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
            uniform float _WindIntensity;
            uniform float _Factor;
			
			v2f vert (appdata v)
			{
				v2f o;
				float2 mappedUV = map(v.uv.xy, float2(0,0), float2(1,1), float2(-1,-1), float2(1,1));
				float4 worldPos = mul(unity_ObjectToWorld, v.vertex);
				float noise = snoise(float3(worldPos.x, _Time.y, worldPos.z));
				float3 displacement = float3(mappedUV.x + noise, mappedUV.y, 0.0);
				displacement = mul(displacement, UNITY_MATRIX_V);
				displacement = mul(unity_WorldToObject, displacement);
				displacement = normalize(displacement) * _Factor;

                v.vertex.xyz += displacement;
				
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);

				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}