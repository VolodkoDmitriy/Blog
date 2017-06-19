using BLL.DTO;
using DAL;
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mappers
{    
    public static class BllEntityMappers
    {
        
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.Id,
                Name = userEntity.UserName,
                Password = userEntity.Password,
                Email = userEntity.Email,
                RoleId = userEntity.Role
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                Id = dalUser.Id,
                UserName = dalUser.Name,
                Password = dalUser.Password,
                Email = dalUser.Email,
                Role = dalUser.RoleId
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                Name = roleEntity.RoleName
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                Id = dalRole.Id,
                RoleName = dalRole.Name
            };
        }

        public static DalPost ToDalPost(this PostEntity postEntity)
        {
            return new DalPost()
            {
                Id = postEntity.Id,
                Text = postEntity.Text
            };
        }

        public static PostEntity ToBllPost(this DalPost dalPost)
        {
            return new PostEntity()
            {
                Id = dalPost.Id,
                Text = dalPost.Text
            };
        }
        //CommentEntity
        public static DalComment ToDalComment(this CommentEntity commentEntity)
        {
            return new DalComment()
            {
                Id = commentEntity.Id,
                PostId = commentEntity.PostId,
                UserId = commentEntity.UserId,
                Text = commentEntity.Text
                
            };
        }

        public static CommentEntity ToBllComment(this DalComment dalComment)
        {
            return new CommentEntity()
            {
                Id = dalComment.Id,
                PostId = dalComment.PostId,
                UserId = dalComment.UserId,
                Text = dalComment.Text
            };
        }
       
    }
}
