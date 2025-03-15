using System.Collections.Generic;
using System.Linq;
using csc13001_data_binding.Interfaces;
using csc13001_data_binding.Model;

namespace csc13001_data_binding.Service
{
    public class PhoneMockDao : IPhoneDao
    {
        private readonly List<Phone> _phones;

        public PhoneMockDao()
        {
            _phones = new List<Phone>
            {
                new Phone { Id = 1, Name = "Iphone 13", Manufacturer = "Apple", Price = 199.99m, Image = "/Assets/iphone13.png" },
                new Phone { Id = 2, Name = "Iphone 13 Pro", Manufacturer = "Apple", Price = 249.99m, Image = "/Assets/iphone13pro.png"},
                new Phone { Id = 3, Name = "Iphone 13 Pro Max", Manufacturer = "Apple", Price = 299.99m, Image = "/Assets/iphone13promax.png" },
                new Phone { Id = 4, Name = "Samsung Galaxy S20", Manufacturer = "Samsung", Price = 239.99m, Image = "/Assets/samsunggalaxys20.png" },
                new Phone { Id = 5, Name = "Samsung Galaxy ZFlip", Manufacturer = "Samsung", Price = 339.99m, Image = "/Assets/samsunggalaxyzflip.png" },
            };
        }
        public IEnumerable<Phone> GetAll()
        {
            return _phones;
        }

        public Phone AddPhone(Phone phone)
        {
            _phones.Add(phone);
            return phone;
        }

        public void DeletePhone(int id)
        {
            var phone = _phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                _phones.Remove(phone);
            }
        }

        public Phone UpdatePhone(Phone phone)
        {
            var existingPhone = _phones.FirstOrDefault(p => p.Id == phone.Id);
            if (existingPhone != null)
            {
                existingPhone.Name = phone.Name;
                existingPhone.Manufacturer = phone.Manufacturer;
                existingPhone.Price = phone.Price;
                existingPhone.Image = phone.Image ?? existingPhone.Image;
            }
            return phone;
        }
    }
}
