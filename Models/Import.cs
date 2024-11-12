using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;

namespace QLNhaThuoc.Models
{
    public class Import
    {
        private string _id;
        private string _employeeID;
        private string _supplierID;
        private DateTime _date;

        private List<ImportDetails> _details = new List<ImportDetails> ();

        public string ID => _id;
        public string EmployeeID => _employeeID;
        public string SupplierID => _supplierID;
        public DateTime Date => _date;
        public List<ImportDetails> Details => _details;


        public Import()
        {
            
        }

        public void SetByID (string id)
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinPhieuNhap", 
                ("@in_MaPN", id));
            if (data.Rows.Count <= 0)
            {
                throw new Exception($"Not found Import data by ID = {id}");
            }

            object[] items = data.Rows[0].ItemArray;

            _id = items[0].ToString();
            _employeeID= items[1].ToString();
            _supplierID = items[2].ToString();
            _date = items[3] == DBNull.Value ? new DateTime(2000, 1, 1) : (DateTime)items[3];

            this.SetDetails();
        }

        private void SetDetails()
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinChiTietPhieuNhap",
                ("@in_MaPN", ID));

            _details.Clear ();
            for(int i = 0; i < data.Rows.Count; i++)
            {
                object[] items = data.Rows[i].ItemArray;

                ImportDetails detail = new ImportDetails();
                detail.medicineID = items[0].ToString();
                detail.quantity = Convert.ToInt32(items[1]);
                detail.price = Convert.ToInt32(items[2]);

                _details.Add(detail);
            }
        }

        public string DetailsToJson()
        {
            return JsonConvert.SerializeObject(Details);
        }
    }

    public class ImportDetails
    {
        public string medicineID;
        public int quantity;
        public decimal price;
    }
}
