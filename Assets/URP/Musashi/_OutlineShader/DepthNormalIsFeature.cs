using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DepthNormalIsFeature : ScriptableRendererFeature
{
    class DepthNormalsPass : ScriptableRenderPass
    {
        int m_kDepthBufferBits = 32;
        private RenderTargetHandle DepthAttachmentHandle { get; set; }
        internal RenderTextureDescriptor Descriptor { get; private set; }

        private Material m_depthNormalIsMaterial = null;
        private FilteringSettings m_filteringSettings;
        string m_profilerTag = "DepthNormals Prepass";
        ShaderTagId m_ShaderTagId = new ShaderTagId("DepthOnly");

        /// <summary>
        /// Constructor
        /// </summary>
        public DepthNormalsPass(RenderQueueRange renderQueueRange, LayerMask layerMask, Material material)
        {
            m_filteringSettings = new FilteringSettings(renderQueueRange, layerMask);
            m_depthNormalIsMaterial = material;
        }

        public void Setup(RenderTextureDescriptor baseDescriptor, RenderTargetHandle depthAttachmenHand)
        {
            this.DepthAttachmentHandle = depthAttachmenHand;
            baseDescriptor.colorFormat = RenderTextureFormat.ARGB32;
            baseDescriptor.depthBufferBits = m_kDepthBufferBits;
            Descriptor = baseDescriptor;
        }

        /// <summary>
        /// This method is ccalled before executing the render pass.
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="cameraTextureDescriptor"></param>
        public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
        {
            cmd.GetTemporaryRT(DepthAttachmentHandle.id, Descriptor, FilterMode.Point);
            ConfigureTarget(DepthAttachmentHandle.Identifier());
            ConfigureClear(ClearFlag.All, Color.black);
        }

        /// <summary>
        /// Here you can implement the rendering logic 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="renderingData"></param>
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get(m_profilerTag);


#pragma warning disable CS0618 // Type or member is obsolete
            using (new ProfilingSample(cmd, m_profilerTag))
#pragma warning restore CS0618 // Type or member is obsolete
            {
                context.ExecuteCommandBuffer(cmd);
                cmd.Clear();

                var sortFlags = renderingData.cameraData.defaultOpaqueSortFlags;
                var drawSettings = CreateDrawingSettings(m_ShaderTagId, ref renderingData, sortFlags);
                drawSettings.perObjectData = PerObjectData.None;

                ref CameraData cameraData = ref renderingData.cameraData;
                Camera camera = cameraData.camera;
                if (cameraData.isStereoEnabled)
                    context.StartMultiEye(camera);


                drawSettings.overrideMaterial = m_depthNormalIsMaterial;

                context.DrawRenderers(renderingData.cullResults, ref drawSettings,
                    ref m_filteringSettings);

                cmd.SetGlobalTexture("_CameraDepthNormalsTexture", DepthAttachmentHandle.id);
            }

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        /// Cleanup any allocated resources that were created during the execution of this render pass.
        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (DepthAttachmentHandle != RenderTargetHandle.CameraTarget)
            {
                cmd.ReleaseTemporaryRT(DepthAttachmentHandle.id);
                DepthAttachmentHandle = RenderTargetHandle.CameraTarget;
            }
        }
    }

    DepthNormalsPass m_depthNormalsPass;
    RenderTargetHandle m_depthNormalsTexture;
    Material m_depthNormalsMaterial;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        m_depthNormalsPass.Setup(renderingData.cameraData.cameraTargetDescriptor, m_depthNormalsTexture);
        renderer.EnqueuePass(m_depthNormalsPass);
    }

    public override void Create()
    {
        m_depthNormalsMaterial = CoreUtils.CreateEngineMaterial("Hidden/Internal-DepthNormalsTexture");
        m_depthNormalsPass = new DepthNormalsPass(RenderQueueRange.opaque, -1, m_depthNormalsMaterial);
        m_depthNormalsPass.renderPassEvent = RenderPassEvent.AfterRenderingPrePasses;
        m_depthNormalsTexture.Init("_CameraDepthNormalsTexture");
    }
}
