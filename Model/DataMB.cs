using FlexComms.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlexComms.Model
{
    public class DataMB : BaseViewModel
    {
        private string? _address;
        public string? Address
        {
            get => _address;
            set
            {
                if (_address != value)
                {
                    _address = value;
                    OnPropertyChanged(nameof(Address));
                }
            }
        }


        private string? _plus1;
        public string? Plus1
        {
            get => _plus1;
            set
            {
                if (_plus1 != value)
                {
                    _plus1 = value;
                    OnPropertyChanged(nameof(Plus1));
                }
            }
        }

        private string? _plus2;
        public string? Plus2
        {
            get => _plus2;
            set
            {
                if (_plus2 != value)
                {
                    _plus2 = value;
                    OnPropertyChanged(nameof(Plus2));
                }
            }
        }

        private string? _plus3;
        public string? Plus3
        {
            get => _plus3;
            set
            {
                if (_plus3 != value)
                {
                    _plus3 = value;
                    OnPropertyChanged(nameof(Plus3));
                }
            }
        }

        private string? _plus4;
        public string? Plus4
        {
            get => _plus4;
            set
            {
                if (_plus4 != value)
                {
                    _plus4 = value;
                    OnPropertyChanged(nameof(Plus4));
                }
            }
        }

        private string? _plus5;
        public string? Plus5
        {
            get => _plus5;
            set
            {
                if (_plus5 != value)
                {
                    _plus5 = value;
                    OnPropertyChanged(nameof(Plus5));
                }
            }
        }

        private string? _plus6;
        public string? Plus6
        {
            get => _plus6;
            set
            {
                if (_plus6 != value)
                {
                    _plus6 = value;
                    OnPropertyChanged(nameof(Plus6));
                }
            }
        }

        private string? _plus7;
        public string? Plus7
        {
            get => _plus7;
            set
            {
                if (_plus7 != value)
                {
                    _plus7 = value;
                    OnPropertyChanged(nameof(Plus7));
                }
            }
        }

        private string? _plus8;
        public string? Plus8
        {
            get => _plus8;
            set
            {
                if (_plus8 != value)
                {
                    _plus8 = value;
                    OnPropertyChanged(nameof(Plus8));
                }
            }
        }

        private string? _plus9;
        public string? Plus9
        {
            get => _plus9;
            set
            {
                if (_plus9 != value)
                {
                    _plus9 = value;
                    OnPropertyChanged(nameof(Plus9));
                }
            }
        }
    }
}
