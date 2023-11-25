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
using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Windows.Browse_Blender_Executible;
using System.Collections.Generic;
using System.Windows.Controls;

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

        string currentTheme;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            try
            {
                // We change the order of this because inside the event listener "wndMain_Initialized" we use the logic variable and we populate the themes menu item with a list of themes and select a default theme (which also be done inside a "Loaded" event listener, but it would result in window wndBrowseBlenderExecutible in not having a theme)
                logic = new clsMainLogic();

                InitializeComponent();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Event Listeners
        #region Menu item event listeners
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
        /// Temporary event listener to be removed for final production of the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Properties.Settings.Default.BlenderApplicationPath = "";
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        #endregion

        #region Main UI event listeners
        /// <summary>
        /// This event listener is fired just after the function "InitializeComponent" finishes executing.  This event listener will then populate the "themes" MenuItem, select the default theme selected from the settings file, and will determine if the blender application file has been not defined or changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wndMain_Initialized(object sender, EventArgs e)
        {
            try
            {
                this.currentTheme = Properties.Settings.Default.currentTheme;  // Gets the current theme saved, and because the theme manager is bound to this variable, it applies the theme the the application

                // Populate the MenuItem by adding sub-MenuItems to contain a list of available themes for the application
                foreach (Theme theme in ThemeManager.GetThemes())
                {
                    MenuItem item = new MenuItem();
                    item.Header = theme.Name;
                    item.IsCheckable = true;
                    item.IsChecked = theme.Name == this.currentTheme;
                    item.Checked += miTheme_Checked;  // Add a Checked event listener to the MenuItem

                    if (theme.Name == this.currentTheme)
                    {
                        theme.IsChecked = true;
                        // Application Level
                        Application.Current.ApplyTheme(theme.Name);
                    }

                    miThemes.Items.Add(item);
                }

                // Determine if the user has to define the blender application location
                BrowseBlenderExe browseBlenderExe = logic.ShouldOpenBlendApplictionWindow();
                if (browseBlenderExe.needToOpenWindow)
                {
                    wndBrowseBlenderExecutible wndBrowseBlenderExecutible = new wndBrowseBlenderExecutible(browseBlenderExe.windowTitle, browseBlenderExe.windowMessage);

                    this.Hide();  //hide this window from the user
                    wndBrowseBlenderExecutible.ShowDialog();  //open this new window and pause here in the code until the window is closed

                    // If the user has not defined the application path, close/terminate this application
                    if (!wndBrowseBlenderExecutible.Saved)
                    {
                        this.Close();
                    }
                    else
                    {
                        this.Show();  //shows this window to the user
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// This event listner listens for when the selection changes to change the theme of the application
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void miTheme_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                MenuItem menuItem = sender as MenuItem;

                string theme = menuItem.Header.ToString();

                // Window Level
                // this.ApplyTheme(theme);

                // Application Level
                Application.Current.ApplyTheme(theme);

                // Save the theme selected so it persists after you close the application
                Properties.Settings.Default.currentTheme = theme;
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

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
        /// Shows the shutdown dockpanel containing the control for how long to wait until the PC shutsdown
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void checkShutdownPC_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                dpShutdown.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Hides the shutdown dockpanel containing the control for how long to wait until the PC shutsdown
        /// </summary>
        /// <param name="sender">The sender of the event</param>
        /// <param name="e">The event's information, I.E. a Routed Event</param>
        private void checkShutdownPC_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                dpShutdown.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
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
                List<BlenderData> renderingInfo = new List<BlenderData>();

                foreach (BlenderSelection blenderUserControl in spBlenderFiles.Children)
                {
                    renderingInfo.Add(blenderUserControl.GetRenderingInfo());
                }

                logic.GenerateScriptFileIfValid(renderingInfo, (bool)checkShutdownPC.IsChecked, necShutdownTime.Value);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                              MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #endregion
    }
}
