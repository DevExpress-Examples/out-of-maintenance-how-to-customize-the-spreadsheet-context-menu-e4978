using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
#region #usings
using DevExpress.XtraSpreadsheet;
using DevExpress.XtraSpreadsheet.Commands;
using DevExpress.XtraSpreadsheet.Services;
using DevExpress.XtraSpreadsheet.Menu;
#endregion #usings

namespace SpreadsheetContextMenu
{
    public partial class Form1 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        #region #popupmenushowing
        private void spreadsheetControl1_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
        {
            if (e.MenuType == SpreadsheetMenuType.Cell)
            {
                // Remove the "Clear Contents" menu item.
                e.Menu.RemoveMenuItem(SpreadsheetCommandId.FormatClearContentsContextMenuItem);

                // Disable the "Hyperlink" menu item.
                e.Menu.DisableMenuItem(SpreadsheetCommandId.InsertHyperlinkContextMenuItem);

                // Create a menu item for the Spreadsheet command, which inserts a picture into a worksheet.
                ISpreadsheetCommandFactoryService service = (ISpreadsheetCommandFactoryService)spreadsheetControl1.GetService(typeof(ISpreadsheetCommandFactoryService));
                SpreadsheetCommand cmd = service.CreateCommand(SpreadsheetCommandId.InsertPicture);
                SpreadsheetMenuItemCommandWinAdapter menuItemCommandAdapter = new SpreadsheetMenuItemCommandWinAdapter(cmd);
                SpreadsheetMenuItem menuItem = (SpreadsheetMenuItem)menuItemCommandAdapter.CreateMenuItem();
                menuItem.BeginGroup = true;
                e.Menu.Items.Add(menuItem);

                // Insert a new item into the Spreadsheet popup menu and handle its click event.
                SpreadsheetMenuItem myItem = new SpreadsheetMenuItem("My Menu Item", new EventHandler(MyClickHandler));
                e.Menu.Items.Add(myItem);
            }
        }

        public void MyClickHandler(object sender, EventArgs e)
        {
            MessageBox.Show("My Menu Item Clicked!");
        }
        #endregion #popupmenushowing

        private void Form1_Load(object sender, EventArgs e)
        {
          spreadsheetControl1.Document.Worksheets[0].Cells["B1"].Value = "Right-click any cell to display the custom context menu";
        }
    }

    
}
