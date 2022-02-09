using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Repository
{
    public class UserPlanValidityRepository : RepositoryBase<UserPlanValidity>, IUserPlanValidityRepository
    {
        public UserPlanValidityRepository(RepositoryContext repositoryContext)
            : base(repositoryContext)
        {
        }
        public void CreateUserPlanValidity(UserPlanValidity userPlanValidity)
        {
            Create(userPlanValidity);
        }

        public UserPlanValidity GetUserPlanValidity(string UserName)
        {
            return FindByCondition(c => c.UserName.Equals(UserName)).SingleOrDefault();
        }

        public IEnumerable< UserPlanValidity> GetAllUserPlanValidity()
        {
            return FindAll().ToList();
        }

        public void UpdateUserPlanValidity(UserPlanValidity userPlanValidity)
        {
            UpdateAsync(userPlanValidity);
        }
    }
}
