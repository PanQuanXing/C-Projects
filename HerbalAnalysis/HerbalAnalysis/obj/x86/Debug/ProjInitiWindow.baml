   M S B A M L   `   `   `  ����I  EHerbalAnalysis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:HerbalAnalysisHerbalAnalysis  5   	   $    HerbalAnalysis.ProjInitiWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:HerbalAnalysis     5   	   -   5          � Title$  ProjInitiWindow��5   	   $	��180��6!   $	��420��6.   $��14(�6:    J� SnapsToDevicePixels .  � 
ResizeMode$ NoResize=�6c    � WindowStartupLocation$ CenterScreen=�6y   .��5	      � -   5        $��MainGrid��5	       � RowDefinitions 5
   
   �� 5      �� 5      �� 5       � ColumnDefinitions 5   
   �� 5      $
��auto �6   �� 5      �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��0��6   $��项目名称：��6-   $��
15 10 5 10q�6=   $��Center=�6Q   �� 5   
   -   5        $��ProjNameTextBox��5      $��0��6)   $��1��66   $
��5 10q�6F   $��Center=�6T   ��6v   �� *Name+6�   �� 5   
   $��1��6   $��0��6    � ToolTip$! 请选择一个合适路径��6-   $��保存位置：��6A   $��
15 10 5 10q�6Q   $��Center=�6e   �� 5   
   -   5        $��ProjPathTextBox��5      $��1��6)   $��1��66   $
��5 10q�6F   $��Center=�6�    6T   �� *Text+ �� ElementName$ ProjPathTextBox����6�   �� *ProjectFolderPath+6�   �� 5   
   -   5        $��ProjPathScanBtn��5      $��1��6(   $��2��65   $��	浏览...��6E   $
��25 4q�6U   $
��5 10q�6d   $��Center=�6r   6�   �� 5   
   -   5        $��OkBtn��5      $��2��6   $��1��6+   $��确定��6;   $��100 10 5 10q�6H   $��Center=�6]    �� 	IsDefault . 6�   �� 5   
   -   5        $��	CancelBtn��5      $��2��6"   $��2��6/   $��取消��6?   $
��5 10q�6L   $��Center=�6Z   6|   � 5   
   -   5        $��WarningPopup��5      	 � 	StaysOpen	 .  $��1q�66   $��Bottom=�6A   .��5      �� ��6   �� *ActualWidth+$ MainGrid����5      �� 5      .��5      � 
 � Color$
 	AliceBlue��6'    � Offset$ 0Q�69   � 5       $
 	LightBlue��6'   $ 1Q�69   5!      ._�5#      �� -	   5        $��WarningLabel��5#      
�������$��4q�6?   
���  ��$��Center=�6[   ��6   �� *WarningText+5$      5%      5&      5'      