   M S B A M L   `   `   `  ����  laboratory_10    laboratory_10.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Car Management��5   	   $	��450��6    $	��800��6-   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��10q�6;   .+�5      �� -   5        $	��Add��5      $��60��6#   $��10,0,0,0q�6.   6@   �� 5      -   5        $��Delete��5      $��60��6&   $��10,0,0,0q�61   6C   �� 5      -   5        $��Display Everything��5      $	��120��62   $��10,0,0,0q�6>   6P   5      �� 5   
   $��0��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5      �� $��
Sort mode:��6   $��Center=�6*   �� 5      -   5         ��Name$ SortComboBox��5      $	��120��6.   $��10q�6:   .@�5      �� $��Model��6   �� 5      $��Engine��6   �� 5      $
��Year��6   5      5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ CarDataGrid��5         AutoGenerateColumns$ False��6(      CanUserAddRows$
 True��6D      CanUserDeleteRows$
 True��6Z      
IsReadOnly$ False��6s   $��1��6�   $��10q�6�   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Model��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6N   1  +System.Windows.Controls.DataGridBoundColumn   Binding 64   �� *Model+6N     5      $
 Engine��6%   $ * 6W    65   �� *EngineDetails+6W     5       $

 Year��6%   $ * 6L    63   �� *Year+6L   5"      �� 5$   
   $��2��6   $ 
Horizontal=�6"   $��Center=�6;   .+�5%      � .+�5&      �� -   5         ��Name$ 	SearchBox��5&      $	��120��6-   $��10q�69   5'      �� 5(      $��
Search by:��6   $��Center=�6*   �� 5)      -   5        $ SearchComboBox��5)      $	��120��6/   $��10q�6;   .@�5*      �� $��Model��6   �� 5+      $��Engine model��6   �� 5,      $
��Year��6   5-      �� 5.      -   5        $��Search��5.      $��60��6&   $��10q�61   6=   5/      50      51      