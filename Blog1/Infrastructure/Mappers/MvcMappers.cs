using BLL;
using Blog1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog1.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName
            };
        }

        public static UserEntity ToBllUser(this UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                Id = userViewModel.Id,
                UserName = userViewModel.UserName
            };
        }
    }
}