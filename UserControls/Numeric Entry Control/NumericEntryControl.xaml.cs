/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder UserControl NumericUpDown
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the UserControl NumericUpDown.
 * Original source for this code with some modifications
 * https://www.philosophicalgeek.com/2009/11/16/a-wpf-numeric-entry-control/
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Helpers;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace Blender_Script_Rendering_Builder.UserControls.Numeric_Entry_Control
{
    /// <summary>
    /// Interaction logic for NumericUpDown.xaml
    /// </summary>
    public partial class NumericEntryControl : UserControl
    {
        #region Variables
        private int _previousValue = 0;
        private bool _isIncrementing = false;

        /// <summary>
        /// Object to perform logic for the NumericUpDown UserControl.
        /// </summary>
        NumericEntryControlLogic logic;
        #endregion

        #region Dispatch timers
        private DispatcherTimer _timer = new DispatcherTimer();
        private static int _delayRate = System.Windows.SystemParameters.KeyboardDelay;  // Grabs the keyboard delay rate from the OS
        private static int _repeatSpeed = Math.Max(1, System.Windows.SystemParameters.KeyboardSpeed);  // Grabs the keyboard's repeat rate from the OS
        #endregion

        #region .Net property wrappers
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value",
            typeof(Int32), typeof(NumericEntryControl),
            new PropertyMetadata(0));

        /*
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MaxValue",
            typeof(Int32), typeof(NumericUpDown),
            new PropertyMetadata(100));
        

        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue",
            typeof(Int32), typeof(NumericUpDown),
            new PropertyMetadata(0));
        */

        public static readonly DependencyProperty IncrementProperty =
            DependencyProperty.Register("Increment",
            typeof(Int32), typeof(NumericEntryControl),
            new PropertyMetadata(1));

        public static readonly DependencyProperty LargeIncrementProperty =
            DependencyProperty.Register("LargeIncrement",
            typeof(Int32), typeof(NumericEntryControl),
            new PropertyMetadata(5));

        #region Getter and Setters for the above properties
        public Int32 Value
        {
            get { return (Int32)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        /*
        public Int32 MaxValue
        {
            get { return (Int32)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public Int32 MinValue
        {
            get { return (Int32)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }
        */

        public Int32 Increment
        {
            get { return (Int32)GetValue(IncrementProperty); }
            set { SetValue(IncrementProperty, value); }
        }

        public Int32 LargeIncrement
        {
            get { return (Int32)GetValue(LargeIncrementProperty); }
            set { SetValue(LargeIncrementProperty, value); }
        }
        #endregion
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public NumericEntryControl()
        {
            try
            {
                InitializeComponent();

                // Add a variety of event listener to the TextBox
                _textbox.PreviewTextInput += new TextCompositionEventHandler(_textbox_PreviewTextInput);
                _textbox.PreviewKeyDown += new KeyEventHandler(_textbox_PreviewKeyDown);
                _textbox.GotFocus += new RoutedEventHandler(_textbox_GotFocus);
                _textbox.LostFocus += new RoutedEventHandler(_textbox_LostFocus);

                // Add a variety of event listers to the buttons to increment/decrement the value stored in the text box
                btnIncrement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(btnIncrement_PreviewMouseLeftButtonDown);
                btnIncrement.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(btnIncrement_PreviewMouseLeftButtonUp);

                btnDecrement.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(btnDecrement_PreviewMouseLeftButtonDown);
                btnDecrement.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(btnDecrement_PreviewMouseLeftButtonUp);

                Value = 0;

                // Add an event lister to the DispatcherTimer for every tick it makes
                _timer.Tick += new EventHandler(_timer_Tick);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event listeners
        /// <summary>
        /// An event listener that will make sure the keyboard key pressed is a number, this will not however prevent the user from copying and pasting it
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Text Composition Event</param>
        void _textbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                if (!IsNumericInput(e.Text))
                {
                    e.Handled = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This will listen for the key presses that the user enters and will either Increment or Decrement the value
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Key Event</param>
        void _textbox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Up:
                        IncrementValue();
                        break;
                    case Key.Down:
                        DecrementValue();
                        break;
                    case Key.PageUp:
                        //Value = Math.Min(Value + LargeIncrement, MaxValue);
                        Value += LargeIncrement;
                        break;
                    case Key.PageDown:
                        //Value = Math.Max(Value - LargeIncrement, MinValue);
                        Value -= LargeIncrement;
                        break;
                    default:
                        //do nothing
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        #region GotFocus/LostFocus event listeners
        /// <summary>
        /// An event listener to listen for when the element comes into focus, it will then save the value currently stored inside it.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        void _textbox_GotFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                _previousValue = Value;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// An event listener to listen for when the element losses focus, it will then validate the input to make sure it is in the range specified by MaxValue and MinValue and if it is outside that range it will set it to the appropriate value.  However, it is is not a number, it will restore it to it's previous content.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        void _textbox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                int newValue = 0;
                if (Int32.TryParse(_textbox.Text, out newValue))
                {
                    /*
                    if (newValue > MaxValue)
                    {
                        newValue = MaxValue;
                    }
                    else if (newValue < MinValue)
                    {
                        newValue = MinValue;
                    }
                    */
                }
                else
                {
                    newValue = _previousValue;
                }
                _textbox.Text = newValue.ToString();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event listeners that allows you to press and hold the buttons to increment the value
        /// <summary>
        /// Listens for when you press the left mouse button on the increment button
        /// </summary>
        /// <param name="sender">The sender of the event<</param>
        /// <param name="e">The event's information, I.E. a Mouse Button Event</param>
        void btnIncrement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                btnIncrement.CaptureMouse();
                _timer.Interval = TimeSpan.FromMilliseconds(_delayRate * 250);
                _timer.Start();

                _isIncrementing = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listens for when you release the left mouse button on the increment button
        /// </summary>
        /// <param name="sender">The sender of the event<</param>
        /// <param name="e">The event's information, I.E. a Mouse Button Event</param>
        void btnIncrement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _timer.Stop();
                btnIncrement.ReleaseMouseCapture();
                IncrementValue();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listens for when you press the left mouse button on the decrement button
        /// </summary>
        /// <param name="sender">The sender of the event<</param>
        /// <param name="e">The event's information, I.E. a Mouse Button Event</param>
        void btnDecrement_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                btnDecrement.CaptureMouse();
                _timer.Interval = TimeSpan.FromMilliseconds(_delayRate * 250);
                _timer.Start();

                _isIncrementing = false;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listens for when you release the left mouse button on the decrement button
        /// </summary>
        /// <param name="sender">The sender of the event<</param>
        /// <param name="e">The event's information, I.E. a Mouse Button Event</param>
        void btnDecrement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                _timer.Stop();
                btnDecrement.ReleaseMouseCapture();
                DecrementValue();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Listens for when the timer's required amount of time has passed before seeing if it has to increment/decrement the value
        /// </summary>
        /// <param name="sender">The sender of the event<</param>
        /// <param name="e">The event's information</param>
        void _timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (_isIncrementing)
                {
                    IncrementValue();
                }
                else
                {
                    DecrementValue();
                }

                _timer.Interval = TimeSpan.FromMilliseconds(1000.0 / _repeatSpeed);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
        #endregion

        #region Functions
        /// <summary>
        /// Makes sure that the text supplied contains only numeric characters
        /// </summary>
        /// <param name="text">The text we are making sure only contains numbers</param>
        /// <returns>True if all the characters are digits, other wise false</returns>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private bool IsNumericInput(string text)
        {
            try
            {
                // If the text supplied contains only digits, return true otherwise false
                Match regexResults = Regex.Match(text, "^\\d+$");

                // The input is valid
                if (regexResults.Success)
                {
                    return true;
                }
                // The input is invalid
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #region Increment/Decrement the value stored inside the variable "Value"
        /// <summary>
        /// Increments the value stored by what is stored inside the "Increment" variable up to the value stored inside "MaxValue"
        /// </summary>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private void IncrementValue()
        {
            try
            {
                //Value = Math.Min(Value + Increment, MaxValue);
                Value += Increment;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Decrements the value stored by what is stored inside the "Increment" variable up to the value stored inside "MinValue"
        /// </summary>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private void DecrementValue()
        {
            try
            {
                //Value = Math.Max(Value - Increment, MinValue);
                Value -= Increment;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
        #endregion
    }
}
