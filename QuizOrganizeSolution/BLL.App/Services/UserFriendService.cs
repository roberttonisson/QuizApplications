using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.DTO.Identity;
using BLL.App.Mappers;
using BLL.Base.Services;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;
using DAL.Base.Mappers;


namespace BLL.App.Services
{
    public class UserFriendService :
        BaseEntityService<IAppUnitOfWork, IUserFriendRepository,
            BLLMapper<DAL.App.DTO.UserFriend, BLL.App.DTO.UserFriend>,
            DAL.App.DTO.UserFriend, BLL.App.DTO.UserFriend>, IUserFriendService
    {
        public UserFriendService(IAppUnitOfWork uow) : base(uow, uow.UserFriends,
            new BLLMapper<DAL.App.DTO.UserFriend, BLL.App.DTO.UserFriend>())
        {
        }

        public async Task<IEnumerable<AppUser>> SearchUsers(string search, Guid? userId = null,
            bool noTracking = true)
        {
            Dictionary<AppUser, int> dic =
                new Dictionary<AppUser, int>();
            var result = await UOW.AppUsers.GetAllAsync();
            foreach (var user in result)
            {
                var counter = 0;
                foreach (var word in search.Split(" "))
                {
                    if (user.Email.ToLower().Contains(word.ToLower()))
                    {
                        counter += 1;
                    }

                    if (user.UserName.ToLower().Contains(word.ToLower()))
                    {
                        counter += 1;
                    }

                    if (user.FirstName.ToLower().Contains(word.ToLower()))
                    {
                        counter += 1;
                    }

                    if (user.LastName.ToLower().Contains(word.ToLower()))
                    {
                        counter += 1;
                    }
                }

                if (counter > 0)
                {
                    dic.Add(new BaseMapper<DAL.App.DTO.Identity.AppUser, AppUser>().Map(user), counter);
                }
                
            }

            var myList = dic.ToList();
            myList = myList.OrderByDescending(a => a.Value).ToList();
            return myList.Select(u => u.Key).ToList();
        }
    }
}