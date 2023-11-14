using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        /// <returns>True if it is valid, false otherwise</returns>
        internal static bool SceneNameValid(string name)
        {
            // The will check the text to see if it is empty and if it is not, then it will then check it for spaces inside it
            bool isEmptyString = StringEmpty(name);
            bool regexValid = isEmptyString ? false : Regex.Match(name, " ").Success;

            bool returnBool = !(isEmptyString || regexValid); // We inverse the results because we only want to return true if the both the isEmptyString and RegexValid are false

            return returnBool;
        }
        #endregion

        #region Render info validators
        /// <summary>
        /// This function will determine if the supplied text is a valid set of characters for the custom frames input
        /// </summary>
        /// <param name="text">The text to check</param>
        /// <returns>True if it is valid, false otherwise</returns>
        internal static bool CustomFramesValid(string text)
        {
            // If the text matches the following regex pattern:
            // One or more digites at the start of the string
            // Followed by ",'one or more digits'" OR ", 'one or more digits'" OR "-'one or more digits'" zero or more times
            // The the string ends
            Match regexResults = Regex.Match(text, "^\\d+(?:,\\d+|, \\d+|-\\d+)*$");

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
