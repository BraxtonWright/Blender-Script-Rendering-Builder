/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder window wndBrowseBlenderExecutable
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Helpers;
using System;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace Blender_Script_Rendering_Builder.Windows.Browse_Blender_Executable
{
    /// <summary>
    /// Interaction logic for wndBrowseBlenderExecutible.xaml
    /// </summary>
    public partial class wndBrowseBlenderExecutable : Window
    {
        #region Variables
        /// <summary>
        /// Used to temporarily save the path of the blender application file
        /// </summary>
        private string _blenderApplicationPath;

        /// <summary>
        /// Has the save button been pressed
        /// </summary>
        private bool _saved = false;

        /// <summary>
        /// A public getter for the private variable
        /// </summary>
        public bool Saved { get { return _saved; } }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor that accepts the title of the window and the window's message
        /// </summary>
        /// <param name="windowTitle">The title of the window</param>
        /// <param name="windowMessage">The message to be displayed on the window</param>
        public wndBrowseBlenderExecutable(string windowTitle, string windowMessage)
        {
            try
            {
                InitializeComponent();

                window.Title = windowTitle;
                tbMessage.Text = windowMessage;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// This event listener will listen for when you press the button to browse for a blender application's executable.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Executable files (*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == true)
                {
                    _blenderApplicationPath = openFileDialog.FileName;
                    btnSave.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This event listener will listen for when you press the button to save the changes and then close the window.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BlenderApplicationPath = _blenderApplicationPath;
            Properties.Settings.Default.Save();
            _saved = true;
            this.Close();
        }

        /// <summary>
        /// This event listener listens for when the window is closing and determines if it should show a message box asking if the changes should be saved
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // If the save button is enabled and the user has not press it
            if (btnSave.IsEnabled && !_saved)
            {
                string message = "You have not saved your changes.\nClose without saving changes?";
                MessageBoxResult result = MessageBox.Show(message, "Changed not saved", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
