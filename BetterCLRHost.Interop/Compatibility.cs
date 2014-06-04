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
    public interface Plugin : BetterCLRHost.Interfaces.IPlugin { }
    
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSource : BetterCLRHost.Interfaces.IImageSource { }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSourceFactory : BetterCLRHost.Interfaces.IImageSourceFactory
    {
        new ImageSource Create(XElement data);
    }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface SettingsPane : BetterCLRHost.Interfaces.ISettingsPane { }
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