using System;
using System.ComponentModel;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using FlexComms.Base;
using Modbus.Device;

namespace FlexComms.ViewModel.Pages
{
    public class MB_TCP_Master_ViewModel : BaseViewModel
    {
        private string _ipAddress;
        private int _port;
        private string _status;
        private ushort _addressWrite;
        private ushort _valueWrite;
        private string _dataRead;
        private ushort _startAddress;
        private ushort _endAddress;
        private bool _isopen;
        private TcpClient _tcpClient;
        private IModbusMaster _modbusMaster;


        public string IP_Address
        {
            get => _ipAddress;
        //    set => SetProperty(ref _ipAddress, value);
        }

        public int Port
        {
            get => _port;
         //   set => SetProperty(ref _port, value);
        }

        public bool IsOpen
        {
            get => _isopen;
            set
            {
                if (value != _isopen)
                {
                    if (value)
                    {
                        // Use Task.Run to avoid blocking the main thread.
                        Task.Run(async () =>
                        {
                            if (await Connect())
                            {
                                _isopen = true;
                            }
                            else
                            {
                                _isopen = false;
                            }
                        });
                    }
                    else
                    {
                        Task.Run(() => Disconnect());
                        _isopen = false;
                    }
                }
            }
        }

        public string Status
        {
            get => _status;
            set => OnPropertyChanged(nameof(Status));
        }

        public ushort Address_Write
        {
            get => _addressWrite;
            set => OnPropertyChanged(nameof(Address_Write));
        }

        public ushort Value_Write
        {
            get => _valueWrite;
            set => OnPropertyChanged(nameof(Value_Write));
        }

        public ushort Start_Address
        {
            get => _startAddress;
            set => OnPropertyChanged(nameof(Start_Address));
        }

        public ushort End_Address
        {
            get => _endAddress;
            set => OnPropertyChanged(nameof(End_Address));
        }

        public string Data_Read
        {
            get => _dataRead;
            set => OnPropertyChanged(nameof(Data_Read));
        }

        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand ReadCommand { get; }
        public ICommand WriteCommand { get; }
        public MB_TCP_Master_ViewModel()
        {
            //ConnectCommand = new RelayCommand(async _ => await Connect(), _ => !IsOpen);
            //DisconnectCommand = new RelayCommand(_ => Disconnect(), _ => IsOpen);
            //ReadCommand = new RelayCommand(async _ => await ReadData(), _ => IsOpen);
            //WriteCommand = new RelayCommand(async _ => await WriteData(), _ => IsOpen);
        }

        private async Task<bool> Connect()
        {
            Status = "Connecting.....";
            try
            {
                _tcpClient = new TcpClient();

                // Thêm timeout cho kết nối
                var connectTask = _tcpClient.ConnectAsync(IP_Address, Port);
                if (await Task.WhenAny(connectTask, Task.Delay(10000)) == connectTask)
                {
                    // Kết nối thành công
                    _modbusMaster = ModbusIpMaster.CreateIp(_tcpClient);
                    Status = "Kết nối thành công với slave.";
                    IsOpen = true;
                    return true;
                }
                else
                {
                    throw new TimeoutException("Kết nối bị timeout.");
                }
            }
            catch (SocketException ex)
            {
                Status = $"Kết nối thất bại: {ex.Message} (Lỗi socket: {ex.SocketErrorCode})";
                IsOpen = false;
                return false;
            }
            catch (Exception ex)
            {
                Status = $"Kết nối thất bại: {ex.Message}";
                IsOpen = false;
                return false;
            }
        }

        private void Disconnect()
        {
            _tcpClient?.Close();
            Status = "Disconnected";
        }
        private async Task ReadData()
        {
            try
            {
                if (_modbusMaster != null)
                {
                    int numberOfRegisters = End_Address - Start_Address + 1;
                    var data = await Task.Run(() =>
                        _modbusMaster.ReadHoldingRegisters(1, Start_Address, (ushort)numberOfRegisters));

                    Data_Read = string.Join(", ", data);
                    Status = "Read successful";
                }
                else
                {
                    Status = "Not connected";
                }
            }
            catch (Exception ex)
            {
                Status = $"Read failed: {ex.Message}";
            }
        }

        private async Task WriteData()
        {
            try
            {
                if (_modbusMaster != null)
                {
                    await Task.Run(() =>
                        _modbusMaster.WriteSingleRegister(1, Address_Write, Value_Write));

                    Status = "Write successful";
                }
                else
                {
                    Status = "Not connected";
                }
            }
            catch (Exception ex)
            {
                Status = $"Write failed: {ex.Message}";
            }
        }
    }
}
