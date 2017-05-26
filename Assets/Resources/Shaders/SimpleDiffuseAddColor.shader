Shader "Custom/SimpleDiffuseAddColor" 
{
	Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
        _BgTex("Backgroud", 2D) = "white" {}
        _Controll ("Controll(Alpha)", 2D) = "white" { }
        _Controll0 ("Controll(Alpha)", 2D) = "white" { }
        _NumTex ("Number. (Alpha)", 2D) = "white" { }
        _Front("Front", Vector) = (1,1,0,0)
        _Back("Back", Vector) = (1,1,0,0)
        _Left("Left", Vector) = (1,1,0,0)
        _Right("Right", Vector) = (1,1,0,0)
        _Top("Top", Vector) = (1,1,0,0)
        _Down("Down", Vector) = (1,1,0,0)
    }

    Category
    {
        SubShader
        {
            Tags { "RenderType"="Opaque" "Queue"="Geometry+51"}
		    Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma surface surf Lambert noforwardadd finalcolor:addColor

            sampler2D _BgTex;
			sampler2D _Controll;
            sampler2D _Controll0;
            sampler2D _NumTex;
			float4 _Color;
            float4 _Front;
            float4 _Left;
            float4 _Top;
            float4 _Back;
            float4 _Right;
            float4 _Down;
			struct Input {
                float2 uv_BgTex;
				float2 uv_Controll;
                float2 uv_NumTex;
			};

			void addColor (Input IN, SurfaceOutput o, inout fixed4 color){
				color *= _Color;
			}

			void surf (Input IN, inout SurfaceOutput o) {
                fixed4 bg = tex2D (_BgTex, IN.uv_BgTex);
				fixed4 c = tex2D (_Controll, IN.uv_Controll);
                fixed4 c0 = tex2D (_Controll0, IN.uv_Controll);

                float4 xuv;
                float4 yuv;
                float4 zuv;

                zuv.xy = IN.uv_NumTex * _Front.xy + _Front.zw;
                fixed4 frontC = tex2D (_NumTex, zuv.xy);
                yuv.xy = IN.uv_NumTex * _Left.xy + _Left.zw;
                fixed4 topC = tex2D (_NumTex, yuv.xy);
                xuv.xy = IN.uv_NumTex * _Top.xy + _Top.zw;
                fixed4 leftC = tex2D (_NumTex, xuv.xy);

                zuv.zw = IN.uv_NumTex * _Back.xy + _Back.zw;
                fixed4 backC = tex2D (_NumTex, zuv.zw);
                yuv.zw = IN.uv_NumTex * _Down.xy + _Down.zw;
                fixed4 downC = tex2D (_NumTex, yuv.zw);
                xuv.zw = IN.uv_NumTex * _Right.xy + _Right.zw;
                fixed4 rightC = tex2D (_NumTex, xuv.zw);


                fixed3 fc = frontC.rgb * frontC.a + bg.rgb * (1-frontC.a);
                fixed3 tc = topC.rgb * topC.a + bg.rgb * (1-topC.a);
                fixed3 lc = leftC.rgb * leftC.a + bg.rgb * (1-leftC.a);
                fixed3 bc = backC.rgb * backC.a + bg.rgb * (1-backC.a);
                fixed3 dc = downC.rgb * downC.a + bg.rgb * (1-downC.a);
                fixed3 rc = rightC.rgb * rightC.a + bg.rgb * (1-rightC.a);

				o.Albedo = fc*c.r*c.a + tc*c.g*c.a + lc*c.b*c.a + bc*c0.r*c0.a + dc*c0.g*c0.a + rc*c0.b*c0.a;
				o.Alpha = 1;
			}
			ENDCG
        } 
		FallBack "Mobile/Diffuse"
    }
}
