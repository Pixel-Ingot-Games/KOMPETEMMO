Shader "LaikaBoss/Plastic" {
	Properties {

		_Color ("Main Color", Color) = (1,1,1,0.5)

		_Cube  ("Reflection", Cube) = "black" {}
		_OcclusionColor ("Occlusion shadow", Color) = (0,0,0,0.5)
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
		samplerCUBE _Cube;

		half4 LightingSimple(SurfaceOutput s, half3 lightDir, half3 viewDir, half atten) 
		{
        	half diff = max (0, dot (s.Normal,lightDir));
			
			half4 c;
          	c.rgb = s.Albedo.rgb * _LightColor0.rgb * diff;
          	c.a = s.Alpha;
          	
          	return c;
      	}

		void surf (Input IN, inout SurfaceOutput o) 
      	{
      		fixed4 m_ao = IN.color;
      		
      		fixed4 _cubeReflection = texCUBE (_Cube, WorldReflectionVector (IN, o.Normal)); 
       		fixed3 reflection = clamp(_cubeReflection * _Reflection_pow, 0, 3);

 			float3 _color = lerp(_Color, _OcclusionColor.rgb, 1-(m_ao * m_ao).r);

 			o.Emission = reflection * m_ao;
 			o.Albedo = _color;
      	}
		ENDCG
	}
	FallBack "Diffuse"
}
