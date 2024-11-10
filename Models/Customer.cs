using System;
using System.Data;

namespace QLNhaThuoc.Models
{
    public class Customer
    {
        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _address = string.Empty;
        private DateTime _birthday = new DateTime(2000, 1, 1);
        private bool _isEmployee = false;

        public string ID => _id;
        public string Name => _name;
        public string PhoneNumber => _phoneNumber;
        public string Address => _address;
        public DateTime Birthday => _birthday;
        public bool IsEmployee => _isEmployee;

        public Customer()
        {
        }

        public Customer(string id)
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinKhachHang", ("@in_MaUser", id));
            if (data.Rows.Count <= 0)
            {
                throw new Exception($"Not found Customer data by ID = {id}");
            }

            object[] items = data.Rows[0].ItemArray;

            _id = items[0].ToString();
            _name = items[1].ToString();
            _phoneNumber = items[2].ToString();
            _address = items[3].ToString();
            _birthday = items[4] == DBNull.Value ? new DateTime(2000, 1, 1) : (DateTime)items[4];
            _isEmployee = Convert.ToBoolean(items[5]);
        }
    }

}
