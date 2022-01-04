using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BOAT
{
    public partial class MainPage : ContentPage
    {
        public SQLC SQL = new SQLC();
        public String[] strq = new string[3];
        public MainPage() { InitializeComponent(); Cone(); LB1F(); }
        public void Cone()
        {
            if (SQL.Con() == true) { LB2.Text = "ON LINE"; LB2.TextColor = Color.Blue; }
            else { LB2.Text = "OFF LINE"; LB2.TextColor = Color.Red; }
        }
        public void t1s(string S)
        {
            SQL.SQLcmd = new System.Data.SqlClient.SqlCommand();SQL.SQLcmd.Connection = SQL.SQLconn;
            try
            {
                for(int k = 0; k < 3; k++) { strq[k] = ""; }
                SQL.SQLconn.Open(); SQL.SQLcmd.CommandText = S; SQL.SQLrd = SQL.SQLcmd.ExecuteReader();
                if (SQL.SQLrd.Read()) { for (int K = 0; K <= (SQL.SQLrd.FieldCount - 1); K++) { strq[K] = Convert.ToString(SQL.SQLrd[K]); } }
                SQL.SQLconn.Close();
            }
            catch (Exception a) { SQL.SQLconn.Close(); DisplayAlert("NT", "ERROR#1\n" + a, "sv"); }
        }
        void LB1F() { LB1.GestureRecognizers.Add(new TapGestureRecognizer() { Command = new Command(() => { DisplayAlert("xd", "XD", "OK"); }) }); }
        private void BT1_Clicked(object sender, EventArgs e)
        {
            t1s("SELECT Pass FROM Users WHERE Usuario = '" + ETY1.Text + "'");
            if (strq[0] == ETY2.Text) { DisplayAlert("Bienvenido", ETY1.Text, "OK"); Application.Current.MainPage.Navigation.PushAsync(new PAD()); }
            else { DisplayAlert("Alerta", "Usuario no reconocido", "OK"); }
        }
    }
}
