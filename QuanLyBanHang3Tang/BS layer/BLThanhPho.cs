using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace QuanLyBanHang3Tang.BS_layer
{
    class BLThanhPho
    {
        public System.Data.Linq.Table<ThanhPho> LayThanhPho()
        {
            DataSet ds = new DataSet();
            QLKHDataContext qlBH = new QLKHDataContext();
            return qlBH.ThanhPhos;
        }
        public bool ThemThanhPho(string MaThanhPho, string TenThanhPho, ref string err)
        {
            QLKHDataContext qlKH = new QLKHDataContext();
            ThanhPho tp = new ThanhPho();
            tp.ThanhPho1 = MaThanhPho;
            tp.TenThanhPho = TenThanhPho;

            qlKH.ThanhPhos.InsertOnSubmit(tp);
            qlKH.ThanhPhos.Context.SubmitChanges();
            return true;
        }
        public bool XoaThanhPho(ref string err, string MaThanhPho)
        {
            QLKHDataContext qlBH = new QLKHDataContext();

            var tpQuery = from tp in qlBH.ThanhPhos
                          where tp.ThanhPho1 == MaThanhPho
                          select tp;
            qlBH.ThanhPhos.DeleteAllOnSubmit(tpQuery);
            qlBH.ThanhPhos.Context.SubmitChanges();
            return true;
        }
        public bool CapNhatThanhPho(string MaThanhPho, string TenThanhPho, ref string err)
        {
            QLKHDataContext qlBH = new QLKHDataContext();

            var tpQuery = (from tp in qlBH.ThanhPhos
                           where tp.ThanhPho1 == MaThanhPho
                           select tp).SingleOrDefault();
            if (tpQuery != null)
            {
                tpQuery.TenThanhPho = TenThanhPho;
                qlBH.SubmitChanges();
            }
            return true;
        }
    }
}
