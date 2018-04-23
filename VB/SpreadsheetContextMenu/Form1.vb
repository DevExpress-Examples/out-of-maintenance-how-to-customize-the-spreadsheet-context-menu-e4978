Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Windows.Forms
#Region "#usings"
Imports DevExpress.XtraSpreadsheet
Imports DevExpress.XtraSpreadsheet.Commands
Imports DevExpress.XtraSpreadsheet.Services
Imports DevExpress.XtraSpreadsheet.Menu
#End Region ' #usings

Namespace SpreadsheetContextMenu
	Partial Public Class Form1
		Inherits DevExpress.XtraBars.Ribbon.RibbonForm
		Public Sub New()
			InitializeComponent()
		End Sub

		#Region "#popupmenushowing"
		Private Sub spreadsheetControl1_PopupMenuShowing(ByVal sender As Object, ByVal e As PopupMenuShowingEventArgs) Handles spreadsheetControl1.PopupMenuShowing
			If e.MenuType = SpreadsheetMenuType.Cell Then
				' Remove the "Clear Contents" menu item.
				e.Menu.RemoveMenuItem(SpreadsheetCommandId.FormatClearContentsContextMenuItem)

				' Disable the "Hyperlink" menu item.
				e.Menu.DisableMenuItem(SpreadsheetCommandId.InsertHyperlinkContextMenuItem)

				' Create a menu item for the Spreadsheet command, which inserts a picture into a worksheet.
				Dim service As ISpreadsheetCommandFactoryService = CType(spreadsheetControl1.GetService(GetType(ISpreadsheetCommandFactoryService)), ISpreadsheetCommandFactoryService)
				Dim cmd As SpreadsheetCommand = service.CreateCommand(SpreadsheetCommandId.InsertPicture)
				Dim menuItemCommandAdapter As New SpreadsheetMenuItemCommandWinAdapter(cmd)
                Dim menuItem As SpreadsheetMenuItem = CType(menuItemCommandAdapter.CreateMenuItem(DevExpress.Utils.Menu.DXMenuItemPriority.High), SpreadsheetMenuItem)
				menuItem.BeginGroup = True
				e.Menu.Items.Add(menuItem)

				' Insert a new item into the Spreadsheet popup menu and handle its click event.
				Dim myItem As New SpreadsheetMenuItem("My Menu Item", New EventHandler(AddressOf MyClickHandler))
				e.Menu.Items.Add(myItem)
			End If
		End Sub

		Public Sub MyClickHandler(ByVal sender As Object, ByVal e As EventArgs)
			MessageBox.Show("My Menu Item Clicked!")
		End Sub
		#End Region ' #popupmenushowing

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
		  spreadsheetControl1.Document.Worksheets(0).Cells("B1").Value = "Right-click any cell to display the custom context menu"
		End Sub
	End Class


End Namespace
