   M S B A M L   `   `   `  ���� G  CPqxSqlHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PqxSqlHelperPqxSqlHelper       PqxSqlHelper.ConnectWindow   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    +loacalclr-namespace:PqxSqlHelper    -     � Title$  访问数据库设置��$	��200��$	��300�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � Topmost .  � 	Resources %  PqxSqlHelper.IsCheckedConverter   isCheckedConverter��%   &	          .��� -   $��mainGrid�� � ColumnDefinitions �� $��* ��� $��2* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$��0��$��服务器IP:��$��2q�$��Center=�$��Center=��� -   $��	ipTextBox��$��0��$��1��$��2q�
������� �� 
IsReadOnly �� 	 �� ElementName$	 remCheckBox��
 �� Path$
 	IsChecked� �� Mode$ OneWay=��� $��1��$��0��$��SQL用户名：��$��2q�$��Center=�$��Center=��� -   $��userNameTextBox��$��1��$��1��$��2q�
������� �� $	 remCheckBox��$
 	IsChecked�$ OneWay=��� $��2��$��0��$��SQL用户密码：��$��Center=�$��Center=�G� -   $��passwordBox��$��2��$��1��$��2q�
�������{��� $	 remCheckBox��$
 	IsChecked�$ OneWay=� �� 	Converter# [  �� $��3��$��0��$��数据库名：��$��Center=�$��Center=��� -   $��dbNameTextBox��$��3��$��1��$��2q�
������� �� $	 remCheckBox��$
 	IsChecked�$ OneWay=��� -   $��remCheckBox��$��4��$��0��$��锁定��$��Center=�$��Center=��� -   $��submitButton��$��4��$��1��$��确定��$��5q��� -	   $��tipsTextBlock��$��5��$��0��$��2��
���  ��$��Center=�