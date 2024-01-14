Shader "LaikaBoss/CarPaint" {
	Properties {

		_Color ("Main Color", Color) = (1,1,1,0.5)
		_CandyColor ("Candy Color", Color) = (1,1,1,0.5)

		_CubeReflection  ("Reflections", Cube) = "black" {}

		_OcclusionColor ("Occlusion shadow", Color) = (0,0,0,0.5)
		_Fresnel_pow ("Fresnel angle", float) = 2
		_Fresnel_scale ("Fresnel power", float) = 1
		_Reflection_pow ("Shellac power", float) = 0.3
		_Metallic_pow ("Metallic power", float) = 0.3
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
		fixed4 _CandyColor;
		fixed4 _OcclusionColor;

		fixed _Reflection_pow;
		fixed _Metallic_pow;
		fixed _Fresnel_pow;
		fixed _Fresnel_scale;

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
      		fixed4 m_ao = IN.color;
      		
      		fixed clear_fresnel = (1.0 - dot( normalize(IN.viewDir), o.Normal ));
			fixed fresnel = _Fresnel_scale * pow(clear_fresnel, clamp(_Fresnel_pow, 0, 30)); 
			
			fixed3 candyColor = lerp(_Color, _CandyColor, clamp(fresnel, 0, 1));
			
       		fixed4 _cubeReflection = texCUBE (_CubeReflection, WorldReflectionVector (IN, o.Normal)); 
       		fixed4 _cubeMetallic = _cubeReflection.a;
       		fixed3 shellac = clamp(_cubeReflection * _Reflection_pow, 0, 3);

 			float3 carPaint = lerp(candyColor, _OcclusionColor.rgb, 1-(m_ao * m_ao).r);
 			fixed3 metallic = _cubeMetallic * _Metallic_pow * (carPaint/4+0.25);

 			o.Emission = (shellac * fresnel + metallic) * m_ao;
 			o.Albedo = carPaint;
      	}
		ENDCG
	}
	FallBack "Diffuse"
}
