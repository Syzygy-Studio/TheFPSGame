﻿Shader "Custom/NewSurfaceShader" {
	SubShader {
		
		Pass {
			CGPROGRAM
			#include "UnityCG.cginc"  

			#pragma vertex vert
			#pragma fragment frag
			sampler2D _CameraDepthTexture;
            sampler2D _InvFade;

			struct v2f{
				float4 vertex:POSITION;
				float3 normal:NORMAL;
				float4 texcoord:TEXCOORD0;
				float4 pos:SV_POSITION;
				float4 projPos:POSITION;
				float2 uv : TEXCOORD0;
				float3 normalDir : TEXCOORD0;
				float4 posWorld : TEXCOORD0;

			};
			//struct appdata_base{
				//float4 vertex:POSITION;
				//float3 normal:NORMAL;
				//float4 texcoord:TEXCOORD0;
			//};



			v2f vert(appdata_base v) {				
				v2f o;
				o.pos = UnityObjectToClipPos ( v.vertex );
  				o.projPos = ComputeScreenPos ( o.pos );
		        o.uv = v.texcoord.xy;
		        o.normalDir = UnityObjectToWorldNormal ( v.normal );
		        o.posWorld = mul ( UNITY_MATRIX_M, v.vertex );
    			return o;
    			}

    		fixed4 frag ( v2f i ) : COLOR {
    
		        fixed alpha = 1;
		        float sceneZ = LinearEyeDepth ( SAMPLE_DEPTH_TEXTURE_PROJ ( _CameraDepthTexture, UNITY_PROJ_COORD ( i.projPos ) ) );
		        float partZ = i.projPos.z;
		        float fade = saturate ( _InvFade * ( sceneZ - partZ ) );
		        alpha *= fade;                
		        float3 viewDirection = normalize ( _WorldSpaceCameraPos.xyz - i.posWorld.xyz );          		                 
		        float4 objectOrigin = mul ( unity_ObjectToWorld, float4 ( 0.0, 0.0, 0.0, 1.0 ) );
		        float dist = distance ( _WorldSpaceCameraPos.xyz, objectOrigin.xyz );
		        float2 wcoord = i.projPos.xy / i.projPos.w;
				wcoord.x *= _Inter.y;
		        wcoord.y *= _Inter.z;
		        wcoord *= dist * _Inter.x;		        
		        float3 nMask = _Strength;		                 
		        float3 hMask = tex2D( _MainTex, wcoord + float2 ( 0, _Time.x * _Inter.w ) );
		        float fresnel = pow ( abs ( dot ( viewDirection, i.normalDir ) ), _FresPow ) * _FresMult;
		        float3 bLayer = lerp ( _bLayerColorA, _bLayerColorB, fresnel );
		        float fresnelOut = pow ( 1 - abs ( dot ( viewDirection, i.normalDir ) ), _FresPowOut ) * _FresMultOut;
		        float3 bLayerC = _bLayerColorC * fresnelOut;
		        float3 final = saturate ( ( hMask + nMask ) * ( bLayer + bLayerC ) ) * alpha;		            
		        return float4 ( final * _Fade, 1) ;
    		}   
			
		    
		ENDCG	
		}

	}
	FallBack "Diffuse"
}
