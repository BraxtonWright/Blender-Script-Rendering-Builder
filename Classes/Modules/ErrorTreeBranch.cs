using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Windows.Error_List;
using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Blender_Script_Rendering_Builder.Classes.Modules
{
    public class ErrorTreeBranch
    {
        #region Variables
        /// <summary>
        /// The name to be displayed for the branch, this is also used as the leafs of the branch
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// A list of errors that are associoated with the branch
        /// </summary>
        public List<ErrorTreeBranch> BranchErrors { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor that will initialize the variable BranchErrors
        /// </summary>
        private ErrorTreeBranch()
        {
            BranchErrors = new List<ErrorTreeBranch>();  // Initalize the BranchErrors list so we can add to it.
        }

        /// <summary>
        /// Default constructor that accepts the name of the branch/leaf
        /// </summary>
        /// <param name="displayName">The name of the branch/leaf</param>
        public ErrorTreeBranch(string displayName) : this()  // This constructor inherits from the above constructor so we don't need include the same code inside this constructor
        {
            DisplayName = displayName;
        }
        #endregion
    }
}
