Shader "LaikaBoss/Glass" {
	Properties {

		_Color ("Main Color", Color) = (1,1,1,0.5)
		_CubeReflection  ("Reflections", Cube) = "black" {}

		_Reflection_pow ("Shellac power", float) = 0.3
	}

	SubShader {

		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
	 
		Ztest Lequal
		Zwrite On
		Blend Srcalpha OneMinusSrcAlpha

		LOD 200
		
		CGPROGRAM
		#pragma surface surf Simple keepalpha

		struct Input  
      	{
			fixed3 viewDir;
			fixed3 worldRefl;
			fixed2 uv_MainTex;
			float4 color: Color; // Vertex color
			INTERNAL_DATA
		};

		fixed4 _Color;

		fixed _Reflection_pow;
		samplerCUBE _CubeReflection;

		half4 LightingSimple(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
        	half diff = max (0, dot (s.Normal, lightDir));
			
			half4 c;
          	c.rgb = s.Albedo.rgb * _LightColor0.rgb * diff;
          	c.a = s.Alpha;
          	
          	return c;
      	}

		void surf (Input IN, inout SurfaceOutput o) 
      	{
      		fixed clear_fresnel = (1.0 - dot( normalize(IN.viewDir), o.Normal ));
			fixed4 _cube = texCUBE (_CubeReflection, WorldReflectionVector (IN, o.Normal)); 

			float3 glass = _cube * clear_fresnel * 1.5;
 			o.Alpha = _Color.a + lerp (_cube * _Reflection_pow, 1, clear_fresnel/2);
 			o.Emission = _cube * _Reflection_pow;
 			o.Albedo = _Color;
      	}
		ENDCG
	}
	FallBack "Diffuse"
}
