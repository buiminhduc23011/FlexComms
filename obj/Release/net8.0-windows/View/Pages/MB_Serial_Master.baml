   M S B A M L   `   `   `  ���� D  @FlexComms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:FlexComms.ViewModel.PagesFlexComms.ViewModel.Pages  ;"clr-namespace:FlexComms.View.PagesFlexComms.View.Pages  +    %FlexComms.View.Pages.MB_Serial_Master   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     =	ViewModel'clr-namespace:FlexComms.ViewModel.Pages     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:FlexComms.View.Pages     c _MaterialDesignThemes.MahApps, Version=3.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962a_ [MaterialDesignThemes.Wpf, Version=5.1.0.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aHmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006     N� Title$  MB_Serial_Master�� � DataContext :   4FlexComms.ViewModel.Pages.MB_Serial_Master_ViewModel  .���  � RowDefinitions �� $	��160 ��� $��* ��� $
��Auto �.+�#  MaterialDesignThemes.Wpf.Card  $��0��$��10q�.  (MaterialDesignThemes.Wpf.ElevationAssist   	Elevation$	 Dp2=�   UniformCornerRadius$ 5Q�.��� $��5q� �� $
��Auto ��� $��* ��� $��* �.+��� $��0��$��5,0,0,0q�$��14(�$��SemiBold"�$��Connection Setting��� $��1�� � ColumnDefinitions �� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $��* ��� $
��Auto ��� $��* �.+�� $��0��$��5q�.+���  �� SelectedIndex$ 0��
 � Tag$	 COM������ *AvailablePorts+ �� Text �� *	Comport+	 �� Mode$	 TwoWay=�� $��1��$��5q�.+��� $ 0��
 �� SelectedValuePath$	
 Tag��$ Baudrate��{��� *IsEdit+ �� SelectedValue �� *
Baudrate+$	 TwoWay=�.@��� $
��9600��$
 9600���� $��19200��$ 19200���� $��38400��$ 38400���� $��57600��$ 57600���� $��115200��$ 115200��� $��2��$��5q�.+��� $
 Content��$ Parity��{��� *IsEdit+ �� *Parity+$	 TwoWay=�.@��� $
��None���� $	��Odd���� $
��Even��� $��3��$��5q�.+��� $ 0��$
 Content��$ StopBits��{��� *IsEdit+ �� *	Stopbit+$	 TwoWay=�.@��� $��1���� $��2��� $��4��$��5q�.+��� $ Timeout(ms)��{��� *IsEdit+���� *	Timeout+$	 TwoWay=�� $��5��$��5q�.+��� $ Time Polling(ms)��{��� *IsEdit+���� *
DelayPolls+$	 TwoWay=�� $��6��$��5q�.+��� $ ID��{��� *IsEdit+���� *ID+$	 TwoWay=�� $��7��$��5q�.+��� $ 3��$	
 Tag��$ IO�� �� *IO+$	 TwoWay=�.@��� $��Read Outputs bits��"   FlexComms.ViewModel.Pages.IO   ReadOutputBits# Z �� $��Read Inputs bits��   ReadInputsBits# Z �� $��Read Holding Register��   ReadHoldingRegister# Z �� $��Read Input Register��   ReadInputRegister# Z � $��8��$��10,0,0,5q�$��Bottom=�.+�d� $	��100��$
��Left=�
������ 
����������� *tgbtn_IsOpen+ d� 	IsChecked �� *IsOpen+$	 TwoWay=�����  !  MaterialDesignPaperDarkButton �� BasedOn# [   �� 
TargetType" d�.���  �� Property � �� �� Value$ Bắt Đầu����  � ��
 ����� �� Triggers P�  P� Property �@d�	IsChecked P� Value$
 TrueY�. ���  � ��$ Close����  � ��  PrimaryRedColor# [ P�  �@d�	IsChecked$ FalseY�. ���  � ��$
 Open����  � ��  SecundaryGreenColor# [ � $��2�� �� $��* ��� $��* ��� $��* ��� $��* �.+�� $��0��$��5q�.+��� $ Start Address��{��� *IsEdit+���� *StartAddress+$	 TwoWay=�� $��1��$��5q�.+��� $ End Address������ *
EndAddress+$	 TwoWay=�� $��2��$��5q�.+��� $ 0��$	
 Tag��$
 Type�� �� *
TypeData+$	 TwoWay=�.@��� $	��Int��(   "FlexComms.ViewModel.Pages.TypeData
   Int# Z �� $��float��   Float# Z � $��3��$��5q�.+��� $ 0��$	
 Tag��$
 Size�� �� *Size+$	 TwoWay=�.@��� $��1��$ 1���� $��2��$ 2��� $��1��.+�� .+�&   System.Windows.Controls.DataGrid  -   $��dataGrid��$��2q�$��Stretch=�$��Stretch=�   AutoGenerateColumns$ False��   CanUserSortColumns$ False��   HeadersVisibility$ Column=����� *Data+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5	  /System.Windows.Controls.DataGridLengthConverter$ *	    Header$ Address��1
  +System.Windows.Controls.DataGridBoundColumn  
  Binding  �� *	Address+  $ *	 $ +1��  �� *Plus1+  $ *	 $ +2��  �� *Plus2+  $ *	 $ +3��  �� *Plus3+  $ *	 $ +4��  �� *Plus4+  $ *	 $ +5��  �� *Plus5+  $ *	 $ +6��  �� *Plus6+  $ *	 $ +7��  �� *Plus7+  $ *	 $ +8��  �� *Plus8+  $ *	 $ +9��  �� *Plus9+�� $��2��$��5,0,0,0q����� *Status+$	 TwoWay=�