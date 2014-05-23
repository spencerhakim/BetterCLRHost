namespace CLROBS.Abstracts
{
    public abstract class AbstractImageSource : Interfaces.IImageSource
    {
        public Vector2 Size { get; set; }

        public virtual void Preprocess() { }
        public virtual void Tick(float seconds) { }
        public abstract void Render(float x, float y, float width, float height);
        public abstract void UpdateSettings();
        public virtual void BeginScene() { }
        public virtual void EndScene() { }

        //TODO - protected API Api { get { return API.Instance; } }
        //TODO - protected GraphicsSystem GS { get { return GraphicsSystem.Instance; } }
    }
}
