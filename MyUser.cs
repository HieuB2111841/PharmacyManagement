using Google.Protobuf;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace QLNhaThuoc
{
    public class MyUser
    {
        private static MyUser _instance = new MyUser();

        /// <summary>
        ///     Đối tượng chứaàthông tin của người dùng
        /// </summary>
        public static MyUser Instance => _instance;


        private string _id = string.Empty;
        private string _name = string.Empty;
        private string _phoneNumber = string.Empty;
        private string _address = string.Empty;
        private DateTime _birthday = DateTime.MinValue;

        private bool _isEmployee = false;

        public string ID => _id;
        public string Name => _name;
        public string PhoneNumber => _phoneNumber;
        public DateTime Birthday => _birthday;
        public string Address => _address;
        public bool IsEmployee => _isEmployee;



        /// <summary>
        ///     Tránh khởi tạo đối tượng MyUser ở nơi khác
        /// </summary>
        private MyUser() { }


        public bool SetUser(string id)
        {
            string query = "SELECT maUser AS id, tenUser AS name, soDienThoai AS phoneNumber, diaChi AS address, ngaySinh AS birthday, nhanVien AS role " +
                 "FROM user WHERE maUser = @id";
            DataTable data = MyPublics.Instance.GetData(query, ("@id", id));

            if (data.Rows.Count > 0)
            {
                try
                {
                    object[] userData = data.Rows[0].ItemArray;
                    _id = userData[0].ToString();
                    _name = userData[1].ToString();
                    _phoneNumber = userData[2].ToString();
                    _address = userData[3].ToString();

                    this.SetBirthday(userData[4].ToString());
                    this.SetRole(userData[5].ToString());

                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("From MyUser:" + e.Message, "Error");
                }
            }
            return false;
        }

        public bool SetUserByPhoneNumber(string phoneNumber)
        {
            string query = "SELECT maUser AS id, tenUser AS name, soDienThoai AS phoneNumber, diaChi AS address, ngaySinh AS birthday, nhanVien AS role " +
                "FROM user WHERE soDienThoai = @phoneNumber";
            DataTable data = MyPublics.Instance.GetData(query, ("@phoneNumber", phoneNumber));

            if (data.Rows.Count > 0)
            {
                try
                {
                    object[] userData = data.Rows[0].ItemArray;
                    _id = userData[0].ToString();
                    _name = userData[1].ToString();
                    _phoneNumber = userData[2].ToString();
                    _address = userData[3].ToString();

                    this.SetBirthday(userData[4].ToString());
                    this.SetRole(userData[5].ToString());

                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show("From MyUser:" + e.Message, "Error");
                }
            }
            return false;
        }


        private void SetBirthday(string value)
        {
            // Sử dụng ParseExact với định dạng cụ thể
            if (DateTime.TryParseExact(value, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                _birthday = parsedDate;
            }
            else _birthday = DateTime.MinValue;
        }

        private void SetRole(string value)
        {
            if (bool.TryParse(value, out bool result))
            {
                _isEmployee = result;
            }
            else _isEmployee = false;
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