   M S B A M L   `   `   `  ����J  FPqxDesignSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PqxDesignSearchPqxDesignSearch  5   	   #    PqxDesignSearch.SettingWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:PqxDesignSearch     5   	   -   5          � Title$  访问数据库设置��5   	   $	��200��6   $	��300��6&    � WindowStyle$ 
ToolWindow=�62    � WindowStartupLocation$ CenterScreen=�6K    � ShowInTaskbar .   � Topmost .  � 	Resources 5      (  "PqxDesignSearch.IsCheckedConverter   isCheckedConverter��%   &	          5   
   6#   5      .��5	      � -   5        $��mainGrid��5	       � ColumnDefinitions 5
   
   �� 5      $��* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      .+�5   
   �� $��0��6   $��0��6!   $��服务器IP:��61   $��2q�6?   $��Center=�6J   $��Center=�6g   G� 5   
   -   5        $��ipPasswordBox��5      $��0��6+   $��1��68   $��2q�6H   
�������{�6f   ��  �� ElementName$ remCheckBox��	 �� Path$	 	IsChecked�
 �� Mode$
 OneWay=� �� 	Converter# [  6�   �� 5   
   $��1��6   $��0��6!   $��SQL用户名：��61   $��2q�6@   $��Center=�6K   $��Center=�6h   G� 5   
   -   5        $��userNamePasswordBox��5      $��1��61   $��1��6>   $��2q�6N   
�������{�6l   �� $ remCheckBox��$	 	IsChecked�$
 OneWay=�# [  �� 5   
   $��2��6   $��0��6!   $��SQL用户密码：��61   $��Center=�6A   $��Center=�6^   G� 5   
   -   5        $��passwordBox��5      $��2��6)   $��1��66   $��2q�6F   
�������{�6d   �� $ remCheckBox��$	 	IsChecked�$
 OneWay=�# [  �� 5!   
   $��3��6   $��0��6!   $��数据库名：��61   $��Center=�6>   $��Center=�6[   �� 5"   
   -   5        $��dbNameTextBox��5"      $��3��6'   $��1��64   $��2q�6D   
������� �� 
IsReadOnly 6b   �� $ remCheckBox��$	 	IsChecked�$
 OneWay=��� 5#   
   -   5        $��remCheckBox��5#      $��4��6&   $��0��63   $��锁定��6C   $��Center=�6P   $��Center=�6m   �� 5$   
   -   5        $��submitButton��5$      $��4��6%   $��1��62   $��确定��6B   $��5q�6O   6Z   �� 5%   
   -	   5        $��tipsTextBlock��5%      $��5��6)   $��0��66   $��2��6F   
���  ��$��Center=�6k   5&      5'      