﻿/*
 * Braxton Wright
 * CS 3650
 * Blender Script Rendering Builder abstract class Validators
 * Dr. Nichole Anderson
 * Due: 12/6/2023
 * Version: 1.0
 *  ----------------------------------------------------------------------------------------------------------
 * This file only contains the functions required for validation of the rendering information for the script.
 * -----------------------------------------------------------------------------------------------------------
 */

using Blender_Script_Rendering_Builder.Classes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Blender_Script_Rendering_Builder.Classes.Helpers
{
    abstract class Validators
    {
        #region Blender info validators
        // See Shared section
        #endregion

        #region Scene info validators
        /// <summary>
        /// Determines if the supplied scene name is valid
        /// </summary>
        /// <param name="name">The name of the scene to test</param>
        /// <returns>An object with two properties, a boolean Valid and a string ErrorMessage if errors occurred</returns>
        internal static ValidatorsReturn SceneNameValid(string name)
        {
            // The will check the text to see if it is empty and if it is not, then it will then check it for spaces inside it
            bool isEmptyString = StringEmpty(name);
            bool regexValid = isEmptyString ? false : Regex.Match(name, " ").Success;

            bool valid = !(isEmptyString || regexValid); // We inverse the results because we only want to return true if the both the isEmptyString and RegexValid are false

            ValidatorsReturn returnObject = new ValidatorsReturn(valid);

            if(isEmptyString)
            {
                returnObject.ErrorMessage += "The scene's name is required";
            }
            else if (regexValid)
            {
                returnObject.ErrorMessage += "The scene's name can't contain spaces";
            }

            return returnObject;
        }
        #endregion

        #region Render info validators
        /// <summary>
        /// This function will determine if the supplied text is a valid set of characters for the custom frames input
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <returns>An object with two properties, a boolean Valid and a string ErrorMessage if errors occurred</returns>
        internal static ValidatorsReturn CustomFramesValid(string text)
        {
            bool isEmptyString = StringEmpty(text);
            // If the text matches the following regex pattern:
            // One or more digits at the start of the string
            // Followed by ",'one or more digits'" OR ", 'one or more digits'" OR "-'one or more digits'" zero or more times
            // The string ends
            bool regexValid = isEmptyString ? false : Regex.Match(text, "^\\d+(?:,\\d+|, \\d+|-\\d+)*$").Success;

            bool valid = !(isEmptyString || !regexValid); // We inverse the results because we only want to return true if the both the isEmptyString and RegexValid are false

            ValidatorsReturn returnObject = new ValidatorsReturn(valid);

            if (isEmptyString)
            {
                returnObject.ErrorMessage = "This field is required";
            }
            else if (!regexValid)
            {
                returnObject.ErrorMessage = "The field doesn't match the required pattern";
            }

            return returnObject;
        }
        #endregion

        #region Shared
        /// <summary>
        /// Determine if the text supplied is null or ""
        /// </summary>
        /// <param name="text">The text to be tested</param>
        /// <returns>True if it is null or "", false otherwise</returns>
        internal static bool StringEmpty(string text)
        {
            // This method is used for a number of items, such as the blender file location, scene name, render type, render engine, output file type, output path selection, and some more
            return string.IsNullOrEmpty(text);
        }
        #endregion
    }
}
