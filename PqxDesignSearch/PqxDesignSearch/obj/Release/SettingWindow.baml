   M S B A M L   `   `   `  ���� J  FPqxDesignSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PqxDesignSearchPqxDesignSearch  #    PqxDesignSearch.SettingWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /localclr-namespace:PqxDesignSearch     -     � Title$  访问数据库设置��$	��200��$	��300�� � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=� � ShowInTaskbar .   � Topmost .  � 	Resources (  "PqxDesignSearch.IsCheckedConverter   isCheckedConverter��%   &	          .��� -   $��mainGrid�� � ColumnDefinitions �� $��* ��� $��2* � � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$��0��$��服务器IP:��$��2q�$��Center=�$��Center=�G� -   $��ipPasswordBox��$��0��$��1��$��2q�
�������{���  �� ElementName$ remCheckBox��	 �� Path$	 	IsChecked�
 �� Mode$
 OneWay=� �� 	Converter# [  �� $��1��$��0��$��SQL用户名：��$��2q�$��Center=�$��Center=�G� -   $��userNamePasswordBox��$��1��$��1��$��2q�
�������{��� $ remCheckBox��$	 	IsChecked�$
 OneWay=�# [  �� $��2��$��0��$��SQL用户密码：��$��Center=�$��Center=�G� -   $��passwordBox��$��2��$��1��$��2q�
�������{��� $ remCheckBox��$	 	IsChecked�$
 OneWay=�# [  �� $��3��$��0��$��数据库名：��$��Center=�$��Center=��� -   $��dbNameTextBox��$��3��$��1��$��2q�
������� �� 
IsReadOnly �� $ remCheckBox��$	 	IsChecked�$
 OneWay=��� -   $��remCheckBox��$��4��$��0��$��锁定��$��Center=�$��Center=��� -   $��submitButton��$��4��$��1��$��确定��$��5q��� -	   $��tipsTextBlock��$��5��$��0��$��2��
���  ��$��Center=�