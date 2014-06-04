namespace CLROBS.Interfaces
{
    public interface IImageSource
    {
        Vector2 Size { get; set; }

        void UpdateSettings();
        void Preprocess();
        void Tick(float seconds);
        void Render(float x, float y, float width, float height);
        void BeginScene();
        void EndScene();
    }
}
