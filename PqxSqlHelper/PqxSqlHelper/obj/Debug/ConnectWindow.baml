   M S B A M L   `   `   `  ����G  CPqxSqlHelper, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:PqxSqlHelperPqxSqlHelper  5   	        PqxSqlHelper.ConnectWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   +loacalclr-namespace:PqxSqlHelper    5   	   -   5          � Title$  访问数据库设置��5   	   $	��200��6   $	��300��6&    � WindowStyle$ 
ToolWindow=�62    � WindowStartupLocation$ CenterScreen=�6K    � ShowInTaskbar .   � Topmost .  � 	Resources 5      %  PqxSqlHelper.IsCheckedConverter   isCheckedConverter��%   &	          5   
   6$   5      .��5	      � -   5        $��mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��0��6   $��0��6!   $��服务器IP:��61   $��2q�6?   $��Center=�6J   $��Center=�6g   �� 5   
   -   5        $��	ipTextBox��5      $��0��6#   $��1��60   $��2q�6@   
������� �� 
IsReadOnly 6^   �� 	 �� ElementName$	 remCheckBox��
 �� Path$
 	IsChecked� �� Mode$ OneWay=�6�   �� 5   
   $��1��6   $��0��6!   $��SQL用户名：��61   $��2q�6@   $��Center=�6K   $��Center=�6h   �� 5   
   -   5        $��userNameTextBox��5      $��1��6)   $��1��66   $��2q�6F   
������� 6d   �� $	 remCheckBox��$
 	IsChecked�$ OneWay=��� 5   
   $��2��6   $��0��6!   $��SQL用户密码：��61   $��Center=�6A   $��Center=�6^   G� 5   
   -   5        $��passwordBox��5      $��2��6)   $��1��66   $��2q�6F   
�������{�6d   �� $	 remCheckBox��$
 	IsChecked�$ OneWay=� �� 	Converter# [  �� 5   
   $��3��6   $��0��6!   $��数据库名：��61   $��Center=�6>   $��Center=�6[   �� 5   
   -   5        $��dbNameTextBox��5      $��3��6'   $��1��64   $��2q�6D   
������� 6b   �� $	 remCheckBox��$
 	IsChecked�$ OneWay=��� 5    
   -   5        $��remCheckBox��5       $��4��6&   $��0��63   $��锁定��6C   $��Center=�6P   $��Center=�6m   �� 5!   
   -   5        $��submitButton��5!      $��4��6%   $��1��62   $��确定��6B   $��5q�6O   6Z   �� 5"   
   -	   5        $��tipsTextBlock��5"      $��5��6)   $��0��66   $��2��6F   
���  ��$��Center=�6k   5#      5$      