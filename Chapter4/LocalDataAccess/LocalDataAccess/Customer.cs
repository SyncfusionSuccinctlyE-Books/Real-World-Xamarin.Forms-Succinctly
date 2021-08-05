using SQLite;
using System.ComponentModel;
namespace LocalDataAccess
{
    [Table("Customers")]
    public class Customer : INotifyPropertyChanged
    {
        private int _id;
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                this._id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string _companyName;
        [NotNull]
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                this._companyName = value;
                OnPropertyChanged(nameof(CompanyName));
            }
        }
        private string _physicalAddress;
        [MaxLength(50)]
        public string PhysicalAddress
        {
            get
            {
                return _physicalAddress;
            }
            set
            {
                this._physicalAddress = value;
                OnPropertyChanged(nameof(PhysicalAddress));
            }
        }
        private string _country;
        public string Country
        {
            get
            {
                return _country;
            }
            set
            {
                _country = value;
                OnPropertyChanged(nameof(Country));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this,
              new PropertyChangedEventArgs(propertyName));
        }
    }
}
