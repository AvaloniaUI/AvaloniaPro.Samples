using Avalonia.Controls;

namespace Avalonia.Media.Demo.Controls.Behaviors
{
    public abstract class MiniBehavior : AvaloniaObject
    {
        public static readonly AttachedProperty<MiniBehavior> BehaviorProperty =
            AvaloniaProperty.RegisterAttached<MiniBehavior, Control, MiniBehavior>(
                "Behavior");

        private Control? _associatedObject;

        static MiniBehavior()
        {
            BehaviorProperty.Changed.AddClassHandler<Control>(OnBehaviorChanged);
        }

        private static void OnBehaviorChanged(Control control, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.OldValue is MiniBehavior oldBehavior)
            {
                oldBehavior.OnDetaching();
                oldBehavior._associatedObject = null;
            }

            if (e.NewValue is MiniBehavior newBehavior)
            {
                newBehavior._associatedObject = control;
                newBehavior.OnAttached();
            }
        }

        public static void SetBehavior(Control element, MiniBehavior value)
        {
            element.SetValue(BehaviorProperty, value);
        }

        public static MiniBehavior GetBehavior(Control element)
        {
            return element.GetValue(BehaviorProperty);
        }

        protected virtual void OnAttached()
        {
        }

        protected virtual void OnDetaching()
        {
        }

        protected Control? AssociatedObject => _associatedObject;
    }
}
