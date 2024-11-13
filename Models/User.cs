using System;
using System.Data;

namespace QLNhaThuoc.Models
{
    public class User
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

        public User() { }

        public User(string id) => this.SetByID(id);
        

        public void SetByID(string id)
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinNguoiDung", ("@in_MaUser", id));
            if (data.Rows.Count <= 0)
            {
                throw new Exception($"Not found User data by ID = {id}");
            }

            object[] items = data.Rows[0].ItemArray;

            _id = items[0].ToString();
            _name = items[1].ToString();
            _phoneNumber = items[2].ToString();
            _address = items[3].ToString();
            _birthday = items[4] == DBNull.Value ? new DateTime(2000, 1, 1) : (DateTime)items[4];
            _isEmployee = Convert.ToBoolean(items[5]);
        }

        public void SetByPhoneNumber(string phoneNumber)
        {
            DataTable data = MyPublics.Instance.CallProcedure("usp_layThongTinNguoiDungBangSDT", ("@in_SDT", phoneNumber));
            if (data.Rows.Count <= 0)
            {
                throw new Exception($"Not found User data by phone number = {phoneNumber}");
            }

            object[] items = data.Rows[0].ItemArray;

            _id = items[0].ToString();
            _name = items[1].ToString();
            _phoneNumber = items[2].ToString();
            _address = items[3].ToString();
            _birthday = items[4] == DBNull.Value ? new DateTime(2000, 1, 1) : (DateTime)items[4];
            _isEmployee = Convert.ToBoolean(items[5]);
        }

        public void Reset()
        {
            _id = string.Empty;
            _name = string.Empty;
            _phoneNumber = string.Empty;
            _address = string.Empty;
            _birthday = new DateTime(2000, 1, 1);
            _isEmployee = false;
        }

        public override string ToString()
        {
            string role = IsEmployee ? "Employee" : "Customer";
            return
                $"id: {ID}; " +
                $"name: {Name} ({role}); " +
                $"phoneNum: {PhoneNumber}; " +
                $"address: {Address}; " +
                $"birthday: {Birthday.ToShortDateString()}";
        }
    }

}
