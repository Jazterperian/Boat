using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.DataGrid;
namespace BOAT
{
    internal class PAD : TabbedPage
    {
        public SQLC SQL = new SQLC(); public Grid[] GR = new Grid[2]; public EntryCell[] ENT = new EntryCell[3]; public ImageButton[] IBT = new ImageButton[3];
        public DataGrid[] DTG= new DataGrid[2]; public DataSet[] DTS = new DataSet[2]; public System.Data.SqlClient.SqlDataAdapter[] VC = new System.Data.SqlClient.SqlDataAdapter[2];
        public string[] SC = {"LP.png"}; public String[] str = new string[10]; public TextCell[] TC; public bool[] ES = new bool[2]; public ContentPage[] CP = new ContentPage[2];
        public void t2s(string S)
        {
            try
            {
                SQL.SQLcmd = new System.Data.SqlClient.SqlCommand(); SQL.SQLcmd.Connection = SQL.SQLconn;SQL.SQLcmd.CommandText = S;
                VC[0] = new System.Data.SqlClient.SqlDataAdapter(SQL.SQLcmd.CommandText, SQL.SQLconn); SQL.SQLconn.Close();
            }
            catch (Exception a) { SQL.SQLconn.Close(); DisplayAlert("NT", "ERROR#1\n" + a, "sv"); }
        }
        public void prdt(string S)
        {
            t2s(S); DTS[0] = new DataSet(); DTS[0].Tables.Add("T1"); VC[0].Fill(DTS[0], "T1"); TC = new TextCell[DTS[0].Tables[0].Rows.Count]; ES[0] = true;
            for (int K = 0; K < DTS[0].Tables[0].Rows.Count; K++) { TC[K] = new TextCell { Text = DTS[0].Tables[0].Rows[K].Field<String>(0), TextColor=Color.Black, Detail = "P1: " + decimal.Round(DTS[0].Tables[0].Rows[K].Field<Decimal>(1), 2) + ", P2: " + decimal.Round(DTS[0].Tables[0].Rows[K].Field<Decimal>(2), 2) + ", P3: " + decimal.Round(DTS[0].Tables[0].Rows[K].Field<Decimal>(3), 2) + ", PV: " + decimal.Round(DTS[0].Tables[0].Rows[K].Field<Decimal>(4), 2) + ", " + DTS[0].Tables[0].Rows[K].Field<String>(5), DetailColor=Color.Black }; }
        }
        public void CNTA()
        {
            ENT[0] = new EntryCell { Placeholder = "Producto" }; IBT[0] = new ImageButton { WidthRequest = 60, BackgroundColor = Color.Transparent, Source = SC[0] };
            prdt("SELECT detalle, cprvd1, cprvd2, cprvd3, pvcu, pr2 FROM Prdts WHERE cpr2 > 0 ORDER BY detalle ASC");
        }
        public void DRW()
        {
            GR[0] = new Grid { RowSpacing = 7, Padding = 5, BackgroundColor = Color.Transparent, RowDefinitions = { new RowDefinition { Height = new GridLength(40, GridUnitType.Absolute) }, new RowDefinition { Height = new GridLength(1, GridUnitType.Star) } } };
            GR[0].Children.Add(new StackLayout { Orientation = StackOrientation.Horizontal, Children = { new TableView { Intent = TableIntent.Form, Root = new TableRoot { new TableSection { ENT[0] } } }, IBT[0] } }, 0, 0);
            GR[0].Children.Add(new ScrollView { Orientation = ScrollOrientation.Vertical, Content = new StackLayout { Orientation = StackOrientation.Vertical, Children = { new TableView { Intent = TableIntent.Form, Root = new TableRoot { new TableSection { TC } } } } } }, 0, 1);
            CP[0] = new ContentPage { Title = "Inventario", BackgroundImageSource = "F1.jpg", Content = GR[0] };
            CP[1] = new ContentPage { Title = "CLIENTES", BackgroundImageSource = "F1.jpg", Content = new StackLayout { Children = { new Label { Text = "Coming Soon" } } } };
            this.BarTextColor = Color.Black; this.BarBackgroundColor = Color.FromHex("#094aca"); this.Children.Add(CP[0]); this.Children.Add(CP[1]);
        }
        public void CLP() { this.Children.Clear(); }
        public PAD()
        {
            NavigationPage.SetHasNavigationBar(this, false); this.Title = "TBP1"; CNTA(); DRW();
            IBT[0].Clicked += (sender, e) => { str[0] = "%" + ENT[0].Text + "%"; prdt("SELECT detalle, cprvd1, cprvd2, cprvd3, pvcu, pr2 FROM Prdts WHERE detalle LIKE '"+str[0]+"' ORDER BY detalle ASC"); CLP();DRW(); };
        }
    }
}
