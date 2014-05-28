using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WP.Core
{
    public class EventToCommand
    {
        #region Attached Property

        public static readonly DependencyProperty CommandProperty =
    DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(EventToCommand), new PropertyMetadata(null));

        public static readonly DependencyProperty CommandParameterProperty =
DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(EventToCommand), new PropertyMetadata(null));

        public static readonly DependencyProperty EventProperty =
    DependencyProperty.RegisterAttached("Event", typeof(RoutedEvent), typeof(EventToCommand), new PropertyMetadata(null, EventChanged));

        #endregion

        #region Methods

        #region Command

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }

	    #endregion

        #region CommandParameter

        public static object GetCommandParameter(DependencyObject obj)
        {
            return (object)obj.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(DependencyObject obj, object value)
        {
            obj.SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region Event

        public static RoutedEvent GetEvent(DependencyObject obj)
        {
            return (RoutedEvent)obj.GetValue(EventProperty);
        }

        public static void SetEvent(DependencyObject obj, RoutedEvent value)
        {
            obj.SetValue(EventProperty, value);
        }

        #endregion
        
        static void EventChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var ele = sender as UIElement;
            if (ele != null)
                ele.AddHandler((RoutedEvent)e.NewValue, new RoutedEventHandler(DoCommand), true);
        }

        static void DoCommand(object sender, RoutedEventArgs e)
        {
            var ele = sender as FrameworkElement;
            if (ele != null)
            {
                var command = (ICommand)ele.GetValue(EventToCommand.CommandProperty);
                if (command != null)
                {
                    var parameter = ele.GetValue(EventToCommand.CommandParameterProperty);
                    parameter = parameter == null ? e : parameter;
                    command.Execute(parameter);
                }
            }
        }

	    #endregion
    }
}
