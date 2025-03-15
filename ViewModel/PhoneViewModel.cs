using System.Linq;
using csc13001_data_binding.Interfaces;
using csc13001_data_binding.Model;
using csc13001_data_binding.Service;
using csc13001_data_binding.Utils;

namespace csc13001_data_binding.ViewModel;

public class PhoneViewModel
{
    private readonly IPhoneRepository _phoneRepository = Services.GetSingleton<IPhoneRepository>();
    public FullObservableCollection<Phone> Phones { get; set; }

    public PhoneViewModel()
    {
        Phones = new FullObservableCollection<Phone>(_phoneRepository.GetAll());
    }

    public void AddPhone(Phone phone)
    {
        _phoneRepository.AddPhone(phone);
        Phones.Add(phone);
    }

    public void DeletePhone(int id)
    {
        _phoneRepository.DeletePhone(id);
        var phone = Phones.FirstOrDefault(p => p.Id == id);
        if (phone != null)
        {
            Phones.Remove(phone);
        }
    }

    public void UpdatePhone(Phone phone)
    {
        _phoneRepository.UpdatePhone(phone);
        var existingPhone = Phones.FirstOrDefault(p => p.Id == phone.Id);
        if (phone != null)
        {
            Phones.Remove(phone);
        }
    }
}
