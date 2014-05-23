namespace CLROBS.Interfaces
{
    public interface IImageSource
    {
        void Preprocess();
        void Tick(float seconds);
        void Render(float x, float y, float width, float height);

        Vector2 Size { get; set; }
        
        void UpdateSettings();
        void BeginScene();
        void EndScene();
    }
}
