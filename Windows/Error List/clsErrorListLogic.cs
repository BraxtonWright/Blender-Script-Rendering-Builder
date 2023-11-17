using Blender_Script_Rendering_Builder.Classes.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Blender_Script_Rendering_Builder.Windows.Error_List
{
    public class clsErrorListLogic
    {
        #region Variables
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public clsErrorListLogic()
        {
            // Do nothing
        }
        #endregion

        #region Functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="branchInfo">The information about the branch you wish to extract information from</param>
        /// <returns>A TreeViewItem containing all the subBranches and leaves for the tree</returns>
        public TreeViewItem GetBranchInfo(ErrorTreeBranch branchInfo)
        {
            TreeViewItem branch = new TreeViewItem();
            branch.Header = branchInfo.DisplayName;

            // If the branch has any sub-branches, expand this branch so that you see the entire branch
            if (branchInfo.BranchErrors.Count > 0)
            {
                branch.IsExpanded = true;
            }

            foreach(ErrorTreeBranch subBranch in branchInfo.BranchErrors)
            {
                TreeViewItem subBranchInfo = GetBranchInfo(subBranch);  // Use recursion to go though the list until it reaches the root cause of the error
                branch.Items.Add(subBranchInfo);  // Add the subBranch to the main branch
            }

            return branch;
        }
        #endregion
    }
}
