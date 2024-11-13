using Google.Protobuf;
using QLNhaThuoc.Models;
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

        private User _user = new User();

        public string ID => _user.ID;
        public string Name => _user.Name;
        public string PhoneNumber => _user.PhoneNumber;
        public DateTime Birthday => _user.Birthday;
        public string Address => _user.Address;
        public bool IsEmployee => _user.IsEmployee;


        /// <summary>
        ///     Tránh khởi tạo đối tượng MyUser ở nơi khác
        /// </summary>
        private MyUser() { }


        public void SetUser(string id) => _user.SetByID(id);
        public void SetUserByPhoneNumber(string phoneNumber) => _user.SetByPhoneNumber(phoneNumber);
        public void Reset() => _user.Reset();

        public override string ToString() => Instance.ToString();
    }
}