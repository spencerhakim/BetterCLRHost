using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLROBS.Abstracts
{
    public abstract class AbstractImageSourceFactory : Interfaces.IImageSourceFactory
    {
        public string ClassName { get; protected set; }
        public string DisplayName { get; protected set; }

        public AbstractImageSourceFactory()
        {
            ClassName = "DefaultImageSourceClassName";
            DisplayName = "DefaultImageSourceDisplayName";
        }

        public abstract Interfaces.IImageSource Create(XElement data);
        public abstract bool ShowConfiguration(XElement data);
    }
}
