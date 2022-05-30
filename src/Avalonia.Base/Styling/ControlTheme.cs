using System;

namespace Avalonia.Styling
{
    /// <summary>
    /// Defines a switchable theme for a control.
    /// </summary>
    public class ControlTheme : StyleBase
    {
        /// <summary>
        /// Gets or sets the type for which this control theme is intended.
        /// </summary>
        public Type? TargetType { get; set; }

        protected override SelectorMatch Matches(IStyleable target, object? host)
        {
            if (TargetType is null)
                throw new InvalidOperationException("ControlTheme has no TargetType.");

            // If target and host are the same control, then we're applying styles to the control
            // that the theme is applied to.
            if (target == host)
            {
                if (target.StyleKey != TargetType)
                    throw new InvalidOperationException(
                        $"Cannot apply a ControlTheme for '{TargetType}' to an instance of {target.GetType()}.");
                return SelectorMatch.AlwaysThisType;
            }

            // If the target is different to the host then we're applying styles to a templated
            // child of the host. The setters in the control theme itself don't apply here: only
            // the child styles.
            return SelectorMatch.NeverThisType;
        }
    }
}
