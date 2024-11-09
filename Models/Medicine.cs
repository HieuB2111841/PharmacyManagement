using System;
using System.Data;

namespace QLNhaThuoc.Models
{
    public class Medicine
    {
        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _type = string.Empty;
        private string _supplierID = string.Empty;
        private string _manufacturerID = string.Empty;
        private string _effect = string.Empty;
        private int _storedQuantity = 0;

        public string ID => _id;
        public string Name => _name;
        public string Type => _type;
        public string SupplierID => _supplierID;
        public string ManufacturerID => _manufacturerID;
        public string Effect => _effect;
        public int StoredQuantity => _storedQuantity;

        public Medicine() 
        {
            
        }

        public Medicine(string id) 
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinThuoc", ("@in_MaThuoc", id));
            if (data.Rows.Count <= 0)
            {
                throw new Exception($"Not found Medicine data by id = {id}");
            }
            object[] items = data.Rows[0].ItemArray;

            _id = items[0].ToString();
            _name = items[1].ToString();
            _type = items[2].ToString();
            _manufacturerID = items[3].ToString();
            _supplierID = items[4].ToString();
            _effect = items[5].ToString();
            _storedQuantity = (int)items[6];
        }
    }
}
