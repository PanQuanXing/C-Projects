   M S B A M L   `   `   `  ����  PqxDesignSearch     PqxDesignSearch.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   a ]Telerik.Windows.Data, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7e aTelerik.Windows.Controls, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7n jTelerik.Windows.Controls.GridView, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7k gTelerik.Windows.Controls.Input, Version=2017.3.913.45, Culture=neutral, PublicKeyToken=5803cfa389c90ce7Etelerik1http://schemas.telerik.com/2008/xaml/presentation     5   	   -   5        $	��600��5   	   $	��800��6     � WindowStartupLocation$  CenterScreen=�6"    � Icon$ PqxDesignSearch.ico��6u   .��5      � -   5        $��mainGrid��5       � ColumnDefinitions 5   
   �� 5      �� 5	      $
��auto �6   �� 5
       � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��0��6#   $��请选择搜索方式��60   $��2q�6A   ���	#FFBCBCBC.��5      �  5      �� 5      �� 5      $
��auto �6'   .+�5      �� -   5         ��Name$ waysComboBox��5      $��0��61   $��1q�6A   
�������6L   �� 5      -   5         ��Name$ editWayButton��5      $��1��60   $��编辑算法��6@   $��1q�6O   
�������6m   5      5      �� 5   
   -   5        $ settingButton��5      $��1��6(   $��0��68   $!��设置连接数据库参数��6E   $��8q�6Y   6d   �� 5   
   $��2��6   $��0��6#   $��查找��60   $��2q�6<   ���	#FFBCBCBC.��5      �  5      �� 5      �� 5       $
��auto �6'   �� 5!      $
��auto �6'   .+�5#      �� -   5         ��Name$ fieldTextBox��5#      $��0��60   $��1q�6@   
�������6K   �� 5$      -   5        $ exactButton��5$      $��1��6.   $��精确搜索��6>   $��1q�6M   
������� �� 	IsDefault . 6�   �� 5%      -   5        $ fuzzyButton��5%      $��2��6.   $��模糊搜索��6>   $��1q�6M   
�������6k   5&      5'      *  $Telerik.Windows.Controls.RadGridView  5(   
   -	   5          Name$ 	gridView1��5(      $��1��61   $��0��6>   $��3��6N   ;  5Telerik.Windows.Controls.GridView.GridViewDataControl	   
ScrollMode$	 RealTime=�6b   
   
IsReadOnly
 .  %� IsVirtualizing . 5*      �� 5,   
   -
   5         ��Name$ megTextBlock��5,      $��2��6*   $��0��67   $��3��6G   $��Center=�6[   
���  ��6x   5.      5/      