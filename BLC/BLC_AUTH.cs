using DALC;
using Entities;

namespace BLC
{
    public class BLC_AUTH
    {
        private DALC_SQL_AUTH _dalc_Auth;
     
        public BLC_AUTH()
        {
            this._dalc_Auth = new();
        }

        public List<Role> GET_ROLES()
        {
            List<Role> roles = _dalc_Auth.get_roles();
            roles = roles.OrderBy(x => x.role_name).ToList();
            return _dalc_Auth.get_roles();
        }

        public void user_registration(RegistrationBodyRequest user_info)
        {
            string encrypted_pass = EncryptPassword.HashPassword(user_info.password);
            string dateString = user_info.dateofbirth;
            DateOnly date = DateOnly.Parse(dateString);
            this._dalc_Auth.user_register(user_info, encrypted_pass, date);
        }
    }
}
