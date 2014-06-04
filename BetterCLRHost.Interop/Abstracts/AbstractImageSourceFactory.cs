using BetterCLRHost.Interfaces;

namespace BetterCLRHost.Abstracts
{
    public abstract class AbstractImageSourceFactory : IImageSourceFactory
    {
        public string ClassName { get; protected set; }
        public string DisplayName { get; protected set; }

        public AbstractImageSourceFactory()
        {
            ClassName = "DefaultImageSourceClassName";
            DisplayName = "DefaultImageSourceDisplayName";
        }

        public abstract IImageSource Create(CLROBS.XElement data);
        public abstract bool ShowConfiguration(CLROBS.XElement data);
    }
}
