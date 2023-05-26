using DALC;
using Entities;
using Microsoft.Extensions.Configuration;

namespace BLC
{
    public class BLC_AUTH
    {
        private DALC_SQL_AUTH _dalc_Auth;
        private GenerateJWT _generateJWT;
        private IConfiguration _configuration;

        public BLC_AUTH(IConfiguration configuration)
        {
            this._dalc_Auth = new();
            _configuration = configuration;
            this._generateJWT = new(_configuration);
        }

        public List<Role> GET_ROLES()
        {
            List<Role> roles = _dalc_Auth.get_roles();
            roles = roles.OrderBy(x => x.role_name).ToList();
            return _dalc_Auth.get_roles();
        }

        public void user_registration(RegistrationBodyRequest user_info) 
        {
            UserResponse user = _dalc_Auth.GetUserByEmail(user_info.email);
            if(user == null)
            {
                string encrypted_pass = EncryptPassword.HashPassword(user_info.password);
                string dateString = user_info.dateofbirth;
                DateOnly date = DateOnly.Parse(dateString);
                string verification_token = _generateJWT.GenerateJwtVerificationToken(user_info.email);
                EmailSender s = new EmailSender(_configuration);
                s.sendEmail(user_info.email, verification_token);
                this._dalc_Auth.user_register(user_info, encrypted_pass, date, verification_token);
            }
            else
            {
                throw new UserEmailExistException("User Email already exist, please try another email!");
            }
            
        }

        public string user_login(LoginRequestBody user_info)
        {
            UserResponse user = _dalc_Auth.GetUserByEmail(user_info.email);
            if (user != null) // that mean email exist and correct
            {
                if(EncryptPassword.VerifyPassword(user_info.password, user.password)) // that mean password is correct 
                {
                    // TODO 1 : check email is verified
                    // TODO 2 : generate a token that hold user information
                    var token = _generateJWT.GenerateJwtToken(user);
                    // return jwtService.generateJWT(user);
                    return token;
                }

                return "incorrect password";
            }

            return "login failed, Please check if you are entering a correct email or password!";
        }

        
    }
}
