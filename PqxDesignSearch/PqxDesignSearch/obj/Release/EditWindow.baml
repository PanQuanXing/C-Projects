   M S B A M L   `   `   `  ���� J  FPqxDesignSearch, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:PqxDesignSearchPqxDesignSearch       PqxDesignSearch.EditWindow   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     /localclr-namespace:PqxDesignSearch     -     � Title$'  !删除或者添加查询字符串��$	��300��$	��400�� � ShowInTaskbar .   � Topmost .  � WindowStyle$ 
ToolWindow=� � WindowStartupLocation$ CenterScreen=� � 	Resources (  "PqxDesignSearch.IsCheckedConverter   isCheckConverter��%   &	          .���  � RowDefinitions �� $
��auto ���  � ColumnDefinitions �� �� �� $
��auto �.+��� $��0��$��2��$��0��$��所有查询算法��.���� -   $��waysListBox��$��5q����� �� $��0��$��1��$��主题��.���� -   $��themeTextBox��$��5 5 5 0q�
������� �� 
IsReadOnly �� 	 �� ElementName$	 	checkBox1��
 �� Path$
 	IsChecked��� $��1��$��1��$��语句字符串��.���� -   $��sentenceTextBox��$��5q� � ToolTip$E ?如果不会添加语句字符串，请联系本软件作者！�� �� TextWrapping$
 Wrap=�" �� VerticalScrollBarVisibility$
 Auto=�
������� �� $	 	checkBox1��$
 	IsChecked�� $��0��$��2��$��2�� �� $
��auto ��� $
��auto ��� .+��� -   $��	addButton��$��0��$��新增��$��	2 20 8 10q�$	��5 2q�{��� $	 	checkBox1��$
 	IsChecked� �� 	Converter# [  �� -   $��	delButton��$��1��$��删除��$��	2 10 8 10q�$	��5 2q�{��� $	 	checkBox1��$
 	IsChecked�# [  �� -   $��	checkBox1��$��2��$��只读��$��2 10 8 0q� d� 	IsChecked$
 TrueY�