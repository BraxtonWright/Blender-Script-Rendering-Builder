/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder Window MainWindow
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Helpers;
using System;
using System.Reflection;
using System.Windows;
using Microsoft.Win32;

namespace Blender_Script_Rendering_Builder.Windows.Browse_Blender_Executible
{
    /// <summary>
    /// Interaction logic for wndBrowseBlenderExecutible.xaml
    /// </summary>
    public partial class wndBrowseBlenderExecutible : Window
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for this window.
        /// </summary>
        clsBrowseBlenderExecutibleLogic logic;

        string blenderApplicationPath;
        #endregion


        #region Constructor
        /// <summary>
        /// Default constructor that accepts the title of the window and the window's message
        /// </summary>
        /// <param name="windowTitle">The title of the window</param>
        /// <param name="windowMessage">The message to be displayed on the window</param>
        public wndBrowseBlenderExecutible(string windowTitle, string windowMessage)
        {
            try
            {
                InitializeComponent();

                window.Title = windowTitle;
                tbMessage.Text = windowMessage;

                logic = new clsBrowseBlenderExecutibleLogic();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// This event listener will listen for when you press the button to browse for a blender application's executible.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Executible files (*.exe)|*.exe";
                if (openFileDialog.ShowDialog() == true)
                {
                    blenderApplicationPath = openFileDialog.FileName;
                    btnSave.IsEnabled = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }

        }

        /// <summary>
        /// This event listener will listen for when you press the button to save the changes to the .
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.BlenderApplicationPath = blenderApplicationPath;
            Properties.Settings.Default.Save();
            this.Close();
        }
    }
}
