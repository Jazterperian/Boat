using System.Data.SqlClient;
namespace BOAT
{
    public class SQLC
    {
        /*-Data Source=192.168.100.11;Initial Catalog=PBlivin;User ID=Jazterperian;Password=Smulca;
         Server=sql10.freesqldatabase.com;Port=3306;Database=sql10459242;UserID=sql10459242;Password=NRkcltGVNN*/
        public SqlConnection SQLconn = new SqlConnection("Data Source=34.151.204.116;Initial Catalog=PBlivin;User ID=Jazterperian;Password=Smulca");
        public SqlCommand SQLcmd; public SqlDataReader SQLrd;
        public bool Con() { try { SQLconn.Open(); SQLconn.Close(); return true; } catch { return false; } }
    }
}
