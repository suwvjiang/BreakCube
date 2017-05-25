Shader "Custom/SimpleDiffuseAddColor" 
{
	Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _MainTex ("Controll(Alpha)", 2D) = "white" { }
        _NumTex ("Number. (Alpha)", 2D) = "white" { }
    }

    Category
    {
        SubShader
        {
            Tags { "RenderType"="Opaque" "Queue"="Geometry+51"}
		    Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma surface surf Lambert noforwardadd finalcolor:addColor

			sampler2D _MainTex;
            sampler2D _NumTex;
			float4 _Color;
			struct Input {
				float2 uv_MainTex;
                float2 uv_NumTex;
			};

			void addColor (Input IN, SurfaceOutput o, inout fixed4 color){
				color *= _Color;
			}

			void surf (Input IN, inout SurfaceOutput o) {
				fixed4 c = tex2D (_MainTex, IN.uv_MainTex);
                fixed4 d = tex2D (_NumTex, IN.uv_NumTex);
				o.Albedo = c.rgb*(1-d.a);
				o.Alpha = c.a * d.a;
			}
			ENDCG
        } 
		FallBack "Mobile/Diffuse"
    }
}
