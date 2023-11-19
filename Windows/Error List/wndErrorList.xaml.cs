using Blender_Script_Rendering_Builder.Classes.Helpers;
using Blender_Script_Rendering_Builder.Classes.Modules;
using Blender_Script_Rendering_Builder.Main;
using Blender_Script_Rendering_Builder.UserControls.Bulleted_Item;
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
        /// Populates the error tree with the required information for the user to know what to do
        /// </summary>
        /// <param name="errorTree">A list of instances of ErrorTreeBranch containing the information about the tree</param>
        public void PopulateTree(List<ErrorTreeBranch> errorTree)
        {
            try
            {
                // This will go through each branch in the list of ErrorTreeBranch and will populate the tree
                foreach (ErrorTreeBranch blendBranch in errorTree)
                {
                    AddBulletPoint(blendBranch.DisplayName);
                    foreach (ErrorTreeBranch sceneBranch in blendBranch.BranchErrors)
                    {
                        AddBulletPoint(sceneBranch.DisplayName, 1);

                        foreach (ErrorTreeBranch dataBranch in sceneBranch.BranchErrors)
                        {
                            AddBulletPoint(dataBranch.DisplayName, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Adds a bullet point to the stackpanel named spErrors
        /// </summary>
        /// <param name="bulletText">The text to be displayed on the bullet point</param>
        /// <param name="tabOffset">An optional tab offset for the bullet point</param>
        /// <exception cref="Exception">Catches any exceptions that this method might come across</exception>
        private void AddBulletPoint(string bulletText, int tabOffset = 0)
        {
            try
            {
                BulletedItem bulletedItem = logic.GenerateBulletPoint(bulletText, tabOffset);

                spErrors.Children.Add(bulletedItem);
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." + MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
        #endregion
    }
}
