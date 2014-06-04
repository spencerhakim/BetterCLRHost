namespace BetterCLRHost.Interfaces
{
    public interface IImageSourceFactory
    {
        string DisplayName { get; }
        string ClassName { get; }

        IImageSource Create(CLROBS.XElement data);
        bool ShowConfiguration(CLROBS.XElement data);
    }
}
