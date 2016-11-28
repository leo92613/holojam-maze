Shader "Custom/facingShaderGLSL" {
Properties {
	_Color ("Color", Color) = (1,1,1,1)
	_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
	_PosTex ("position", 2D) = "white" {}
	_SpriteTex("sprite", 2D) = "white" {}
	_ShadowTex("shadow", 2D) = "white" {}
	_Tile ("Tiling", Float) = 12
	_ShadowTile ("Tiling", Float) = 12
	_ShadowSpeed ("Tiling", Float) = 12
	_LineWidth ("lineWidth", Float) = 12
	_Speed ("Speed", Float) = 12
	_Saturation ("saturation", Float) = 1
	_Brightness ("brightness", Float) = 1
	_HueShift ("hue shift", Float) = 0
}
   SubShader {
   		
      Tags { "Queue" = "Transparent" } 
         // draw after all opaque geometry has been drawn
      Pass {
         ZWrite Off // don't write to depth buffer 
            // in order not to occlude other objects
		Cull Off
         Blend One One // use alpha blending

         GLSLPROGRAM
               
         #ifdef VERTEX
         

//		uniform sampler2D _MainTex;
		uniform vec4 _Color;
		uniform vec4 _MainTex_ST;
		uniform sampler2D _SpriteTex;
		uniform sampler2D _PosTex;
		uniform sampler2D _ShadowTex;
		uniform float _Tile;
		uniform float _ShadowTile;
//		uniform float _HueShift;
		uniform float _ShadowSpeed;
		uniform float _Saturation;
		uniform float _Brightness;
		uniform float _Speed;
		uniform float _LineWidth;
		uniform float _UNPnts;
//		uniform float repeat;

         // The following built-in uniforms are also defined in 
         // "UnityCG.glslinc", which could be #included 
         uniform vec3 _WorldSpaceCameraPos; 
            // camera position in world space
         uniform mat4 _Object2World; // model matrix
         uniform mat4 _World2Object; // inverse model matrix 
            // (apart from the factor unity_Scale.w)
                  
         varying vec3 varyingNormalDirection; 
            // normalized surface normal vector
         varying vec3 varyingViewDirection; 
         uniform sampler2D _MainTex;
         varying vec2 uv;
         varying vec2 uv2;
         varying vec3 pos;
            // normalized view direction 
                 
         
         void main()
         {				
            mat4 modelMatrix = _Object2World;
            mat4 modelMatrixInverse = _World2Object; 
            uv = gl_MultiTexCoord0.xy;
            uv2 = gl_MultiTexCoord1.xy;
            pos = (_Object2World*gl_Vertex).xyz;

               // multiplication with unity_Scale.w is unnecessary 
               // because we normalize transformed vectors

            varyingNormalDirection = normalize(
               vec3(vec4(gl_Normal, 0.0) * modelMatrixInverse));
            varyingViewDirection = normalize(_WorldSpaceCameraPos 
               - vec3(modelMatrix * gl_Vertex));

            gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
         }
         
         
         
         #endif

         #ifdef FRAGMENT
         
         uniform sampler2D _MainTex;
		uniform vec4 _Color;
//		uniform vec4 _MainTex_ST;
		uniform sampler2D _SpriteTex;
		uniform sampler2D _PosTex;
		uniform sampler2D _ShadowTex;
		uniform float _Tile;
		uniform float _ShadowTile;
		uniform float _HueShift;
		uniform float _ShadowSpeed;
		uniform float _Saturation;
		uniform float _Brightness;
		uniform float _Speed;
		uniform float _LineWidth;
		uniform float _UNPnts;
		uniform float repeat;
		
		uniform vec4 _Time;
		
		varying vec3 pos;
		uniform vec3 _WorldSpaceCameraPos; 
		varying vec3 varyingNormalDirection; 
		// normalized surface normal vector
		varying vec3 varyingViewDirection;
		varying vec2 uv;
		varying vec2 uv2;
            // normalized view direction
            
             	
				
				vec3 hsv2rgb(vec3 c)
{
    vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
    vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
    return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}
				 
         void main()
         {
             vec3 normalDirection = normalize(varyingNormalDirection);
            vec3 viewDirection = normalize(varyingViewDirection);
            float newOpacity = pow(abs(dot(viewDirection, normalDirection)),1.5)*1.8;
            float dropoff = max(min(1.0,distance(pos.xyz,_WorldSpaceCameraPos)*.001),0.0);
            vec4 tex = texture2D(_MainTex,uv*_Tile);//*newOpacity*_Color;
//           gl_FragColor = tex*newOpacity*_Color;//*mix(vec4(0,.7,1.0,1.0),vec4(0,0,.8,1.0),);//*(vec4(1,.20,1,0)+tex)*vec4(vec3(1.0),newOpacity) ;


//				float3 normalDirection = normalize(i.varyingNormalDirection);
//				float3 viewDirection = normalize(i.varyingViewDirection);
//				float newOpacity =pow(abs(i.varyingViewDirection),2.)*1.;
//				fixed4 tex = tex2D(_MainTex,i.uv*_Tile);//*newOpacity*_Color;
//				
				
				vec4 col = texture2D(_MainTex, uv);
				vec4 sprite =  texture2D(_SpriteTex, vec2(((uv2.x)*_Tile*col.r)+_Time.z*_Speed,uv2.y));
				vec4 sprite2 = texture2D(_ShadowTex, vec2((uv2.x*_ShadowTile*col.r)+col.g*_Time.z*_ShadowSpeed,uv2.y));
				float wave = min(1.0,sin(uv2.x*3.1415)*2.);
				sprite*=sprite2;
				vec4 hue = vec4(hsv2rgb(vec3(col.b+(_HueShift*_Time.x),_Saturation,_Brightness)),1.0);
				sprite.a *= col.a;
				 gl_FragColor= wave*sprite*hue*sprite.a*newOpacity;
				
         }
         
         #endif

         ENDGLSL
      }
   }
}