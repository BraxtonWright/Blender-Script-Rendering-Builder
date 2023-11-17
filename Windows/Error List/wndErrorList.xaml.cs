using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Blender_Script_Rendering_Builder.Windows.Error_List
{
    /// <summary>
    /// Interaction logic for ErrorList.xaml
    /// </summary>
    public partial class wndErrorList : Window
    {
        #region Variables
        /// <summary>
        /// Object to perform logic for this window.
        /// </summary>
        private clsErrorListLogic logic;

        private List<ErrorTreeBranch> errors;
        #endregion

        #region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public wndErrorList(List<ErrorTreeBranch> errors)
        {
            try
            {
                InitializeComponent();

                logic = new clsErrorListLogic();
                PopulateTree(errors);
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                            MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion

        #region Functions
        /// <summary>
        /// Populates the error tree with the required 
        /// </summary>
        /// <param name="errors">A list of instances of ErrorTreeBranch containing the information about the tree</param>
        public void PopulateTree(List<ErrorTreeBranch> errors)
        {
            // WIP https://stackoverflow.com/questions/18842620/how-to-fill-treeview-in-wpf-dynamically or currenlty implmenting https://stackoverflow.com/a/36664659
            foreach (ErrorTreeBranch branch in errors)
            {
                TreeViewItem branchInfo = logic.GetBranchInfo(branch);
                tvErrorTree.Items.Add(branchInfo);
            }
        }
        #endregion
    }
}
