/*
 * This file exists purely to establish aliases for the reorganized classes
 * so as to maintain compatibility with CLRHost v1.0. I suppose these may
 * eventually be removed. Maybe not. We'll see.
 */

#if COMPATIBILITY

using System;

namespace CLROBS
{
    #region Interfaces
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface Plugin
    {
        string Name { get; }
        string Description { get; }

        bool LoadPlugin();
        void UnloadPlugin();

        void OnStartStream();
        void OnStopStream();
    }
    
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSource
    {
        Vector2 Size { get; set; }

        void UpdateSettings();
        void Preprocess();
        void Tick(float seconds);
        void Render(float x, float y, float width, float height);
        void BeginScene();
        void EndScene();
    }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSourceFactory
    {
        string DisplayName { get; }
        string ClassName { get; }

        ImageSource Create(XElement data);
        bool ShowConfiguration(XElement data);
    }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface SettingsPane
    {
        void CreatePane(IntPtr parentHwnd);
        void DestroyPane();

        void ApplySettings();
        void CancelSettings();

        bool HasDefaults();
        void SetDefaults();

        string GetCategory();
    }
    #endregion

    #region Abstracts
    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSource : BetterCLRHost.Abstracts.AbstractImageSource, ImageSource
    {
        public AbstractImageSource() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSourceFactory : BetterCLRHost.Abstracts.AbstractImageSourceFactory, ImageSourceFactory
    {
        public AbstractImageSourceFactory() : base() { }

        ImageSource ImageSourceFactory.Create(XElement data)
        {
            return (ImageSource)Create(data);
        }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractPlugin : BetterCLRHost.Abstracts.AbstractPlugin, Plugin
    {
        public AbstractPlugin() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractWPFSettingsPane : BetterCLRHost.Abstracts.AbstractWPFSettingsPane, SettingsPane
    {
        public AbstractWPFSettingsPane() : base() { }

        public abstract string GetCategory();
    }
    #endregion

    #region Concretes
    public class XElement : BetterCLRHost.XElement
    {
        public XElement(long elementPtr) : base(elementPtr) { }
        //public unsafe XElement(IntPtr elementPtr) : base(elementPtr.ToPointer()) { }
    }
    #endregion
}

#endif