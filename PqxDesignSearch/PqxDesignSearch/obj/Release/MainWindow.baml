   M S B A M L   `   `   `  ����   PqxDesignSearch     PqxDesignSearch.MainWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     a ]Telerik.Windows.Data, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Etelerik1http://schemas.telerik.com/2008/xaml/presentation     -   $	��600��$	��800��  � WindowStartupLocation$  CenterScreen=� � Icon$ PqxDesignSearch.ico��.��� -   $��mainGrid�� � ColumnDefinitions �� �� $
��auto ���  � RowDefinitions �� $
��auto ��� �� $
��auto �.+��� $��0��$��0��$��请选择搜索方式��$��2q����	#FFBCBCBC.���  �� �� $
��auto �.+��� -    ��Name$ waysComboBox��$��0��$��1q�
��������� -    ��Name$ editWayButton��$��1��$��编辑算法��$��1q�
��������� -   $ settingButton��$��1��$��0��$!��设置连接数据库参数��$��8q��� $��2��$��0��$��查找��$��2q����	#FFBCBCBC.���  �� �� $
��auto ��� $
��auto �.+��� -    ��Name$ fieldTextBox��$��0��$��1q�
��������� -   $ exactButton��$��1��$��精确搜索��$��1q�
������� �� 	IsDefault . �� -   $ fuzzyButton��$��2��$��模糊搜索��$��1q�
�������*  $Telerik.Windows.Controls.RadGridView  -	     Name$ 	gridView1��$��1��$��0��$��3��;  5Telerik.Windows.Controls.GridView.GridViewDataControl	   
ScrollMode$	 RealTime=�
   
IsReadOnly
 .  %� IsVirtualizing . �� -
    ��Name$ megTextBlock��$��2��$��0��$��3��$��Center=�
���  ��