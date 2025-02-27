using UnityEngine;
using UnityEngine.Video;
using System.Collections;

namespace HardCoded.VRigUnity {
    public class VideoFileSource : ImageSource {
        public string videoURL = System.IO.Path.Combine(Application.streamingAssetsPath, "webcamtest1.mp4");
        private VideoPlayer videoPlayer;
        private RenderTexture renderTexture;

        public override Texture CurrentTexture {
            get {
                return videoPlayer != null ? videoPlayer.texture : null;
            }
        }

        public override int TextureWidth {
            get { return videoPlayer != null && videoPlayer.texture != null ? videoPlayer.texture.width : 0; }
        }
        public override int TextureHeight {
            get { return videoPlayer != null && videoPlayer.texture != null ? videoPlayer.texture.height : 0; }
        }

        public override bool IsPrepared => videoPlayer != null && videoPlayer.isPrepared;
        public override bool IsPlaying => videoPlayer != null && videoPlayer.isPlaying;
        public override bool IsVerticallyFlipped { get => false; set { } }
        public override string SourceName => "videoSource";
        public override string[] SourceCandidateNames => new string[] { "videoSource" };

        public override IEnumerator Play() {
            videoPlayer = gameObject.AddComponent<VideoPlayer>();
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = videoURL;
            videoPlayer.renderMode = VideoRenderMode.RenderTexture;
            renderTexture = new RenderTexture(1920, 1080, 0);
            videoPlayer.targetTexture = renderTexture;
            videoPlayer.Prepare();
            while (!videoPlayer.isPrepared) {
                yield return null;
            }
            videoPlayer.Play();
            yield return null;
        }

        public override void Stop() {
            if (videoPlayer != null) {
                videoPlayer.Stop();
                Destroy(videoPlayer);
                videoPlayer = null;
            }
        }

        public override ResolutionStruct[] GetResolutions() {
          return null;
        }

        public override void UpdateFromSettings() {

        }
    }
}