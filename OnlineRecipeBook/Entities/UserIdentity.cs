using System;

namespace Entities
{
    public class UserIdentity : CommonEntity
    {
        public string Login { get; set; }

        public string PasswordHashSum { get; set; }



        public UserIdentity() { }

        public UserIdentity(int id, string login, string hashedPassword) : base(id)
        {
            Login = login;

            PasswordHashSum = hashedPassword;
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

    }
}
