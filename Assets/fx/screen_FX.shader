// Made with Amplify Shader Editor v1.9.2.2
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "screen_FX"
{
	Properties
	{
		_TlingOffset("TlingOffset", Vector) = (1,1,0,0)
		_MainTex("MainTex", 2D) = "white" {}
		_Panner("Panner", Vector) = (1,1,0,0)
		_Color("Color", Color) = (1,1,1,1)
		_Scale("Scale", Float) = 1
		_Alpha("Alpha", Float) = 1
		_RGBA_MASK("RGBA_MASK", Vector) = (0,0,0,0)
		_mask("mask", 2D) = "white" {}
		_maskTlingOffset("maskTlingOffset", Vector) = (1,1,0,0)
		[Toggle(_KEYWORD0_ON)] _Keyword0("off/on", Float) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" "Queue"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend SrcAlpha OneMinusSrcAlpha
		AlphaToMask Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest Always
		Offset 0 , 0
		
		
		
		Pass
		{
			Name "Unlit"

			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature_local _KEYWORD0_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _MainTex;
			uniform half4 _Panner;
			uniform half4 _TlingOffset;
			uniform half4 _Color;
			uniform half _Scale;
			uniform sampler2D _mask;
			uniform half4 _maskTlingOffset;
			uniform half4 _RGBA_MASK;
			uniform half _Alpha;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				half mulTime20 = _Time.y * _Panner.z;
				half2 appendResult19 = (half2(_Panner.x , _Panner.y));
				float4 screenPos = i.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				half2 appendResult40 = (half2(ase_screenPosNorm.x , ase_screenPosNorm.y));
				half2 temp_output_41_0 = (appendResult40*2.0 + -1.0);
				half2 break43 = temp_output_41_0;
				half2 appendResult46 = (half2(length( temp_output_41_0 ) , (0.0 + (atan2( break43.y , break43.x ) - 0.0) * (1.0 - 0.0) / (3.141593 - 0.0))));
				half2 appendResult14 = (half2(_TlingOffset.x , _TlingOffset.y));
				half2 appendResult15 = (half2(_TlingOffset.z , _TlingOffset.w));
				half2 panner10 = ( ( mulTime20 + _Panner.w ) * appendResult19 + (appendResult46*appendResult14 + appendResult15));
				half2 appendResult9 = (half2(ase_screenPosNorm.x , ase_screenPosNorm.y));
				half2 appendResult34 = (half2(_maskTlingOffset.x , _maskTlingOffset.y));
				half2 appendResult36 = (half2(_maskTlingOffset.z , _maskTlingOffset.w));
				#ifdef _KEYWORD0_ON
				half staticSwitch47 = ( ( _RGBA_MASK.x * _Color.r ) + ( _RGBA_MASK.y * _Color.g ) + ( _RGBA_MASK.z * _Color.b ) + ( _RGBA_MASK.w * _Color.a ) );
				#else
				half staticSwitch47 = 1.0;
				#endif
				half4 appendResult6 = (half4(( tex2D( _MainTex, panner10 ) * _Color * _Scale ).rgb , ( ( tex2D( _mask, (appendResult9*appendResult34 + appendResult36) ).r * 1.0 * staticSwitch47 ) * _Alpha )));
				
				
				finalColor = appendResult6;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	Fallback Off
}
/*ASEBEGIN
Version=19202
Node;AmplifyShaderEditor.RangedFloatNode;5;-266.308,256.4986;Inherit;False;Property;_Scale;Scale;4;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-302,28.16666;Inherit;False;Property;_Color;Color;3;0;Create;True;0;0;0;False;0;False;1,1,1,1;1,0,0,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;18;-1394.448,170.4352;Inherit;False;Property;_Panner;Panner;2;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;22;116.7259,105.5664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;113.8226,206.9287;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;112.756,315.7285;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;25;107.4227,433.0621;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;29;355.6647,196.1688;Inherit;False;4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;14;-1453.579,-145.5162;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector4Node;13;-1702.913,-110.8495;Inherit;False;Property;_TlingOffset;TlingOffset;0;0;Create;True;0;0;0;False;0;False;1,1,0,0;1,0,0,0.77;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;15;-1443.58,20.48392;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;32;-194.7681,779.7968;Inherit;False;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;34;-589.2756,747.645;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;36;-579.2766,913.6451;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;31;131.7299,715.6369;Inherit;True;Property;_mask;mask;7;0;Create;True;0;0;0;False;0;False;-1;84b27aa5f2158ec4891a87e7b4ee0b1d;e899b5b1fb7830546acd8033438d82f8;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;248.9049,936.67;Inherit;False;Constant;_maskScale;maskScale;9;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;35;-838.6099,782.3117;Inherit;False;Property;_maskTlingOffset;maskTlingOffset;8;0;Create;True;0;0;0;False;0;False;1,1,0,0;3.88,1.22,-1.44,-0.13;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;45;-2415.191,49.06743;Inherit;True;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;3.141593;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ATan2OpNode;44;-2690.295,-24.81089;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;8;-3529.798,-1163.054;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LengthOpNode;42;-2547.075,-445.7028;Inherit;True;1;0;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;9;-1627.923,-1134.813;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;11;-1176.486,-284.1513;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT2;1,0;False;2;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;41;-3152.311,-537.389;Inherit;True;3;0;FLOAT2;0,0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;43;-2896.083,-278.6366;Inherit;True;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;7;358.1124,390.3473;Inherit;False;Property;_Alpha;Alpha;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;28;-235.9355,376.7003;Inherit;False;Property;_RGBA_MASK;RGBA_MASK;6;0;Create;True;0;0;0;False;0;False;0,0,0,0;1,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;46;-2037.378,-291.0683;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;40;-3142.206,-917.041;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;48;416.6907,89.11531;Inherit;False;Constant;_Float0;Float 0;10;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;37;819.0904,512.3718;Inherit;True;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;1058.127,284.2942;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1517.178,-238.1941;Half;False;True;-1;2;ASEMaterialInspector;100;5;screen_FX;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;2;5;False;;10;False;;0;1;False;;0;False;;True;0;False;;0;False;;False;False;False;False;False;False;False;False;False;True;0;False;;True;True;2;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;True;True;2;False;;True;7;False;;True;True;0;False;;0;False;;True;2;RenderType=Opaque=RenderType;Queue=Transparent=Queue=0;True;2;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;638356600989505250;0;1;True;False;;False;0
Node;AmplifyShaderEditor.StaticSwitch;47;651.1498,189.4728;Inherit;False;Property;_Keyword0;off/on;9;0;Create;False;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;1243.974,-151.2959;Inherit;True;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;2;-0.666569,-216.5;Inherit;True;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;21;-768.7072,275.1817;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;10;-692.1898,-284.2231;Inherit;True;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;1;-386.2968,-381.4915;Inherit;True;Property;_MainTex;MainTex;1;0;Create;True;0;0;0;False;0;False;-1;cf134bfc6af673445b5abdbcd4043ae2;88dd5a92bf704db4f80d63102084f417;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleTimeNode;20;-901.1694,91.29404;Inherit;True;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;19;-1155.698,-51.35515;Inherit;True;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
WireConnection;22;0;28;1
WireConnection;22;1;4;1
WireConnection;23;0;28;2
WireConnection;23;1;4;2
WireConnection;24;0;28;3
WireConnection;24;1;4;3
WireConnection;25;0;28;4
WireConnection;25;1;4;4
WireConnection;29;0;22;0
WireConnection;29;1;23;0
WireConnection;29;2;24;0
WireConnection;29;3;25;0
WireConnection;14;0;13;1
WireConnection;14;1;13;2
WireConnection;15;0;13;3
WireConnection;15;1;13;4
WireConnection;32;0;9;0
WireConnection;32;1;34;0
WireConnection;32;2;36;0
WireConnection;34;0;35;1
WireConnection;34;1;35;2
WireConnection;36;0;35;3
WireConnection;36;1;35;4
WireConnection;31;1;32;0
WireConnection;45;0;44;0
WireConnection;44;0;43;1
WireConnection;44;1;43;0
WireConnection;42;0;41;0
WireConnection;9;0;8;1
WireConnection;9;1;8;2
WireConnection;11;0;46;0
WireConnection;11;1;14;0
WireConnection;11;2;15;0
WireConnection;41;0;40;0
WireConnection;43;0;41;0
WireConnection;46;0;42;0
WireConnection;46;1;45;0
WireConnection;40;0;8;1
WireConnection;40;1;8;2
WireConnection;37;0;31;1
WireConnection;37;1;39;0
WireConnection;37;2;47;0
WireConnection;30;0;37;0
WireConnection;30;1;7;0
WireConnection;0;0;6;0
WireConnection;47;1;48;0
WireConnection;47;0;29;0
WireConnection;6;0;2;0
WireConnection;6;3;30;0
WireConnection;2;0;1;0
WireConnection;2;1;4;0
WireConnection;2;2;5;0
WireConnection;21;0;20;0
WireConnection;21;1;18;4
WireConnection;10;0;11;0
WireConnection;10;2;19;0
WireConnection;10;1;21;0
WireConnection;1;1;10;0
WireConnection;20;0;18;3
WireConnection;19;0;18;1
WireConnection;19;1;18;2
ASEEND*/
//CHKSM=98CE234CA6FF33A5234B89759D8558F18D4F6033