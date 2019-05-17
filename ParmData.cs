using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFTransform
{
    public  class ParmData:INotifyPropertyChanged
    {
        private  int _value;
        public  int ValueText
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                propertyChanged(nameof(ValueText));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void propertyChanged(string Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}
