using Entities;

namespace DALC
{
    public interface IDALC_SQL_AUTH
    {
        public List<Role> get_roles();
        public void user_register(RegistrationBodyRequest user_info, string encrypted_pass, DateOnly date);
    }
}
