/*
 * This file exists purely to establish aliases for the reorganized classes
 * so as to maintain compatibility with CLRHost v1.0. I suppose these may
 * eventually be removed. Maybe not. We'll see.
 */

using System;

namespace CLROBS
{
    #region Interfaces
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface Plugin : Interfaces.IPlugin { }
    
    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSource : Interfaces.IImageSource { }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface ImageSourceFactory : Interfaces.IImageSourceFactory { }

    [Obsolete("This interface provided for compatibility purposes only")]
    public interface SettingsPane : Interfaces.ISettingsPane { }
    #endregion

    #region Abstracts
    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSource : Abstracts.AbstractImageSource
    {
        public AbstractImageSource() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractImageSourceFactory : Abstracts.AbstractImageSourceFactory
    {
        public AbstractImageSourceFactory() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractPlugin : Abstracts.AbstractPlugin
    {
        public AbstractPlugin() : base() { }
    }

    [Obsolete("This class provided for compatibility purposes only")]
    public abstract class AbstractWPFSettingsPane : Abstracts.AbstractWPFSettingsPane
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
