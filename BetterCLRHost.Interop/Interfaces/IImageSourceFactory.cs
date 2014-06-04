namespace CLROBS.Interfaces
{
    public interface IImageSourceFactory
    {
        string DisplayName { get; }
        string ClassName { get; }

        IImageSource Create(XElement data);
        bool ShowConfiguration(XElement data);
    }
}
