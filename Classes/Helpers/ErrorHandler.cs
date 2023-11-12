/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder abstract class ErrorHandler
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file only contains the function to handle errors that are passed to it by displaying the error to the user.
 * If this fails, it outputs it to a file.
 * -----------------------------------------------------------------------------------------------------------
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    /// <summary>
    /// We make this an abstract class so that we don't need to make an instance of the class to call the below function
    /// </summary>
    abstract class ErrorHandler
    {
        /// <summary>
        /// Handle the error passed to it.
        /// </summary>
        /// <param name="ErrorMessage">The error message that was generated</param>
        internal static void HandleError(string ErrorMessage)
        {
            try
            {
                // Would write to a file or database here.
                // Show a message box for the path the error took
                MessageBox.Show(ErrorMessage);
                // Show only the thing that caused the error and not show the path that was taken to make that error
                // MessageBox.Show(ErrorMessage.Substring(ErrorMessage.LastIndexOf("-> ") + 3));
            }
            catch (Exception ex)
            {
                // The base directory for this project
                string SavePath = AppDomain.CurrentDomain.BaseDirectory + "Error.txt";

                // Make/append the exception to the file Error.txt
                System.IO.File.AppendAllText(SavePath, Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
