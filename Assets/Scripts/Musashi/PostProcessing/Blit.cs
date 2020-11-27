using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace UnityEngine.Rendering.LWRP
{
    /// <summary>
    /// OutLineをポストエフェクトで実装する
    /// </summary>
    public class Blit : ScriptableRendererFeature//URP自作ポストエフェクトするために必要な抽象クラス
    {
        /// <summary>
        /// Renderer Featuresのインスペクターに表示されるクラス
        /// </summary>
        [System.Serializable]
        public class BlitSettings 
        {
            public RenderPassEvent Event = RenderPassEvent.AfterRenderingOpaques;
            public Material blitMaterial = null;
            public int blitMaterialPassIndex = -1;
            public Target destination = Target.Color;
            public string textureId = "_BlitPassTexture";
        }

        public enum Target
        {
            Color,
            Texture
        }

        public BlitSettings m_settings = new BlitSettings();
        RenderTargetHandle m_renderTextureHandle;

        BlitPass m_blitPass;

        /// <summary>
        /// レンダーパスの作成を行う
        /// </summary>
        public override void Create()
        {
            var passIndex = m_settings.blitMaterial != null ? m_settings.blitMaterial.passCount - 1 : 1;
            m_settings.blitMaterialPassIndex = Mathf.Clamp(m_settings.blitMaterialPassIndex, -1, passIndex);
            m_blitPass = new BlitPass(m_settings.Event, m_settings.blitMaterial, m_settings.blitMaterialPassIndex, name);
            m_renderTextureHandle.Init(m_settings.textureId);
        }

        /// <summary>
        /// レンダーパスの追加を行う
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="renderingData"></param>
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var src = renderer.cameraColorTarget;
            var dest = (m_settings.destination == Target.Color) ? RenderTargetHandle.CameraTarget : m_renderTextureHandle;

            if (m_settings.blitMaterial == null)
            {
                Debug.LogWarningFormat("Missing Blit Material. {0} blit pass will not execute. Check for missing reference in the assigned renderer.", GetType().Name);
                return;
            }

            m_blitPass.Setup(src, dest);
            renderer.EnqueuePass(m_blitPass);
        }
    }

    /// <summary>
    /// AddRenderPassesコールバック内でURPのRendererを触れるようになっており、
    /// ここで独自のScriptableRenderPassを登録して描画パスをパイプラインに差し込む。
    /// </summary>
    internal class BlitPass : ScriptableRenderPass
    {
        public enum RenderTarget
        {
            Color,
            RenderTexture,
        };

        public Material m_blitMaterial = null;
        public int m_blitShaderPassIndex = 0;

        public FilterMode FilterMode { get; set; }

        private RenderTargetIdentifier Source { get; set; }

        private RenderTargetHandle Destination { get; set; }

        RenderTargetHandle m_temporaryColorTexture;
        string m_profilerTag;

        public BlitPass(RenderPassEvent renderPassEvent,Material blitMaterial,int blitShaderPassIndex,string tag)
        {
            this.renderPassEvent = renderPassEvent;
            this.m_blitMaterial = blitMaterial; 
            this.m_blitShaderPassIndex = blitShaderPassIndex;
            m_profilerTag = tag;
            m_temporaryColorTexture.Init("_TemporaryColorTexture");
        }

        public void Setup(RenderTargetIdentifier source, RenderTargetHandle destination)
        {
            this.Source = source;
            this.Destination = destination;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(m_profilerTag);

            RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
            opaqueDesc.depthBufferBits = 0;

            // Can't read and write to same color target, create a temp render target to blit. 
            if (Destination == RenderTargetHandle.CameraTarget)
            {
                cmd.GetTemporaryRT(m_temporaryColorTexture.id, opaqueDesc, FilterMode);
                Blit(cmd, Source, m_temporaryColorTexture.Identifier(), m_blitMaterial, m_blitShaderPassIndex);
                Blit(cmd, m_temporaryColorTexture.Identifier(), Source);
            }
            else
            {
                Blit(cmd, Source, Destination.Identifier(), m_blitMaterial, m_blitShaderPassIndex);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (Destination == RenderTargetHandle.CameraTarget)
                cmd.ReleaseTemporaryRT(m_temporaryColorTexture.id);
        }
    }
}


