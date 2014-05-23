using System;

namespace CLROBS.Interfaces
{
    public interface ISettingsPane
    {
        [Obsolete("Use the IntPtr version of this method")]
        long CreatePane(long parentHwnd);
        IntPtr CreatePane(IntPtr parentHwnd);
        void DestroyPane();

        void ApplySettings();
        void CancelSettings();

        bool HasDefaults();
        void SetDefaults();

        string Category { get; }
    }
}
