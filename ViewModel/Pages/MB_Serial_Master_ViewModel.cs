using FlexComms.Base;
using System;
using System.Windows;
using EasyModbus;
using System.IO.Ports;
using System.Windows.Input;
using System.Threading;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Collections.Generic;
using FlexComms.Model;
using static EasyModbus.ModbusServer;

namespace FlexComms.ViewModel.Pages
{



    public enum IO
    {
        ReadOutputBits = 1,
        ReadInputsBits = 2,
        ReadHoldingRegister = 3,
        ReadInputRegister = 4
    }
    public enum TypeData
    {
        Int = 1,
        Float = 2,
    }

    public class MB_Serial_Master_ViewModel : BaseViewModel
    {
        private string[] _availablePorts;
        public string[] AvailablePorts
        {
            get => _availablePorts;
            private set
            {
                _availablePorts = value;
                OnPropertyChanged(nameof(AvailablePorts));
            }
        }
        private string _comport;
        public string Comport
        {
            get => _comport;
            set
            {
                _comport = value;
                OnPropertyChanged(nameof(Comport));
            }
        }

        private int _baudrate;
        public int Baudrate
        {
            get => _baudrate;
            set
            {
                _baudrate = value;
                OnPropertyChanged(nameof(Baudrate));
            }
        }

        private Parity _parity;
        public Parity Parity
        {
            get => _parity;
            set
            {
                _parity = value;
                OnPropertyChanged(nameof(Parity));
            }
        }

        private StopBits _stopbit;
        public StopBits Stopbit
        {
            get => _stopbit;
            set
            {
                _stopbit = value;
                OnPropertyChanged(nameof(Stopbit));
            }
        }

        private int _timeout = 1000;
        public int Timeout
        {
            get => _timeout;
            set
            {
                _timeout = value;
                OnPropertyChanged(nameof(Timeout));
            }
        }

        private int _delayPolls = 200;
        public int DelayPolls
        {
            get => _delayPolls;
            set
            {
                _delayPolls = value;
                OnPropertyChanged(nameof(DelayPolls));
            }
        }

        private int _id = 1;
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }

        private IO _io;
        public IO IO
        {
            get => _io;
            set
            {
                _io = value;
                OnPropertyChanged(nameof(IO));
            }
        }
        private TypeData _typeData;
        public TypeData TypeData
        {
            get => _typeData;
            set
            {
                _typeData = value;
                OnPropertyChanged(nameof(TypeData));
            }
        }
        private int _size;
        public int Size
        {
            get => _size;
            set
            {
                _size = value;
                OnPropertyChanged(nameof(Size));
            }
        }

        private bool _isedit = true;
        public bool IsEdit
        {
            get => _isedit;
            set
            {
                _isedit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get => _isOpen;
            set
            {
                _isOpen = value;
                OnPropertyChanged(nameof(IsOpen));
            }
        }

        private int _startAddress = 0;
        public int StartAddress
        {
            get => _startAddress;
            set
            {
                if (value >= 0 && value <= 124)
                {
                    _startAddress = value;

                    // Cập nhật EndAddress nếu nó không còn lớn hơn StartAddress
                    if (_endAddress <= _startAddress)
                    {
                        EndAddress = _startAddress + 1; // Đảm bảo EndAddress lớn hơn StartAddress
                    }
                }
                OnPropertyChanged(nameof(StartAddress));
            }
        }

        private int _endAddress = 10;
        public int EndAddress
        {
            get => _endAddress;
            set
            {
                // Đảm bảo EndAddress lớn hơn StartAddress và trong khoảng 0 đến 125
                if (value > _startAddress && value <= 125)
                {
                    _endAddress = value;
                }
                else if (value <= _startAddress)
                {
                    _endAddress = _startAddress + 1; // Đặt EndAddress lớn hơn StartAddress nếu cần
                }
                OnPropertyChanged(nameof(EndAddress));
            }
        }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        private ObservableCollection<DataMB> _data;
        public ObservableCollection<DataMB> Data
        {
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged(nameof(Data));
            }
        }

        private ICommand _tgbtn_IsOpen;
        public ICommand tgbtn_IsOpen => _tgbtn_IsOpen ??= new RelayCommand(param =>
        {
            HandelModbus();
        });

        private ModbusClient MBClient;
        private Thread _workerThread;
        private bool _isThreadRunning;

        public MB_Serial_Master_ViewModel()
        {
            Data = new ObservableCollection<DataMB>();
            LoadAvailablePorts();
        }

        private void WorkerMethod()
        {
            while (_isThreadRunning)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    try
                    {
                        switch (IO)
                        {
                            case IO.ReadHoldingRegister:
                                int[] holdingRegisters = MBClient.ReadHoldingRegisters(StartAddress, EndAddress); // Đọc 10 thanh ghi bắt đầu từ địa chỉ 0
                                ConvertData(holdingRegisters, TypeData, Size);
                                break;
                            case IO.ReadInputRegister:
                                int[] inputRegisters = MBClient.ReadInputRegisters(StartAddress, EndAddress); // Đọc 10 thanh ghi đầu vào bắt đầu từ địa chỉ 0
                                ConvertData(inputRegisters, TypeData, Size);
                                break;
                            default:
                                Status = "Chế độ IO không được hỗ trợ.";
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Status = "Lỗi khi đọc dữ liệu:" + ex.Message.ToString();
                    }
                });

                Thread.Sleep(DelayPolls);
            }
        }
        private void ConvertData(int[] data, TypeData type, int size)
        {
            int sizeResult = data.Length / size;
            string[] dataResult = new string[data.Length];

            if (size == 1 && type == TypeData.Int)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    dataResult[i] = data[i].ToString(); // Chuyển dữ liệu int thành chuỗi
                }
            }
            else if (size == 2 && type == TypeData.Float)
            {
                for (int i = 0; i < data.Length - 1; i += 2)
                {
                    int combined = (data[i + 1] << 16) | data[i];
                    float floatValue = BitConverter.ToSingle(BitConverter.GetBytes(combined), 0);
                    dataResult[i] = floatValue.ToString("F2");
                }
            }

            else
            {
                Status = "Kích thước không được hỗ trợ.";
            }

            UpdateData(dataResult);
        }



        private bool InitModbus()
        {
            if (string.IsNullOrEmpty(Comport) || Comport.Length < 4)
            {
                MessageBox.Show("Điền Com đi!!!!!");
                return false;
            }
            try
            {
                MBClient = new ModbusClient
                {
                    SerialPort = Comport,
                    Baudrate = Baudrate,
                    Parity = Parity,
                    StopBits = Stopbit,
                    UnitIdentifier = (byte)ID,
                    ConnectionTimeout = Timeout
                };
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Error");
                return false;
            }
        }

        public bool MBConnect()
        {
            if (InitModbus())
            {
                try
                {
                    MBClient.Connect();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi kết nối: {ex.Message}", "Error");
                    return false;
                }
            }
            return false;
        }

        public void MBDisconnect()
        {
            MBClient?.Disconnect();
        }

        public void HandelModbus()
        {
            if (IsOpen)
            {
                IsOpen = MBConnect();
                if (IsOpen)
                {
                    IsEdit = false;
                    _isThreadRunning = true;
                    _workerThread = new Thread(WorkerMethod)
                    {
                        IsBackground = true
                    };
                    _workerThread.Start(); // Bắt đầu luồng khi kết nối thành công
                    Status = "Connected!!!!!";
                }
            }
            else
            {
                IsEdit = true;
                _isThreadRunning = false; // Dừng luồng
                _workerThread?.Join(); // Chờ luồng kết thúc
                MBDisconnect();
                IsOpen = false;
                Status = "Disconnect!!!!!";
            }
        }
        private void UpdateData(string[] data)
        {
            int baseAddress = 0;
            if (IO == IO.ReadHoldingRegister) baseAddress = 40000;
            else if (IO == IO.ReadInputRegister) baseAddress = 30000;

            for (int i = 0; i < data.Length; i += 10)
            {
                int indexData = i / 10;

                // Kiểm tra nếu indexData nhỏ hơn số phần tử hiện có
                if (indexData < Data.Count)
                {
                    // Cập nhật phần tử hiện có
                    Data[indexData].Address = (baseAddress + i).ToString();

                    // Cập nhật các thuộc tính, đảm bảo không vượt quá độ dài của data
                    Data[indexData].Plus1 = (i + 1 < data.Length) ? data[i + 1] : Data[indexData].Plus1;
                    Data[indexData].Plus2 = (i + 2 < data.Length) ? data[i + 2] : Data[indexData].Plus2;
                    Data[indexData].Plus3 = (i + 3 < data.Length) ? data[i + 3] : Data[indexData].Plus3;
                    Data[indexData].Plus4 = (i + 4 < data.Length) ? data[i + 4] : Data[indexData].Plus4;
                    Data[indexData].Plus5 = (i + 5 < data.Length) ? data[i + 5] : Data[indexData].Plus5;
                    Data[indexData].Plus6 = (i + 6 < data.Length) ? data[i + 6] : Data[indexData].Plus6;
                    Data[indexData].Plus7 = (i + 7 < data.Length) ? data[i + 7] : Data[indexData].Plus7;
                    Data[indexData].Plus8 = (i + 8 < data.Length) ? data[i + 8] : Data[indexData].Plus8;
                    Data[indexData].Plus9 = (i + 9 < data.Length) ? data[i + 9] : Data[indexData].Plus9;
                }
                else
                {
                    // Thêm phần tử mới nếu chưa có
                    Data.Add(new DataMB
                    {
                        Address = (baseAddress + i).ToString(),
                        Plus1 = (i + 1 < data.Length) ? data[i + 1] : (string?)null,
                        Plus2 = (i + 2 < data.Length) ? data[i + 2] : (string?)null,
                        Plus3 = (i + 3 < data.Length) ? data[i + 3] : (string?)null,
                        Plus4 = (i + 4 < data.Length) ? data[i + 4] : (string?)null,
                        Plus5 = (i + 5 < data.Length) ? data[i + 5] : (string?)null,
                        Plus6 = (i + 6 < data.Length) ? data[i + 6] : (string?)null,
                        Plus7 = (i + 7 < data.Length) ? data[i + 7] : (string?)null,
                        Plus8 = (i + 8 < data.Length) ? data[i + 8] : (string?)null,
                        Plus9 = (i + 9 < data.Length) ? data[i + 9] : (string?)null
                    });
                }
            }
            int countToRemove = Data.Count - data.Length / 10;
            for (int i = 0; i < countToRemove; i++)
            {
                Data.RemoveAt(Data.Count - 1);
            }
        }

        private void LoadAvailablePorts()
        {
            AvailablePorts = SerialPort.GetPortNames();
        }
    }
}
