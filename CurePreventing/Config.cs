using Exiled.API.Interfaces;
using System.ComponentModel;

namespace CurePreventing
{
    public sealed class Config : IConfig
    {
        [Description("Whether or not this plugin is enabled.")]
        public bool IsEnabled { get; set; } = true;

        [Description("The amount of time that using SCP-500 should protect you.")]
        public ushort PreventionTime { get; set; } = 120;

        [Description("Whether or not attacking SCP-049 should allow SCP-049 to retaliate.")]
        public bool Hurting049ResetsProtection { get; set; } = false;

        [Description("Whether or not hints should be used instea of broadcasts.")]
        public bool UseHints { get; set; } = true;

        [Description("The message that should be show to SCP-049 when they try to kill someone who has used SCP-500.")]
        public string Message { get; set; } = "<color=#f00>{player} does not have the pestilence!</color>";
    }
}