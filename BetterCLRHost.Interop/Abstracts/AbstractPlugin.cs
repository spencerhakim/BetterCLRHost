namespace CLROBS.Abstracts
{
    public abstract class AbstractPlugin : Interfaces.IPlugin
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public AbstractPlugin()
        {
            Name = "Default Plugin Name";
            Description = "Default Plugin Description";
        }

        public virtual bool LoadPlugin(){ return true; }
        public virtual void UnloadPlugin(){}

        public virtual void OnStartStream(){}
        public virtual void OnStopStream(){}

        public virtual void OnStartStreaming(){}
        public virtual void OnStopStreaming(){}

        public virtual void OnStartRecording(){}
        public virtual void OnStopRecording(){}

        public virtual void OnOBSStatus(bool running, bool streaming, bool recording, bool previewing, bool reconnecting){}
        public virtual void OnStreamStatus(bool streaming, bool previewOnly, uint bytesPerSec, double strain, uint totalStreamTime, uint totalNumFrames, uint numDroppedFrames, uint fps){}

        public virtual void OnSceneSwitch(string scene){}
        public virtual void OnScenesChanged(){}

        public virtual void OnSourceOrderChanged(){}
        public virtual void OnSourceChanged(string sourceName, XElement source){}
        public virtual void OnSourcesAddedOrRemoved(){}

        public virtual void OnMicVolumeChanged(float level, bool muted, bool finalValue){}
        public virtual void OnDesktopVolumeChanged(float level, bool muted, bool finalValue){}

        public virtual void OnLogUpdate(string delta, uint length){}
    }
}
