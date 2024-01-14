// Upgrade NOTE: replaced 'PositionFog()' with multiply of UNITY_MATRIX_MVP by position
// Upgrade NOTE: replaced 'V2F_POS_FOG' with 'float4 pos : SV_POSITION'

Shader "LaikaBoss/Glass_add" {
	Properties 
	{
		_Color ("Main Color (.a Opacity)", Color) = (1,1,1,0.5)
		_RefColor ("REflection Color", Color) = (1,1,1,0.5)
		
		_CubeReflection  ("Reflections", Cube) = ""
		_Reflection_pow ("Shellac power", float) = 0.5
		_Fresnel_scale ("Fresnel scale", float) = 1
		_fresnel_pow ("Fresnel power", float) = 1
	}

	SubShader 
	{
		Tags { "RenderType"="Transparent" }
	 
	 	Blend One One
	 	
		CGPROGRAM
		#pragma surface surf Lambert alpha
		
		struct Input  
      	{
			float3 viewDir;
			float3 worldRefl;
			INTERNAL_DATA

		};

		float4 _Color;
		float4 _RefColor;
		
		samplerCUBE _CubeReflection;
		float _Reflection_pow;
		float _fresnel_pow;
		float _Fresnel_scale;
				
		void surf (Input IN, inout SurfaceOutput o) 
      	{
      		fixed4 clear_fresnel = (1.0 - dot( normalize(IN.viewDir), o.Normal ));
			fixed4 fresnel = pow(clear_fresnel,_fresnel_pow); 
			
      		float3 cube = texCUBE (_CubeReflection, WorldReflectionVector (IN, o.Normal)).rgb; 
       		float ref = cube.r *  _Reflection_pow;
			float3 shellac = float3(ref,ref,ref);
			
			o.Alpha = shellac.r + _Color.a ;
			o.Emission = shellac * _RefColor;
			o.Albedo = _Color;
      	}
		ENDCG  
	}
	
	// Fallback for cards that don't do cubemapping
	FallBack "VertexLit", 1
}