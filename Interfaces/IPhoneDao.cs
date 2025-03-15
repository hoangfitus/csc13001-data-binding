using System.Collections.Generic;
using csc13001_data_binding.Model;

namespace csc13001_data_binding.Interfaces
{
    public interface IPhoneDao
    {
        IEnumerable<Phone> GetAll();
        Phone AddPhone(Phone phone);
        Phone UpdatePhone(Phone phone);
        void DeletePhone(int id);
    }
}
