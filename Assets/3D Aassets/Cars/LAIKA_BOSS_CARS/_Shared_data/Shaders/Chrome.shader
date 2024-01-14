Shader "LaikaBoss/Chrome" {
	Properties {

		_Color ("Main Color", Color) = (1,1,1,0.5)
		_CubeReflection  ("Reflections", Cube) = "black" {}
		_Reflection_pow ("Shellac power", float) = 0.3
	}

	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Simple

		struct Input  
      	{
			fixed3 viewDir;
			fixed3 worldRefl;
			fixed2 uv_MainTex;
			float4 color: Color; // Vertex color
			INTERNAL_DATA
		};

		fixed4 _Color;
		fixed4 _OcclusionColor;

		fixed _Reflection_pow;
		samplerCUBE _CubeReflection;

		half4 LightingSimple(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
        	half diff = max (0, dot (s.Normal, half4(0,1,0,0)))/1.8 + 0.2;
			
			half4 c;
          	c.rgb = s.Albedo.rgb * _LightColor0.rgb * diff;
          	c.a = s.Alpha;
          	
          	return c;
      	}

		void surf (Input IN, inout SurfaceOutput o) 
      	{
      		fixed4 m_ao = IN.color;
      		
      		fixed4 _cubeReflection = texCUBE (_CubeReflection, WorldReflectionVector (IN, o.Normal)); 
       		fixed3 reflection = clamp(_cubeReflection * _Reflection_pow, 0, 3);

 			float3 _color = _Color;
 			o.Emission = reflection * m_ao;
 			o.Albedo = _color;
      	}
		ENDCG
	}
	FallBack "Diffuse"
}
