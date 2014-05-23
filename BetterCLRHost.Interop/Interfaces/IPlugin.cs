namespace CLROBS.Interfaces
{
    public interface IPlugin
    {
        string Name { get; }
        string Description { get; }

        bool LoadPlugin();
        void UnloadPlugin();

        void OnStartStream();
        void OnStopStream();

        void OnStartStreaming();
        void OnStopStreaming();

        void OnStartRecording();
        void OnStopRecording();

        void OnOBSStatus(bool running, bool streaming, bool recording, bool previewing, bool reconnecting);
        void OnStreamStatus(bool streaming, bool previewOnly, uint bytesPerSec, double strain, uint totalStreamTime, uint totalNumFrames, uint numDroppedFrames, uint fps);

        void OnSceneSwitch(string scene);
        void OnScenesChanged();

        void OnSourceOrderChanged();
        void OnSourceChanged(string sourceName, XElement source);
        void OnSourcesAddedOrRemoved();

        void OnMicVolumeChanged(float level, bool muted, bool finalValue);
        void OnDesktopVolumeChanged(float level, bool muted, bool finalValue);

        void OnLogUpdate(string delta, uint length);
    }
}