namespace CLROBS.Interfaces
{
    public interface IImageSourceFactory
    {
        IImageSource Create(XElement data);
        bool ShowConfiguration(XElement data);

        string DisplayName { get; }
        string ClassName { get; }
    }
}
