   M S B A M L   `   `   `  ����G  Claboratory_8, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:laboratory_8laboratory_8  5   	   #    laboratory_8.CreateNewElement   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:laboratory_8     5   	     � Title$  CreateNewElement��5   	   $	��450��6"   $	��800��6/   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��Name:��6   $��0��6    $��10q�6-   �� 5   
   -   5         ��Name$ NameTextBox��5      $��0��6'   $��100,10,10,10q�64   $��Center=�6J   �� 5   
   $��Type:��6   $��1��6    $��10q�6-   $��Center=�69   	� 5   
   -   5         	�Name$ fileRadioButton��5      $
��File��6/   $��1��6>   $��100,10,10,10q�6K   $��Center=�6a   	� 5   
   -   5        $ folderRadioButton��5      $��Folder��61   $��1��6B   $��200,10,10,10q�6O   $��Center=�6e   �� 5   
   $��Attributes:��6   $��2��6&   $��10q�63   �� 5   
   -   5         ��Name$ readOnlyCheckBox��5      $��	Read-only��6-   $��2��6A   $��100,10,10,10q�6N   $��Center=�6d   �� 5   
   -   5        $ archiveCheckBox��5      $��Archive��6,   $��2��6>   $��200,10,10,10q�6K   $��Center=�6a   �� 5   
   -   5        $ hiddenCheckBox��5      $��Hidden��6+   $��2��6<   $��300,10,10,10q�6I   $��Center=�6_   �� 5    
   -   5        $ systemCheckBox��5       $��System��6+   $��2��6<   $��400,10,10,10q�6I   $��Center=�6_   �� 5"   
   $��4��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $
��0,20q�6X   .+�5#      �� -   5         ��Name$ okButton��5#      $��OK��6'   $��10q�6K   $��80��6W   �� 5$      -	   5        $ cancelButton��5$      $��Cancel��6+   $��10q�6W   $��80��6c   5%      5&      5'      