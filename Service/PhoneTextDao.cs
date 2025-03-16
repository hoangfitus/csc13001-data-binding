using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using csc13001_data_binding.Interfaces;
using csc13001_data_binding.Model;

namespace csc13001_data_binding.Service
{
    public class PhoneTextDao : IPhoneDao
    {
        private readonly List<Phone> _phones;
        private readonly string _filePath;
        private readonly string _folder;

        public PhoneTextDao()
        {
            _folder = AppDomain.CurrentDomain.BaseDirectory;
            _filePath = Path.Combine(_folder, "phones.txt");
            _phones = LoadPhonesFromFile();
        }

        private List<Phone> LoadPhonesFromFile()
        {
            var phones = new List<Phone>();
            const string imageFolder = "/Assets/";
            if (File.Exists(_filePath))
            {
                var lines = File.ReadAllLines(_filePath);
                foreach (var line in lines)
                {
                    var tokens = line.Split(',');

                    string priceString = tokens[2].Replace(".", "");
                    phones.Add(new Phone
                    {
                        Id = phones.Any() ? phones.Max(p => p.Id) + 1 : 1,
                        Name = tokens[0],
                        Manufacturer = tokens[1].Trim(),
                        Price = decimal.Parse(priceString),
                        Image = $"{imageFolder}{tokens[3].Trim()}",
                    });

                   
                }
            }
            return phones;
        }


        private void SavePhonesToFile()
        {
            var lines = _phones.Select(p => $"{p.Name}, {p.Manufacturer}, {p.Price}, {p.Image.Replace("/Assets/", "")}");
            File.WriteAllLines(_filePath, lines);
        }

        public IEnumerable<Phone> GetAll()
        {
            return _phones;
        }

        public Phone AddPhone(Phone phone)
        {
            phone.Id = _phones.Any() ? _phones.Max(p => p.Id) + 1 : 1;
            _phones.Add(phone);
            SavePhonesToFile();
            return phone;
        }

        public void DeletePhone(int id)
        {
            var phone = _phones.FirstOrDefault(p => p.Id == id);
            if (phone != null)
            {
                _phones.Remove(phone);
                SavePhonesToFile();
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
                SavePhonesToFile();
            }
            return phone;
        }
    }
}
