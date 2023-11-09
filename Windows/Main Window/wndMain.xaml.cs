/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder Window wndMain
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 0.5
 *  ----------------------------------------------------------------------------------------------------------
 * This file contains the required event listeners for the main window.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Main;
using Blender_Script_Rendering_Builder.UserControls.Blender_Selection;
using System;
using System.Reflection;
using System.Windows;
using System.Configuration;
using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Collections;
using System.IO;
using Blender_Script_Rendering_Builder.Windows.Browse_Blender_Executible;
using System.Collections.Generic;

namespace Blender_Script_Rendering_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for this window.
        /// </summary>
        clsMainLogic logic;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                logic = new clsMainLogic();

                BrowseBlenderExe browseBlenderExe = logic.ShouldOpenBlendApplictionWindow();
                if (browseBlenderExe.needToOpenWindow)
                {
                    wndBrowseBlenderExecutible wndBrowseBlenderExecutible = new wndBrowseBlenderExecutible(browseBlenderExe.windowTitle, browseBlenderExe.windowMessage);

                    wndBrowseBlenderExecutible.ShowDialog();  //open this new window and pause here in the code until the window is closed
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        #region Menu event listeners
        private void miLightMode_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                AppTheme.ChangeTheme(new Uri("Themes/Light.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void miDarkMode_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                AppTheme.ChangeTheme(new Uri("Themes/Dark.xaml", UriKind.Relative));
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// This event listener will listen for when you press the button to add a new blender file to be processed.
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnAddNewBlenderFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spBlenderFiles.Children.Add(new BlenderSelection());
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                 MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener will listen for then you press the button to create the script file from the information you supplied
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void btnCreateScriptFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                logic.GenerateScriptFile(spBlenderFiles);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                              MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listener listens for when you press the menu item to change the Blender executible file location
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void miChangeBlendExeLocation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string windowMessage = "Here you can redefine where the Blender executiable file is located if you installed a new version of Blender or you mis-configured the setting.  If you wish to change it, click the browse button.";
                wndBrowseBlenderExecutible browseBlenderExecutible = new wndBrowseBlenderExecutible("Change executible location", windowMessage);
                browseBlenderExecutible.Owner = this;  //this sets it so that the search window owner is this window (so it loads where this window is currently)

                browseBlenderExecutible.ShowDialog();  //open this new window and pause here in the code until the window is closed
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                              MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void checkShutdownPC_Checked(object sender, RoutedEventArgs e)
        {
            dpShutdown.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void checkShutdownPC_Unchecked(object sender, RoutedEventArgs e)
        {
            dpShutdown.Visibility = Visibility.Collapsed;
        }
        #endregion
    }
}
