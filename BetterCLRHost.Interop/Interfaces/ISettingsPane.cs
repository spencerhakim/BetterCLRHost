using System;

namespace BetterCLRHost.Interfaces
{
    public interface ISettingsPane
    {
        void CreatePane(IntPtr parentHwnd);
        void DestroyPane();

        void ApplySettings();
        void CancelSettings();

        bool HasDefaults();
        void SetDefaults();

        string Category { get; }
    }
}
