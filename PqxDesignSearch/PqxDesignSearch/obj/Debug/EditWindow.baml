   M S B A M L   `   `   `  ����J  FPqxDesignSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PqxDesignSearchPqxDesignSearch  5   	        PqxDesignSearch.EditWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   /localclr-namespace:PqxDesignSearch     5   	   -   5          � Title$'  !删除或者添加查询字符串��5   	   $	��300��6   $	��400��6*    � ShowInTaskbar .   � Topmost .  � WindowStyle$ 
ToolWindow=�6[    � WindowStartupLocation$ CenterScreen=�6t    � 	Resources 5      (  "PqxDesignSearch.IsCheckedConverter   isCheckConverter��%   &	          5   
   6#   5      .��5	      �  � RowDefinitions 5
   
   �� 5      $
��auto �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��2��6    $��0��61   $��所有查询算法��6A   .��5      �� -   5        $��waysListBox��5      $��5q�6)   ��64   �� 5      5      �� 5   
   $��0��6   $��1��6    $��主题��60   .��5      �� -   5        $��themeTextBox��5      $��5 5 5 0q�6*   
������� �� 
IsReadOnly 6;   �� 	 �� ElementName$	 	checkBox1��
 �� Path$
 	IsChecked�6w   5      �� 5   
   $��1��6   $��1��6    $��语句字符串��60   .��5      �� -   5        $��sentenceTextBox��5      $��5q�6.    � ToolTip$E ?如果不会添加语句字符串，请联系本软件作者！��69    �� TextWrapping$
 Wrap=�6Y   " �� VerticalScrollBarVisibility$
 Auto=�6m   
������� 6�   �� $	 	checkBox1��$
 	IsChecked�6�   5      � 5   
   $��0��6   $��2��6   $��2��6-    5       �� 5!      $
��auto �6    �� 5"      $
��auto �6    �� 5#      .+�5%      �� -   5        $��	addButton��5%      $��0��6&   $��新增��63   $��	2 20 8 10q�6@   $	��5 2q�6S   {�6a   �� $	 	checkBox1��$
 	IsChecked� �� 	Converter# [  6�   �� 5&      -   5        $��	delButton��5&      $��1��6'   $��删除��64   $��	2 10 8 10q�6A   $	��5 2q�6T   {�6b   �� $	 	checkBox1��$
 	IsChecked�# [  6�   �� 5'      -   5        $��	checkBox1��5'      $��2��6(   $��只读��65   $��2 10 8 0q�6B    d� 	IsChecked$
 TrueY�6T   5(      5)      5*      