Shader "Unlit/SpriteFishSwim"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_NoiseTex ("Texture", 2D) = "white" {}
		_NoiseDetail ("NoiseDetail",Float) = 1
		_Settings("X:Freq Y:Mult Z:Speed W:Center",Vector) = (1,1,1,1)
		_FogColor("Fog",Color) = (1,1,1,1)
		_FogDist("X:Near,Y:Mult,Z:TexMix,W:Pow", Vector) = (0,0,.5,3)

//		_CamAngle("_CamAngle", Float) = 0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Off
		ZTest Off
//		ZTest Always
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 pos : COLOR;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float depth : FLOAT;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
				float2 uv3 : TEXCOORD2;
				float3 pos : COLOR;
				float4 vertex : SV_POSITION;
				float depth : FLOAT;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float4 _Settings;
			float4 _FogDist;
			float4 _FogColor;
			sampler2D _NoiseTex;
			float _NoiseDetail;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.pos = mul (unity_ObjectToWorld, v.vertex).xyz;
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv2 = TRANSFORM_TEX(v.uv2, _MainTex);
				o.uv3 = TRANSFORM_TEX(v.uv3, _MainTex);
				float d = length(mul (UNITY_MATRIX_MV,v.vertex));

				o.depth = pow(min(1.0,max(0.0,d-_FogDist.x)*_FogDist.y),_FogDist.w);
				return o;
			}

			float clamper(float In, float Min, float Max){
				return max(Min,min(Max,In));
			}
			fixed4 frag (v2f i) : SV_Target
			{
				float lerpTurn = ((cos(abs(1-i.uv3.x-_Settings.w)*6.28)+1)*.5);
				float mult = .9+cos(i.uv3.x*6.28*2);
				float turn = lerp(i.uv2.x,1-i.uv2.x,max(0.0,min(1.0,((lerpTurn-.5)*10))));
				float forward = sin( _Settings.x*turn-(i.uv3.y*6.28+_Time.z)*-_Settings.z)*_Settings.y*mult;
				fixed2 uvOff = fixed2(0,max(0.0,(1-turn)-.3)*forward)*.002;
				fixed4 col = tex2D(_MainTex, i.uv+uvOff);
				fixed4 tex = tex2D(_NoiseTex,float2(_Time.x,0)+i.pos.xz*_NoiseDetail);
				fixed4 fogMix = lerp(col*(.9*(tex.r+.5)),_FogColor * _FogDist.z,i.depth*col.a);
//				fixed4 finalMix = lerp(col,fogMix,_FogColor.a);
//				finalMix.a = col.a;
				return fixed4(fogMix.rgb,(1-clamper(i.depth,0,1))*col.a);
			}
			ENDCG
		}
	}
}
