// Made with Amplify Shader Editor v1.9.3.3
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "AmplifyShaderPack/Burn Effect"
{
	Properties
	{
		_AlbedoMix("Albedo Mix", Range( 0 , 1)) = 0.5
		_CharcoalMix("Charcoal Mix", Range( 0 , 1)) = 1
		_EmberColorTint("Ember Color Tint", Color) = (0.9926471,0.6777384,0,1)
		_Albedo("Albedo", 2D) = "white" {}
		_Normals("Normals", 2D) = "bump" {}
		_BaseEmber("Base Ember", Range( 0 , 1)) = 0
		_GlowEmissionMultiplier("Glow Emission Multiplier", Range( 0 , 30)) = 1
		_GlowColorIntensity("Glow Color Intensity", Range( 0 , 10)) = 0
		_BurnOffset("Burn Offset", Range( 0 , 5)) = 1
		_CharcoalNormalTile("Charcoal Normal Tile", Range( 2 , 5)) = 5
		_BurnTilling("Burn Tilling", Range( 0.1 , 1)) = 1
		_GlowBaseFrequency("Glow Base Frequency", Range( 0 , 5)) = 1.1
		_GlowOverride("Glow Override", Range( 0 , 10)) = 1
		_Masks("Masks", 2D) = "white" {}
		_BurntTileNormals("Burnt Tile Normals", 2D) = "white" {}
		_Smoothness("Smoothness", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		ZTest LEqual
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#pragma target 3.0
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Normals;
		uniform sampler2D _BurntTileNormals;
		uniform float _CharcoalNormalTile;
		uniform float _CharcoalMix;
		uniform sampler2D _Masks;
		uniform float _BurnOffset;
		uniform float _BurnTilling;
		uniform sampler2D _Albedo;
		uniform float _AlbedoMix;
		uniform float _BaseEmber;
		uniform float4 _EmberColorTint;
		uniform float _GlowColorIntensity;
		uniform float _GlowBaseFrequency;
		uniform float _GlowOverride;
		uniform float _GlowEmissionMultiplier;
		uniform half _Smoothness;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			half4 tex2DNode83 = tex2D( _BurntTileNormals, ( i.uv_texcoord * _CharcoalNormalTile ) );
			half4 appendResult182 = (half4(1.0 , tex2DNode83.g , 0.0 , tex2DNode83.r));
			half2 panner9 = ( _BurnOffset * float2( 1,0.5 ) + ( i.uv_texcoord * _BurnTilling ));
			half4 tex2DNode98 = tex2D( _Masks, panner9 );
			half temp_output_19_0 = ( _CharcoalMix + tex2DNode98.r );
			half3 lerpResult103 = lerp( UnpackNormal( tex2D( _Normals, i.uv_texcoord ) ) , UnpackNormal( appendResult182 ) , temp_output_19_0);
			o.Normal = lerpResult103;
			half4 tex2DNode80 = tex2D( _Albedo, i.uv_texcoord );
			half4 temp_cast_0 = (0.0).xxxx;
			half4 lerpResult28 = lerp( ( tex2DNode80 * _AlbedoMix ) , temp_cast_0 , temp_output_19_0);
			half4 lerpResult148 = lerp( ( float4(0.718,0.0627451,0,1) * ( tex2DNode83.a * 2.95 ) ) , ( float4(0.647,0.06297875,0,1) * ( tex2DNode83.a * 4.2 ) ) , tex2DNode98.g);
			half4 lerpResult152 = lerp( lerpResult28 , ( ( lerpResult148 * tex2DNode98.r ) * _BaseEmber ) , ( tex2DNode98.r * 1.0 ));
			o.Albedo = lerpResult152.rgb;
			half4 temp_cast_2 = (0.0).xxxx;
			half4 temp_cast_3 = (100.0).xxxx;
			half4 clampResult176 = clamp( ( ( tex2DNode98.r * ( ( ( ( _EmberColorTint * _GlowColorIntensity ) * ( ( sin( ( _Time.y * _GlowBaseFrequency ) ) * 0.5 ) + ( _GlowOverride * ( tex2DNode98.r * tex2DNode83.a ) ) ) ) * tex2DNode98.g ) * tex2DNode83.a ) ) * _GlowEmissionMultiplier ) , temp_cast_2 , temp_cast_3 );
			o.Emission = clampResult176.rgb;
			o.Smoothness = ( tex2DNode80.a * _Smoothness );
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=19303
Node;AmplifyShaderEditor.CommentaryNode;128;-3113.25,-277.6554;Inherit;False;1648.54;574.2015;;7;7;9;11;10;98;180;129;;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;39;-2354.221,1634.534;Inherit;False;1523.056;586.484;Base + Burnt Detail Mix (1 Free Alpha channels if needed);9;103;181;182;6;5;179;82;40;183;;1,1,1,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;180;-3032.306,-240.7004;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;11;-3060.807,-39.78358;Float;False;Property;_BurnTilling;Burn Tilling;10;0;Create;True;0;0;0;False;0;False;1;0.484;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-3061.854,59.54606;Float;False;Property;_BurnOffset;Burn Offset;8;0;Create;True;0;0;0;False;0;False;1;0.22;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;130;-2566.58,462.9727;Inherit;False;2529.991;765.4811;Emission;18;157;158;69;66;95;68;67;76;73;77;127;65;70;106;101;170;174;169;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;-2680.848,-125.3553;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;-2308.22,1995.127;Float;False;Property;_CharcoalNormalTile;Charcoal Normal Tile;9;0;Create;True;0;0;0;False;0;False;5;2;2;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;179;-2297.001,1722.1;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;76;-2501.525,1037.474;Float;False;Property;_GlowBaseFrequency;Glow Base Frequency;11;0;Create;True;0;0;0;False;0;False;1.1;2.35;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.TimeNode;67;-2487.243,814.3365;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;9;-2436.848,-67.1541;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;1,0.5;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-2032,1872;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;40;-1862.12,1886.328;Inherit;False;343.3401;246.79;Emission in Alpha;1;83;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;83;-1837.837,1936.235;Inherit;True;Property;_BurntTileNormals;Burnt Tile Normals;14;0;Create;True;0;0;0;False;0;False;-1;None;2fb1b14b4e2147e4a580f44624c73725;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;98;-2187.974,90.68339;Inherit;True;Property;_Masks;Masks;13;0;Create;True;0;0;0;False;0;False;-1;None;21b9f4e0af3140a99ef0bc5a43d58a97;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;68;-2214.131,864.2569;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;95;-2048.016,1006.15;Float;False;Constant;_GlowDuration;Glow Duration;-1;0;Create;True;0;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;171;-2059.027,1470.798;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;169;-2503.727,1130.798;Float;False;Property;_GlowOverride;Glow Override;12;0;Create;True;0;0;0;False;0;False;1;1.07;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;66;-2005.042,836.0363;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;170;-1863.427,1078.999;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;69;-1859.748,866.4651;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;77;-2516.58,713.7126;Float;False;Property;_GlowColorIntensity;Glow Color Intensity;7;0;Create;True;0;0;0;False;0;False;0;3.66;0;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;73;-2500.298,512.9727;Float;False;Property;_EmberColorTint;Ember Color Tint;2;0;Create;True;0;0;0;False;0;False;0.9926471,0.6777384,0,1;0.966,0.1062517,0.004325263,1;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;138;-1244.786,362.2247;Float;False;Constant;_R2;R2;-1;0;Create;True;0;0;0;False;0;False;4.2;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;65;-1833.5,705.4734;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;144;-1201.686,-84.4754;Float;False;Constant;_R2144;R2 144;-1;0;Create;True;0;0;0;False;0;False;2.95;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;174;-1695.621,992.7978;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;147;-1134.788,-277.6757;Float;False;Constant;_ColorNode39134147;ColorNode39134 147;-1;0;Create;True;0;0;0;False;0;False;0.718,0.0627451,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;70;-1650.418,755.3741;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;134;-1253.789,180.1245;Float;False;Constant;_ColorNode39134;ColorNode 39 134;-1;0;Create;True;0;0;0;False;0;False;0.647,0.06297875,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;137;-877.9863,266.6246;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;145;-864.0865,-85.57518;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;101;-1374.632,659.5688;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;129;-2023.204,-221.9194;Inherit;False;471.6918;296.3271;Mix Base Albedo;2;13;19;;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;136;-735.1851,46.52425;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;146;-718.0855,-186.0759;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;38;-1752.147,-1032.491;Inherit;False;1183.903;527.3994;Albedo - Smoothness in Alpha;5;35;27;34;28;80;;1,1,1,1;0;0
Node;AmplifyShaderEditor.LerpOp;148;-532.6986,-105.8688;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;106;-1147.638,615.7524;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;183;-1653.1,1805.106;Float;False;Constant;_Float0;Float 0;15;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;13;-2012.303,-170.8193;Float;False;Property;_CharcoalMix;Charcoal Mix;1;0;Create;True;0;0;0;False;0;False;1;0.713;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;80;-1729.756,-911.4553;Inherit;True;Property;_Albedo;Albedo;3;0;Create;True;0;0;0;False;0;False;-1;None;85e3723e62d44f758723754190c67911;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;35;-1371.507,-835.4999;Float;False;Property;_AlbedoMix;Albedo Mix;0;0;Create;True;0;0;0;False;0;False;0.5;0.356;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;127;-952.7081,532.5618;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;158;-922.8376,723.2053;Float;False;Property;_GlowEmissionMultiplier;Glow Emission Multiplier;6;0;Create;True;0;0;0;False;0;False;1;21.9;0;30;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;149;-348.4115,-25.67536;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;19;-1726.307,-47.39226;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;156;-182.4112,109.8244;Float;False;Constant;_RangedFloatNode156;RangedFloatNode 156;-1;0;Create;True;0;0;0;False;0;False;1;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;150;-535.9109,125.925;Float;False;Property;_BaseEmber;Base Ember;5;0;Create;True;0;0;0;False;0;False;0;0.371;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;182;-1500.6,1970.726;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;34;-1066.102,-910.4909;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-913.8334,-841.804;Float;False;Constant;_RangedFloatNode27;_RangedFloatNode27;-1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;154;41.58921,-45.27597;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;157;-597.8378,569.7058;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.UnpackScaleNormalNode;181;-1341.6,1914.726;Inherit;True;Tangent;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;177;-204.6184,257.1976;Float;False;Constant;_RangedFloatNode177;RangedFloatNode 177;-1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;178;30.78172,469.7978;Float;False;Constant;_RangedFloatNode178;RangedFloatNode 178;-1;0;Create;True;0;0;0;False;0;False;100;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;82;-1360.313,1695.685;Inherit;True;Property;_Normals;Normals;4;0;Create;True;0;0;0;False;0;False;-1;None;abfd39fa1d6a42ba9b322e4301333932;True;2;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;0,0;False;1;FLOAT2;1,0;False;2;FLOAT;1;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;28;-638.6332,-908.8198;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;185;9.815278,-978.4721;Inherit;False;Property;_Smoothness;Smoothness;15;0;Create;True;0;0;0;False;0;False;0;0.13;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;151;24.78982,-722.3491;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;176;257.5815,221.0976;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;103;-1004.304,1816.428;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;152;221.8159,-902.0341;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;184;223.6089,-789.4983;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;915.5276,-898.8098;Half;False;True;-1;2;ASEMaterialInspector;0;0;Standard;AmplifyShaderPack/Burn Effect;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;;3;False;;False;0;False;;0;False;;False;0;Opaque;0;True;True;0;False;Opaque;;Geometry;All;12;all;True;True;True;True;0;False;;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;0;4;10;25;False;0.5;True;0;0;False;;0;False;;0;0;False;;0;False;;1;False;;1;False;;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;True;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;;-1;0;False;;0;0;0;False;0.1;False;;0;False;;False;17;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;16;FLOAT4;0,0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;180;0
WireConnection;7;1;11;0
WireConnection;9;0;7;0
WireConnection;9;1;10;0
WireConnection;5;0;179;0
WireConnection;5;1;6;0
WireConnection;83;1;5;0
WireConnection;98;1;9;0
WireConnection;68;0;67;2
WireConnection;68;1;76;0
WireConnection;171;0;98;1
WireConnection;171;1;83;4
WireConnection;66;0;68;0
WireConnection;170;0;169;0
WireConnection;170;1;171;0
WireConnection;69;0;66;0
WireConnection;69;1;95;0
WireConnection;65;0;73;0
WireConnection;65;1;77;0
WireConnection;174;0;69;0
WireConnection;174;1;170;0
WireConnection;70;0;65;0
WireConnection;70;1;174;0
WireConnection;137;0;83;4
WireConnection;137;1;138;0
WireConnection;145;0;83;4
WireConnection;145;1;144;0
WireConnection;101;0;70;0
WireConnection;101;1;98;2
WireConnection;136;0;134;0
WireConnection;136;1;137;0
WireConnection;146;0;147;0
WireConnection;146;1;145;0
WireConnection;148;0;146;0
WireConnection;148;1;136;0
WireConnection;148;2;98;2
WireConnection;106;0;101;0
WireConnection;106;1;83;4
WireConnection;80;1;180;0
WireConnection;127;0;98;1
WireConnection;127;1;106;0
WireConnection;149;0;148;0
WireConnection;149;1;98;1
WireConnection;19;0;13;0
WireConnection;19;1;98;1
WireConnection;182;0;183;0
WireConnection;182;1;83;2
WireConnection;182;3;83;1
WireConnection;34;0;80;0
WireConnection;34;1;35;0
WireConnection;154;0;98;1
WireConnection;154;1;156;0
WireConnection;157;0;127;0
WireConnection;157;1;158;0
WireConnection;181;0;182;0
WireConnection;82;1;179;0
WireConnection;28;0;34;0
WireConnection;28;1;27;0
WireConnection;28;2;19;0
WireConnection;151;0;149;0
WireConnection;151;1;150;0
WireConnection;176;0;157;0
WireConnection;176;1;177;0
WireConnection;176;2;178;0
WireConnection;103;0;82;0
WireConnection;103;1;181;0
WireConnection;103;2;19;0
WireConnection;152;0;28;0
WireConnection;152;1;151;0
WireConnection;152;2;154;0
WireConnection;184;0;80;4
WireConnection;184;1;185;0
WireConnection;0;0;152;0
WireConnection;0;1;103;0
WireConnection;0;2;176;0
WireConnection;0;4;184;0
ASEEND*/
//CHKSM=DB85C660CE6E8F1022ABEA36537C246FF5D9BC1B