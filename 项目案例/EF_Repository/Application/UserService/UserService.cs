using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository;
using Entity;

namespace Application
{
    public class UserService
    {
        private IUserRepository service = new UserRepository();

        public bool Add(UserEntity entity)
        {
            return service.Add(entity);
        }
    }
}
