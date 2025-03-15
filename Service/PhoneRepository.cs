using System.Collections.Generic;
using csc13001_data_binding.Interfaces;
using csc13001_data_binding.Model;

namespace csc13001_data_binding.Service
{
    public class PhoneRepository(IPhoneDao dao) : IPhoneRepository
    {
        private readonly IPhoneDao _dao = dao;

        public IEnumerable<Phone> GetAll()
        {
            return _dao.GetAll();
        }

        public Phone AddPhone(Phone phone)
        {
            return _dao.AddPhone(phone);
        }

        public void DeletePhone(int id)
        {
            _dao.DeletePhone(id);
        }

        public Phone UpdatePhone(Phone phone)
        {
            return _dao.UpdatePhone(phone);
        }
    }
}
