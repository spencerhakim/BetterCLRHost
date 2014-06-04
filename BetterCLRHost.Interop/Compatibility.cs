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
    public interface Plugin : Interfaces.IPlugin { }
    
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSource : Interfaces.IImageSource { }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSourceFactory : Interfaces.IImageSourceFactory
    {
        new ImageSource Create(XElement data);
    }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface SettingsPane : Interfaces.ISettingsPane { }
    #endregion

    #region Abstracts
    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSource : Abstracts.AbstractImageSource, ImageSource
    {
        public AbstractImageSource() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSourceFactory : Abstracts.AbstractImageSourceFactory, ImageSourceFactory
    {
        public AbstractImageSourceFactory() : base() { }

        ImageSource ImageSourceFactory.Create(XElement data)
        {
            return (ImageSource)Create(data);
        }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractPlugin : Abstracts.AbstractPlugin, Plugin
    {
        public AbstractPlugin() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractWPFSettingsPane : Abstracts.AbstractWPFSettingsPane, SettingsPane
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