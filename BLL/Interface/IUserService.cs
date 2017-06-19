
using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IService<Tentity> where Tentity:IEntity
    {
        Tentity Get(int id);
        IEnumerable<Tentity> GetAll();
        void Create(Tentity e);
        void Update(Tentity e);
        void Delete(int id);
    }
}
