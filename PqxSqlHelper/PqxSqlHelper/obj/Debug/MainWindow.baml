   M S B A M L   `   `   `  ����U  QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35[<clr-namespace:Microsoft.Windows.Controls;assembly=WPFToolkitMicrosoft.Windows.Controls  5   	   M ISystem, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089M3clr-namespace:System.ComponentModel;assembly=SystemSystem.ComponentModel 5   	    PqxSqlHelper   PqxSqlHelper.MainWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   8x,http://schemas.microsoft.com/winfx/2006/xaml    5   	   Ntoolkit<clr-namespace:Microsoft.Windows.Controls;assembly=WPFToolkit    5   	   @cm3clr-namespace:System.ComponentModel;assembly=System    5   	   -   5          � Title$3  -数据库查询助手（作者：潘全星）��5   	   $	��600��6!   $	��800��6.    � WindowStartupLocation$ CenterScreen=�6:    � Icon$ searchSqlHelper.ico��6z    � 	Resources 5      ,  &System.ComponentModel.BackgroundWorker   backgroundWorker1!   WorkerSupportsCancellation��%6   &	          5   
   -   5         . 5   |   5	      .��5
      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5      $
��auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      $��4* �6   �� 5      $��2* �6   �� 5      $��3* �6   .+�5   
   �� $��	选择表��6   $��0��6    $��3��6-   $��0��6>   $��1q�6N   ���	#FFBCBCBC.��5      � $��1q�6    5      �� 5      $��2* �6'   �� 5      $��5* �6'   �� 5      $��2* �6'   �� 5      $��5* �6'   .+�5       �� $��0��6   $��	类型：��6,   $��Center=�67   $��Right=�6R   �� 5!      -   5        $��typeComboBox��5!      $��1��6/    �� 
IsEditable . 
������� �� 
IsReadOnly . 	 �� SelectedIndex	 6v   �� *	TYPEINDEX+
 �� Mode$
 TwoWay=�6�   �� 5"      $��2��6   $��	表名：��6,   $��Center=�67   $��Right=�6R   �� 5#      -   5        $��tableItemComboBox��5#      $��3��64    . 
������� . 	 6{   �� *
TABLEINDEX+$
 TwoWay=�6�   5$      5%      �� 5'   
   $��选择字段��6   $��1��6!   $��0��61   $��3��6>   $��1q�6O   ���	#FFBCBCBC.��5(      � $��1q�6    5)      �� 5*      $��2* �6'   �� 5+      $��5* �6'   .+�5-      �� $��0��6   $��	字段：��6,   $��Center=�67   $��Right=�6R   �� 5.      -   5        $��fieldComboBox��5.      $��1��60    . 
������� . ��6e   �� 6�   5/      50      �� 52   
   $��查找��6   $��2��6   $��0��6/   $��3��6<   $��1q�6M   ���	#FFBCBCBC.��53      � $��1q�6    54      �� 55      $��4* �6'   �� 56      $��* �6'   .+�58      �� -   5        $��fieldTextBox��58      $��0��6.   $��1q�6>   
�������6I   �� 59      -   5        $��searchButton��59      $��1��6-   $��2q�6=   $��搜索��6H    �� 	IsDefault . 6p   5:      5;      �� 5=   
   -   5        $��tipsTextBlock��5=      $��0��6)   $��2��69   $��3��6M   $��Center=�6Z   
���  ��6w   �� 5>   
   -	   5        $��setttingButton��5>      $��3��6'   $��2��64   $��数据库参数设置��6D   $��4q�6s   )   #Microsoft.Windows.Controls.DataGrid  5A   
   -
   5          Name$ dataGrid��5A      $��0��6-   $��3��6=   $��5��6Q      AutoGenerateColumns .    
IsReadOnly . $��1q�6�   5B      5C      5D      